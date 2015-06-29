using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading;
using QueryMaster;

namespace ServerBrowser
{
  #region class UpdateRequest
  class UpdateRequest
  {
    public UpdateRequest(int id)
    {
      this.Id = id;
    }

    public readonly int Id;
    public CountdownEvent PendingTasks;
    public int TaskCount;
    public int TasksWithRetries;
  }
  #endregion

  #region class TextEventArgs
  public class TextEventArgs : EventArgs
  {
    public string Text { get; private set; }

    public TextEventArgs(string text)
    {
      this.Text = text;
    }
  }
  #endregion

  #region class UpdateCompleteEventArgs
  public class UpdateCompleteEventArgs : EventArgs
  {
    public List<ServerRow> Rows { get; private set; }

    public UpdateCompleteEventArgs(List<ServerRow> rows)
    {
      this.Rows = rows;
    }
  }
  #endregion

  #region class ServerEventArgs
  public class ServerEventArgs : EventArgs
  {
    public ServerRow Server { get; private set; }

    public ServerEventArgs(ServerRow server)
    {
      this.Server = server;
    }
  }
  #endregion

  public class ServerQueryLogic
  {
    private volatile int prevRequestId; // logical clock to drop obsolete replies, e.g. when the user selected a different game in the meantime
    private volatile UpdateRequest currentRequest = new UpdateRequest(0);
    private volatile bool shutdown;
    private volatile List<ServerRow> allServers;
    private List<ServerRow> servers;
    private int maxResults;
    private bool queryServerRules;
    private bool sendFirstUdpPacketTwice;
    private int serverListUpdateNeeded; // bool, but there is no Interlocked.Exchange(bool)
    public event EventHandler<TextEventArgs> UpdateStatus;
    public event EventHandler<UpdateCompleteEventArgs> UpdateServerListComplete;
    public event EventHandler<ServerEventArgs> UpdateSingleServerComplete;

    #region IsUpdating
    public bool IsUpdating
    {
      get { return this.currentRequest.PendingTasks != null && !this.currentRequest.PendingTasks.IsSet; }
    }
    #endregion

    #region GetAndResetUpdateNeededFlag()
    public bool GetAndResetUpdateNeededFlag()
    {
      return Interlocked.Exchange(ref serverListUpdateNeeded, 0) != 0;
    }
    #endregion

    #region Servers
    public List<ServerRow> Servers  { get { return this.allServers; } }
    #endregion


    #region UpdateServerList()
    // ReSharper disable ParameterHidesMember
    public void UpdateServerList(IPEndPoint masterServerEndPoint, int maxResults, Game steamAppId, Region region, bool queryServerRules)
      // ReSharper restore ParameterHidesMember
    {
      this.maxResults = maxResults;
      this.queryServerRules = queryServerRules;
      var request = new UpdateRequest(++prevRequestId);
      var rows = new List<ServerRow>(); // local reference to guarantee thread safety
      this.servers = rows;

      MasterServer master = new MasterServer(masterServerEndPoint);
      master.GetAddressesLimit = maxResults;
      IpFilter filter = new IpFilter();
      filter.App = steamAppId;     

      master.GetAddresses(region, endpoints => OnMasterServerReceive(endpoints, request, rows), filter);
      this.currentRequest = request;
    }
    #endregion

    #region Cancel()
    public void Cancel()
    {
      this.shutdown = true;
    }
    #endregion

    #region OnMasterServerReceive()
    private void OnMasterServerReceive(ReadOnlyCollection<IPEndPoint> endPoints, UpdateRequest request, List<ServerRow> rows)
    {
      // ignore results from older queries
      if (request.Id != this.currentRequest.Id)
        return;

      string statusText;
      if (endPoints == null)
        statusText = "Master server request timed out";
      else
      {
        statusText = "Requesting next batch of server list...";
        foreach (var ep in endPoints)
        {
          if (this.shutdown)
            return;
          if (ep.Address.Equals(IPAddress.Any))
          {
            statusText = "Master server returned " + this.servers.Count + " servers";
            this.AllServersReceived(request, rows);
          }
          else if (servers.Count >= maxResults)
          {
            statusText = "Server list limited to " + maxResults + " entries";
            this.AllServersReceived(request, rows);
            break;
          }
          else
            rows.Add(new ServerRow(ep));
        }
      }

      this.serverListUpdateNeeded = 1;
      if (this.UpdateStatus != null)
        this.UpdateStatus(this, new TextEventArgs(statusText));
    }
    #endregion

    #region AllServersReceived()
    private void AllServersReceived(UpdateRequest request, List<ServerRow> rows)
    {
      // migrate old ServerRow into new list
      var oldServers = new Dictionary<IPEndPoint, ServerRow>();
      if (this.allServers != null)
      {
        foreach (var server in this.allServers)
          oldServers[server.EndPoint] = server;
      }
      for (int i=0, c=rows.Count; i<c; i++)
      {
        ServerRow oldServer;
        if (oldServers.TryGetValue(rows[i].EndPoint, out oldServer))
          rows[i] = oldServer;
      }
      this.allServers = rows;

      request.TaskCount = rows.Count;

      // use a background thread so that the caller doesn't have to wait for the accumulated Thread.Sleep()
      ThreadPool.QueueUserWorkItem(dummy =>
      {
        request.PendingTasks = new CountdownEvent(rows.Count);
        foreach (var row in rows)
        {
          if (request.Id != this.currentRequest.Id)
            return;
          var safeRow = row;
          ThreadPool.QueueUserWorkItem(context => UpdateServerDetails(safeRow, request));
          Thread.Sleep(5); // launching all threads at once results in totally wrong ping values
        }
        request.PendingTasks.Wait();

        if (request.Id != this.currentRequest.Id)
          return;
        
        this.UpdateServerListFinished(request, rows);
      });
    }
    #endregion

    #region UpdateServerListFinished()
    private void UpdateServerListFinished(UpdateRequest request, List<ServerRow> rows)
    {
      if (request.TasksWithRetries > request.TaskCount / 3)
        this.sendFirstUdpPacketTwice = true;

      if (this.UpdateServerListComplete != null)
        this.UpdateServerListComplete(this, new UpdateCompleteEventArgs(rows));      
    }
    #endregion


    #region UpdateSingleServer()
    public void UpdateSingleServer(ServerRow row)
    {
      row.Status = "updating...";
      this.currentRequest = new UpdateRequest(++this.prevRequestId);
      this.currentRequest.PendingTasks = new CountdownEvent(1);
      ThreadPool.QueueUserWorkItem(dummy =>
        this.UpdateServerDetails(row, this.currentRequest, () =>
        {
          if (this.UpdateSingleServerComplete != null)
            this.UpdateSingleServerComplete(this, new ServerEventArgs(row));
        }));
    }
    #endregion


    #region UpdateServerDetails()
    private void UpdateServerDetails(ServerRow row, UpdateRequest request, Action callback = null)
    {
      try
      {
        if (request.Id != this.currentRequest.Id) // drop obsolete requests
          return;

        string status;
        using (Server server = ServerQuery.GetServerInstance(EngineType.Source, row.EndPoint, false, 500, 500))
        {
          row.Retries = 0;
          server.SendFirstPacketTwice = this.sendFirstUdpPacketTwice;
          server.Retries = 3;
          status = "timeout";
          if (this.UpdateServerInfo(row, server, request))
          {
            this.UpdatePlayers(row, server, request);
            this.UpdateRules(row, server, request);
            status = "ok";
          }
        }

        if (request.Id != this.currentRequest.Id) // status might have changed
          return;

        if (row.Retries > 0)
          status += " (" + row.Retries + ")";
        row.Status = status;
        row.Update();

        if (this.shutdown)
          return;
        this.serverListUpdateNeeded = 1;
        if (callback != null)
          callback();
      }
      finally
      {
        request.PendingTasks.Signal();
      }
    }
    #endregion

    #region UpdateServerInfo()
    private bool UpdateServerInfo(ServerRow row, Server server, UpdateRequest request)
    {
      bool ok= UpdateDetail(row, server, request, retryHandler =>
      {
        row.ServerInfo = server.GetInfo(retryHandler);
        row.RequestId = request.Id;
      });
      if (!ok)
        row.ServerInfo = null;
      return ok;
    }
    #endregion

    #region UpdatePlayers()
    private void UpdatePlayers(ServerRow row, Server server, UpdateRequest request)
    {
      bool ok = UpdateDetail(row, server, request, retryHandler =>
      {
        var players = server.GetPlayers(retryHandler);
        row.Players = players == null ? null : new List<Player>(players);
      });
      if (!ok)
        row.Players = null;
    }
    #endregion

    #region UpdateRules()
    private void UpdateRules(ServerRow row, Server server, UpdateRequest request)
    {
      if (!this.queryServerRules)
        return;
      bool ok = UpdateDetail(row, server, request, retryHandler =>
      {
        row.Rules = new List<Rule>(server.GetRules(retryHandler));
      });
      if (!ok)
        row.Rules = null;
    }
    #endregion

    #region UpdateDetail()
    private bool UpdateDetail(ServerRow row, Server server, UpdateRequest request, Action<Action<int>> updater)
    {
      if (request.Id != this.currentRequest.Id)
        return false;

      try
      {
        row.Status = "updating " + row.Retries;
        this.serverListUpdateNeeded = 1;
        updater(retry =>
        {
          if (request.Id != currentRequest.Id)
            throw new OperationCanceledException();
          if (row.Retries == 0)
            Interlocked.Increment(ref request.TasksWithRetries);
          row.Status = "updating " + (++row.Retries + 1);
          this.serverListUpdateNeeded = 1;
        });
        return true;
      }
      catch (TimeoutException)
      {
        return false;
      }
      catch
      {
        return true;
      }
    }
    #endregion
  }
}
