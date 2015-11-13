using System;
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
    public Encoding Encoding { get; set; }
    public event DownloadStringCompletedEventHandler DownloadStringCompleted;
    public event DownloadDataCompletedEventHandler DownloadDataCompleted;
    public Exception Error { get; private set; }
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
      // just for syntax compatibility with WebClient class
    }
    #endregion

    #region DownloadString, DownloadStringAsync

    public string DownloadString(string url)
    {
      return this.DownloadString(new Uri(url));
    }

    public string DownloadString(Uri url)
    {
      var request = new AsyncRequest(url, null);
      this.DownloadStringWithTimeout(request);
      this.Error = request.StringResult.Error;
      return this.Error != null ? null : request.StringResult?.Result;
    }

    public void DownloadStringAsync(Uri url, object state = null)
    {
      if (this.DownloadStringCompleted == null)
        return;
      var request = new AsyncRequest(url, state);
      ThreadPool.QueueUserWorkItem(this.DownloadStringWithTimeout, request);
    }

    private void DownloadStringWithTimeout(object asyncRequest)
    {
      var request = (AsyncRequest)asyncRequest;
      using (request)
      {
        // the WebClient calls an event through the Windows message pump, so we can't make a blocking wait here and have to call the pump every now and then
        request.DownloadStringAsync(this.Encoding);
        long time = DateTime.UtcNow.Millisecond;
        while (!request.WaitHandle.WaitOne(100))
        {
          if (DateTime.UtcNow.Millisecond - time < this.Timeout)
          {
            System.Windows.Forms.Application.DoEvents();
            continue;
          }

          var internalCtor = typeof(DownloadStringCompletedEventArgs).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null,
            new[] { typeof(string), typeof(Exception), typeof(bool), typeof(object) }, null);
          request.StringResult = (DownloadStringCompletedEventArgs)internalCtor.Invoke(new[] { null, new TimeoutException("Request timed out"), false, request.State });
        }
      }

      var handler = this.DownloadStringCompleted;
      handler?.Invoke(this, request.StringResult);
    }
    #endregion

    #region DownloadData, DownloadDataAsync

    public byte[] DownloadData(Uri url)
    {
      var request = new AsyncRequest(url, null);
      this.DownloadDataWithTimeout(request);
      if (request.DataResult.Error != null)
        throw request.DataResult.Error;
      return request.DataResult.Result;
    }


    public void DownloadDataAsync(Uri url, object state)
    {
      if (this.DownloadDataCompleted == null)
        return;
      var request = new AsyncRequest(url, state);
      ThreadPool.QueueUserWorkItem(this.DownloadDataWithTimeout, request);
    }

    private void DownloadDataWithTimeout(object asyncRequest)
    {
      var request = (AsyncRequest)asyncRequest;
      using (request)
      {
        request.DownloadDataAsync();
        if (!request.WaitHandle.WaitOne(this.Timeout))
        {
          var ex = new TimeoutException("Request timed out");
          var internalCtor = typeof(DownloadDataCompletedEventArgs).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null,
            new[] { typeof(byte[]), typeof(Exception), typeof(bool), typeof(object) }, null);
          request.DataResult = (DownloadDataCompletedEventArgs)internalCtor.Invoke(new [] { null, ex, false, request.State });
        }
      }

      var handler = this.DownloadDataCompleted;
      if (handler != null)
        handler(this, request.DataResult);
    }
    #endregion

    #region class AsyncRequest
    class AsyncRequest : IDisposable
    {
      public readonly Uri Url;
      public readonly object State;
      private readonly WebClient webClient;

      public readonly ManualResetEvent WaitHandle = new ManualResetEvent(false);
      public DownloadDataCompletedEventArgs DataResult;
      public DownloadStringCompletedEventArgs StringResult;

      public AsyncRequest(Uri url, object state)
      {
        this.Url = url;
        this.State = state;
        this.webClient = new WebClient();
        this.webClient.Proxy = null;
        this.webClient.DownloadStringCompleted += DownloadStringCompleted;
        this.webClient.DownloadDataCompleted += DownloadDataCompleted;
      }

      public void DownloadStringAsync(Encoding encoding)
      {
        if (encoding != null)
          this.webClient.Encoding = encoding;
        this.webClient.DownloadStringAsync(this.Url, this.State);
      }

      public void DownloadDataAsync()
      {
        this.webClient.DownloadDataAsync(this.Url, this.State);
      }

      private void DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
      {
        this.StringResult = e;
        this.WaitHandle.Set();
      }

      private void DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
      {
        this.DataResult = e;
        this.WaitHandle.Set();
      }

      public void Dispose()
      {
        this.webClient.Dispose();
      }
    }
    #endregion
  }
}
