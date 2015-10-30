using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading;
using QueryMaster;

namespace ServerBrowser
{
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

  #region class ServerListEventArgs
  public class ServerListEventArgs : EventArgs
  {
    public List<ServerRow> Rows { get; private set; }

    public ServerListEventArgs(List<ServerRow> rows)
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
    #region class UpdateRequest
    private class UpdateRequest
    {
      public readonly long Timestamp;
      public readonly Game AppId;
      public readonly int MaxResults;
      public readonly int Timeout;
      public readonly GameExtension GameExtension;
      public readonly List<ServerRow> Servers = new List<ServerRow>();

      public UpdateRequest(Game appId, int maxResults, int timeout, GameExtension gameExtension)
      {
        this.Timestamp = DateTime.Now.Ticks;
        this.AppId = appId;
        this.MaxResults = maxResults;
        this.Timeout = timeout;
        this.GameExtension = gameExtension;
      }

      public int receivedServerCount;
      public CountdownEvent PendingTasks;
      public int TaskCount;
      public int TasksWithRetries;
      public int TasksWithTimeout;

      public volatile bool IsCancelled;

      #region Get/SetDataModified()

      private int DataModified; // bool, but there is no Interlocked.Exchange(bool)
      public bool KeepPreviousPing { get; set; }
      public int BatchNumber { get; set; }

      public void SetDataModified()
      {
        Interlocked.Exchange(ref this.DataModified, 1);
      }

      public bool GetAndResetDataModified()
      {
        return Interlocked.Exchange(ref this.DataModified, 0) != 0;
      }
      #endregion
    }
    #endregion

    private readonly GameExtensionPool gameExtensions;
    private volatile UpdateRequest currentRequest;
    private volatile List<ServerRow> allServers;
    private bool sendFirstUdpPacketTwice;

    public event EventHandler<TextEventArgs> UpdateStatus;
    public event EventHandler ServerListReceived;
    public event EventHandler<ServerListEventArgs> ReloadServerListComplete;
    public event EventHandler<ServerEventArgs> RefreshSingleServerComplete;

    #region ctor()
    public ServerQueryLogic(GameExtensionPool gameExtensions)
    {
      this.gameExtensions = gameExtensions;

      // fake a completed request
      this.currentRequest = new UpdateRequest(0, 0, 500, gameExtensions.Get(0));
      this.currentRequest.PendingTasks = new CountdownEvent(1);
      this.currentRequest.PendingTasks.Signal();
    }
    #endregion

    #region Servers
    public List<ServerRow> Servers => this.allServers;

    #endregion

    #region IsUpdating
    public bool IsUpdating => !this.currentRequest.PendingTasks?.IsSet ?? true;

    #endregion

    #region GetAndResetDataModified()
    public bool GetAndResetDataModified()
    {
      return this.currentRequest.GetAndResetDataModified();
    }
    #endregion

    #region Cancel()
    public void Cancel()
    {
      this.currentRequest.IsCancelled = true;
    }
    #endregion

    // reload server list

    #region ReloadServerList()
    public void ReloadServerList(IServerSource serverSource, int timeout, int maxResults, Region region, IpFilter filter)
    {
      this.currentRequest.IsCancelled = true;

      var extension = this.gameExtensions.Get(filter.App);
      var request = new UpdateRequest(filter.App, maxResults, timeout, extension); // use local var for thread safety
      this.currentRequest = request;
      serverSource.GetAddresses(region, filter, maxResults, endpoints => OnMasterServerReceive(request, endpoints));
    }
    #endregion

    #region OnMasterServerReceive()
    private void OnMasterServerReceive(UpdateRequest request, ReadOnlyCollection<IPEndPoint> endPoints)
    {
      if (request.IsCancelled)
        return;

      string statusText;
      if (endPoints == null)
      {
        statusText = $"Master server request timed out after returning {request.receivedServerCount} servers (clients are limited to receive 30 packets per minute)";
        if (request.receivedServerCount > 0)
          this.AllServersReceived(request);
      }
      else
      {
        statusText = $"Requesting batch {++request.BatchNumber} of server list...";
        foreach (var ep in endPoints)
        {
          if (request.IsCancelled)
            return;
          if (ep.Address.Equals(IPAddress.Any))
          {
            statusText = $"Master server returned {request.Servers.Count} servers";
            this.AllServersReceived(request);
          }
          else if (++request.receivedServerCount >= request.MaxResults)
          {
            statusText = $"Server list limited to {request.MaxResults} entries";
            this.AllServersReceived(request);
            break;
          }
          else if (request.GameExtension.AcceptGameServer(ep))
            request.Servers.Add(new ServerRow(ep, request.GameExtension));
        }
      }

      request.SetDataModified();
      this.UpdateStatus?.Invoke(this, new TextEventArgs(statusText));
    }
    #endregion

    #region RefreshAllServers()
    public void RefreshAllServers(List<ServerRow> servers)
    {
      var request = new UpdateRequest(this.currentRequest.AppId, this.currentRequest.MaxResults, this.currentRequest.Timeout, this.currentRequest.GameExtension);
      request.Servers.AddRange(servers);
      foreach (var server in servers)
        server.Status = "updating...";
      this.currentRequest.SetDataModified();
      ThreadPool.QueueUserWorkItem(ctx => this.AllServersReceived(request));
    }
    #endregion
    
    #region AllServersReceived()
    private void AllServersReceived(UpdateRequest request)
    {
      var rows = request.Servers;

      // reuse old ServerRow objects for new list to preserve last known data until the update completes
      if (this.allServers != null)
      {
        var oldServers = new Dictionary<IPEndPoint, ServerRow>();
        foreach (var server in this.allServers)
          oldServers[server.EndPoint] = server;
        for (int i = 0, c = rows.Count; i < c; i++)
        {
          ServerRow oldServer;
          if (oldServers.TryGetValue(rows[i].EndPoint, out oldServer))
            rows[i] = oldServer;
        }
      }

      this.allServers = rows;
      this.ServerListReceived?.Invoke(this, EventArgs.Empty);

      request.TaskCount = rows.Count;

      // use a background thread so that the caller doesn't have to wait for the accumulated Thread.Sleep()
      ThreadPool.QueueUserWorkItem(dummy =>
      {
        request.PendingTasks = new CountdownEvent(rows.Count);
        foreach (var row in rows)
        {
          if (request.IsCancelled)
            return;
          var safeRow = row;
          ThreadPool.QueueUserWorkItem(context => UpdateServerAndDetails(request, safeRow, false));
          Thread.Sleep(5); // launching all threads at once results in totally wrong ping values
        }
        request.PendingTasks.Wait();

        if (request.IsCancelled)
          return;
        
        this.ReloadServerListFinished(request);
      });
    }
    #endregion

    #region ReloadServerListFinished()
    private void ReloadServerListFinished(UpdateRequest request)
    {
      if (request.TasksWithTimeout > request.TaskCount/4)
        this.sendFirstUdpPacketTwice = false;
      else if (request.TasksWithRetries > request.TaskCount / 3)
        this.sendFirstUdpPacketTwice = true;

      this.ReloadServerListComplete?.Invoke(this, new ServerListEventArgs(request.Servers));
    }
    #endregion

    // refresh single server

    #region RefreshSingleServer()
    public void RefreshSingleServer(ServerRow row, bool updatePing = true)
    {
      row.Status = "updating...";
      var req = new UpdateRequest(this.currentRequest.AppId, 1, this.currentRequest.Timeout, this.currentRequest.GameExtension);
      req.KeepPreviousPing = !updatePing;
      req.PendingTasks = new CountdownEvent(1);
      this.currentRequest = req;
      ThreadPool.QueueUserWorkItem(dummy => this.UpdateServerAndDetails(req, row, true));
    }
    #endregion

    // shared update code

    #region UpdateServerAndDetails()
    private void UpdateServerAndDetails(UpdateRequest request, ServerRow row, bool fireRefreshSingleServerComplete)
    {
      string status = "";
      try
      {
        if (request.IsCancelled)
          return;

        using (Server server = ServerQuery.GetServerInstance(EngineType.Source, row.EndPoint, false, request.Timeout, request.Timeout))
        {
          row.Retries = 0;
          server.SendFirstPacketTwice = this.sendFirstUdpPacketTwice;
          server.Retries = 3;
          status = "timeout";
          var oldPing = row.ServerInfo?.Ping ?? 0;
          if (this.UpdateServerInfo(request, row, server))
          {
            if (request.KeepPreviousPing && row.ServerInfo != null)
              row.ServerInfo.Ping = oldPing; // keep old ping so that the row isn't immediately resorted and moved
            this.UpdatePlayers(request, row, server);
            this.UpdateRules(request, row, server);
            status = "ok";
          }
          row.RequestTimestamp = request.Timestamp;
        }

        if (request.IsCancelled) // status might have changed
          return;

        if (row.Retries > 0)
          status += " (" + row.Retries + ")";
      }
      catch
      {
        // this happens when you hibernate windows and the program resumes before the network connection has be reestablished
        status = "network error";
      }
      finally
      {
        row.Status = status;
        row.Update();
        request.SetDataModified();

        if (fireRefreshSingleServerComplete)
          this.RefreshSingleServerComplete?.Invoke(this, new ServerEventArgs(row));

        if (!request.PendingTasks.IsSet)
          request.PendingTasks.Signal();
      }
    }
    #endregion

    #region UpdateServerInfo()
    private bool UpdateServerInfo(UpdateRequest request, ServerRow row, Server server)
    {
      bool ok = ExecuteUpdate(request, row, server, retryCallback =>
      {
        var si = server.GetInfo(retryCallback);
        if (si == null)
          return false;
        row.ServerInfo = si;
        var gameId = si.Extra?.GameId ?? 0;
        if (gameId == 0) gameId = si.Id;
        if (gameId == 0) gameId = (int)request.AppId;
        var extension = this.gameExtensions.Get((Game)gameId);
        row.GameExtension = extension;
        row.QueryPlayers = extension.SupportsPlayersQuery(row);
        row.QueryRules = extension.SupportsRulesQuery(row);
        return true;
      });
      //if (!ok)
      //  row.ServerInfo = null;
      return ok;
    }
    #endregion

    #region UpdatePlayers()
    private void UpdatePlayers(UpdateRequest request, ServerRow row, Server server)
    {
      if (!row.QueryPlayers)
        return;
      bool ok = ExecuteUpdate(request, row, server, retryCallback =>
      {
        var players = server.GetPlayers(retryCallback);
        if (players == null) return false;
        row.Players =new List<Player>(players);
        return true;
      });
      if (!ok)
      {
        row.Players = null;
        row.QueryPlayers = false;
      }
    }
    #endregion

    #region UpdateRules()
    private void UpdateRules(UpdateRequest request, ServerRow row, Server server)
    {
      if (!row.QueryRules)
        return;
      bool ok = ExecuteUpdate(request, row, server, retryCallback =>
      {
        var rules = server.GetRules(retryCallback);
        if (rules == null) return false;
        row.Rules = new List<Rule>(rules);
        return true;
      });
      if (!ok)
      {
        row.Rules = null;
        row.QueryRules = false;
      }
    }
    #endregion

    #region ExecuteUpdate()
    /// <summary>
    /// Template method for common code needed in UpdateServerInfo, UpdatePlayers and UpdateRules
    /// </summary>
    private bool ExecuteUpdate(UpdateRequest request, ServerRow row, Server server, Func<Action<int>, bool> updater)
    {
      if (request.IsCancelled)
        return false;

      try
      {
        row.Status = "updating " + row.Retries;
        request.SetDataModified();
        bool ok = updater(retry =>
        {
          if (request.IsCancelled)
            throw new OperationCanceledException();
          if (row.Retries == 0)
            Interlocked.Increment(ref request.TasksWithRetries);
          row.Status = "updating " + (++row.Retries + 1);
          request.SetDataModified();
        });
        if (ok)
          return true;
        Interlocked.Increment(ref request.TasksWithTimeout);
      }
      catch
      {
      }
      return false;
    }
    #endregion
  }
}
