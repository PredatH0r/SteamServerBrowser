using System.Collections.Generic;
using System.Net;
using QueryMaster;

namespace ServerBrowser
{
  public class ServerRow
  {
    private readonly Dictionary<string, string> rulesFieldCache = new Dictionary<string, string>();
    private readonly Dictionary<string, object> extenderFieldCache = new Dictionary<string, object>();

    public IPEndPoint EndPoint { get; private set; }
    public long RequestTimestamp { get; set; }
    public ServerInfo ServerInfo { get; set; }
    public PlayerCountInfo PlayerCount { get; private set; }
    internal int Retries { get; set; }
    public string Status { get; set; }
    public bool Dedicated { get { return ServerInfo != null && ServerInfo.ServerType == "Dedicated"; } }
    public List<Player> Players { get; set; }
    public List<Rule> Rules { get; set; }
    public int? TotalPlayers { get { return ServerInfo == null ? (int?)null : ServerInfo.Players + ServerInfo.Bots; } }

    public ServerRow(IPEndPoint ep)
    {
      this.EndPoint = ep;
      this.PlayerCount = new PlayerCountInfo(this);
    }

    public string GetRule(string field)
    {
      string value;
      this.rulesFieldCache.TryGetValue(field, out value);
      return value;
    }

    public object GetExtenderCellValue(GameExtension extension, string field)
    {
      if (this.ServerInfo == null)
        return null;
      object value;
      if (!this.extenderFieldCache.TryGetValue(field, out value))
        this.extenderFieldCache[field] = value = extension.GetServerCellValue(this, field);
      return value;
    }

    public void Update()
    {
      this.PlayerCount.Update();
      this.extenderFieldCache.Clear();

      this.rulesFieldCache.Clear();
      if (this.Rules != null)
      {
        foreach (var rule in this.Rules)
          rulesFieldCache[rule.Name] = rule.Value;
      }
    }
  }
}