using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;

namespace ServerBrowser
{
  /// <summary>
  /// The .NET built-in WebClient class has numerous bugs in its timeout handling.
  /// This helper class emulates the WebClient API and adds a working timeout handling to it.
  /// - setting WebClient.Proxy=null works in many cases, but not all
  /// - even when overriding WebClient.GetWebRequest() and setting a timeout, it is on some PCs ignored in synchronous requests
  /// - even when overriding WebClient.GetWebRequest() and setting a timeout, it is always ignored in asynchronous requests
  /// In the above cases where the set timeout is ignored (e.g. when connecting to a non-opened port), the timeout happens after 20sec
  /// </summary>
  public class XWebClient : IDisposable
  {
    public int Timeout { get; set; }
    public Encoding Encoding { get; set; } = new UTF8Encoding(false);
    public Exception Error { get; private set; }

    public event DownloadStringCompletedEventHandler DownloadStringCompleted;
    public event DownloadDataCompletedEventHandler DownloadDataCompleted;

    private ManualResetEvent openReadWaitHandle;
    private object openReadResult;
    private WebResponse response;

    #region ctor, Dispose

    public XWebClient() : this(1000)
    {
    }

    public XWebClient(int timeout)
    {
      this.Timeout = timeout;
    }

    public void Dispose()
    {
      if (this.openReadResult is IDisposable disp)
        disp.Dispose();
      if (this.response is IDisposable disp2)
        disp2.Dispose();
    }
    #endregion

    public WebHeaderCollection Headers { get; set; } = new WebHeaderCollection();

    public int ResumeOffset { get; set; }

    #region OpenRead()

    public Stream OpenRead(string address)
    {
      this.openReadWaitHandle = new ManualResetEvent(false);
      var thread = new Thread(OpenReadWorker);
      thread.Name = "OpenReadWorker";
      thread.IsBackground = true;
      thread.Start(new Uri(address));
      if (this.openReadWaitHandle.WaitOne(this.Timeout + 25))
      {
        if (this.openReadResult is Exception ex)
          throw ex;

        // need to wrap the stream so that when the calling code is disposing the stream, we can dispose the WebResponse along with it.
        // otherwise the HTTP connection stays open and the server may block new connections when enforcing a connection count limit.
        return new StreamWrapper((Stream)this.openReadResult, this.response, 0);
      }
      throw new TimeoutException("Timeout waiting for " + address);
    }

    private void OpenReadWorker(object uri)
    {
      try
      {
        var req = (HttpWebRequest)WebRequest.Create((Uri)uri);
        req.Timeout = this.Timeout;
        if (this.ResumeOffset > 0)
          req.AddRange(this.ResumeOffset);

        // check if server supports resuming
        this.response = req.GetResponse();
        if (this.response.Headers["Accept-Ranges"] != "bytes")
          this.ResumeOffset = 0;

        this.openReadResult = this.response.GetResponseStream();
      }
      catch (Exception ex)
      {
        this.openReadResult = ex;
      }
      this.openReadWaitHandle.Set();
    }
    #endregion

    #region DownloadString, DownloadStringAsync

    public string DownloadString(string url)
    {
      return this.DownloadString(new Uri(url));
    }

    public string DownloadString(Uri url)
    {
      var req = this.CreateWebRequest(url);
      using (var res = req.GetResponse())
      using (var r = new StreamReader(res.GetResponseStream(), this.Encoding))
      {
        return r.ReadToEnd();
      }
    }

    public void DownloadStringAsync(Uri url, object state = null)
    {
      var t = new Thread(ctx =>
      {
        Exception error = null;
        string result = null;

        try
        {
          result = DownloadString(url);
        }
        catch (Exception ex)
        {
          error = ex;
        }

        if (this.DownloadStringCompleted != null)
        {
          var internalCtor = typeof(DownloadStringCompletedEventArgs).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null,
            new[] { typeof(string), typeof(Exception), typeof(bool), typeof(object) }, null);
          var args = (DownloadStringCompletedEventArgs)internalCtor.Invoke(new[] { result, error, false, state });
          this.DownloadStringCompleted?.Invoke(this, args);
        }
      });
      t.Name = this.GetType().Name + nameof(this.DownloadStringAsync);
      t.IsBackground = true;
      t.Start();
    }

    #endregion

    #region DownloadData, DownloadDataAsync

    public byte[] DownloadData(string url)
    {
      return DownloadData(new Uri(url));
    }

    public byte[] DownloadData(Uri url)
    {
      this.Error = null;
      var req = this.CreateWebRequest(url);
      using (var res = req.GetResponse())
      {
        var mem = new MemoryStream((int)req.ContentLength);
        using (var r = res.GetResponseStream())
        {
          r.CopyTo(mem);
          return mem.GetBuffer();
        }
      }
    }

    public void DownloadDataAsync(Uri url, object state)
    {
      var t = new Thread(ctx =>
      {
        Exception error = null;
        byte[] result = null;

        try
        {
          result = DownloadData(url);
        }
        catch (Exception ex)
        {
          error = ex;
        }

        if (this.DownloadDataCompleted != null)
        {
          var internalCtor = typeof(DownloadDataCompletedEventArgs).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null,
            new[] { typeof(byte[]), typeof(Exception), typeof(bool), typeof(object) }, null);
          var args = (DownloadDataCompletedEventArgs)internalCtor.Invoke(new[] { result, error, false, state });
          this.DownloadDataCompleted?.Invoke(this, args);
        }
      });
      t.Name = this.GetType().Name + nameof(this.DownloadDataAsync);
      t.IsBackground = true;
      t.Start();
    }
    #endregion

    #region UploadString
    public string UploadString(string url, string data)
    {
      return UploadString(new Uri(url), data);
    }

    public string UploadString(Uri url, string data)
    {
      var req = this.CreateWebRequest(url);
      req.Method = "POST";
      var bytes = this.Encoding.GetBytes(data);
      req.ContentLength = bytes.Length;

      using (var s = req.GetRequestStream())
      {
        s.Write(bytes, 0, bytes.Length);
        s.Close();
      }

      using (var res = req.GetResponse())
      using (var r = new StreamReader(res.GetResponseStream(), this.Encoding))
      {
        return r.ReadToEnd();
      }
    }
    #endregion

    #region CreateWebRequest
    private WebRequest CreateWebRequest(Uri url)
    {
      this.Error = null;

      // Some header fields like ContentType MUST NOT be set via Headers[x], but have specific properties instead.
      // Headers must be set first, because this clears out all other properties

      var req = WebRequest.Create(url);

      var headers = new WebHeaderCollection();
      var propertySetters = new List<Action>();
      foreach (string key in this.Headers.Keys)
      {
        var val = this.Headers.Get(key);
        if (key == "Content-Type")
          propertySetters.Add(() => req.ContentType = val);
        else if (key == "Content-Length")
          propertySetters.Add(() => req.ContentLength = int.Parse(val));
        else
          headers.Add(key, val);
      }

      req.Headers = headers; // this clears all other properties like ContentType, ...
      foreach (var propertySetter in propertySetters)
        propertySetter();

      req.Timeout = this.Timeout;
      return req;
    }
    #endregion
  }

  #region class StreamWrapper
  /// <summary>
  /// Helper class to dispose WebResponse when the stream consumer disposes the ResponseStream.
  /// It can also simulate network errors for testing
  /// </summary>
  class StreamWrapper : Stream
  {
    private readonly Stream impl;
    private IDisposable disposeOnClose;
    private readonly double errorProbability;
    private readonly Random rnd;

    public StreamWrapper(Stream stream, IDisposable disposeOnClose, double errorProbability = 0)
    {
      this.impl = stream;
      this.disposeOnClose = disposeOnClose;
      this.errorProbability = errorProbability;
      if (errorProbability > 0)
        this.rnd = new Random();
    }

    public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state) => impl.BeginRead(buffer, offset, count, callback, state);

    public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state) => impl.BeginWrite(buffer, offset, count, callback, state);

    public override void Close() => impl.Close();


#pragma warning disable 809
    [Obsolete]
    protected override WaitHandle CreateWaitHandle()
#pragma warning restore 809
    {
      var meth = this.impl.GetType().GetMethod("CreateWaitHandle", BindingFlags.Instance | BindingFlags.NonPublic);
      return (WaitHandle)meth.Invoke(this.impl, null);
    }

    public override int EndRead(IAsyncResult asyncResult) => impl.EndRead(asyncResult);

    public override void EndWrite(IAsyncResult asyncResult) => impl.EndWrite(asyncResult);

    public override void Flush() => this.impl.Flush();

#pragma warning disable 809
    [Obsolete()]
    protected override void ObjectInvariant()
#pragma warning restore 809
    {
      var meth = this.impl.GetType().GetMethod("ObjectInvariant", BindingFlags.Instance | BindingFlags.NonPublic);
      meth.Invoke(this.impl, null);
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      if (this.rnd?.NextDouble() <= this.errorProbability)
        throw new IOException("random error");
      return this.impl.Read(buffer, offset, count);
    }

    public override int ReadByte() => impl.ReadByte();

    public override long Seek(long offset, SeekOrigin origin) => this.impl.Seek(offset, origin);

    public override void SetLength(long value)
    {
      this.impl.SetLength(value);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      if (this.rnd?.NextDouble() <= this.errorProbability)
        throw new IOException("random error");
      this.impl.Write(buffer, offset, count);
    }

    public override bool CanRead => this.impl.CanRead;

    public override bool CanSeek => this.impl.CanSeek;

    public override bool CanWrite => this.impl.CanWrite;

    public override bool CanTimeout => this.impl.CanTimeout;

    public override long Length => this.impl.Length;

    public override int ReadTimeout
    {
      get => this.impl.ReadTimeout;
      set => this.impl.ReadTimeout = value;
    }

    public override int WriteTimeout
    {
      get => this.impl.WriteTimeout;
      set => this.impl.WriteTimeout = value;
    }

    public override long Position
    {
      get => this.impl.Position;
      set => this.impl.Position = value;
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      this.disposeOnClose?.Dispose();
      this.disposeOnClose = null;
    }
  }
  #endregion
}
