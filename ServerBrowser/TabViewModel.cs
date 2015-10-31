using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using QueryMaster;

namespace ServerBrowser
{
  class TabViewModel
  {
    internal List<ServerRow> servers;
    internal ServerRow lastSelectedServer;
    internal ServerRow currentServer;
    internal IServerSource serverSource;
    internal GameExtension gameExtension;

    public enum SourceType { MasterServer, CustomList, Favorites }

    public string MasterServer { get; set; }
    public int InitialGameID { get; set; }
    public string FilterMod { get; set; }
    public string FilterMap { get; set; }
    public string TagsInclude { get; set; }
    public string TagsExclude { get; set; }
    public bool GetEmptyServers { get; set; }
    public bool GetFullServers { get; set; }
    public int MasterServerQueryLimit { get; set; }

    public string GridFilter { get; set; }
    public MemoryStream ServerGridLayout { get; set; }
    public SourceType Source { get; set; }
    public int ImageIndex => Source == SourceType.Favorites ? 3 : Source == SourceType.CustomList ? 12 : -1;
    public List<string> CustomDetailColumns { get; } = new List<string>();
    public List<string> CustomRuleColumns { get; } = new List<string>();

    public TabViewModel()
    {
      this.GetEmptyServers = true;
      this.GetFullServers = true;
      this.MasterServerQueryLimit = 1000;
    }

    #region AssignFrom()
    public void AssignFrom(TabViewModel opt)
    {
      this.MasterServer = opt.MasterServer;
      this.InitialGameID = opt.InitialGameID;
      this.FilterMod = opt.FilterMod;
      this.FilterMap = opt.FilterMap;
      this.TagsInclude = opt.TagsInclude;
      this.TagsExclude = opt.TagsExclude;
      this.GetEmptyServers = opt.GetEmptyServers;
      this.GetFullServers = opt.GetFullServers;
      this.MasterServerQueryLimit = opt.MasterServerQueryLimit;

      this.Source = opt.Source;
      this.serverSource = opt.serverSource;
      this.servers = opt.servers;
      this.gameExtension = opt.gameExtension;
      this.GridFilter = opt.GridFilter;
      this.ServerGridLayout = opt.ServerGridLayout;
      this.CustomDetailColumns.AddRange(opt.CustomDetailColumns);
      this.CustomRuleColumns.AddRange(opt.CustomRuleColumns);
    }
    #endregion

    #region LoadFromIni()
    public void LoadFromIni(IniFile iniFile, IniFile.Section ini, GameExtensionPool pool)
    {
      this.Source = (SourceType) ini.GetInt("Type");
      this.MasterServer = ini.GetString("MasterServer") ?? "hl2master.steampowered.com:27011";
      this.InitialGameID = ini.GetInt("InitialGameID");
      this.FilterMod = ini.GetString("FilterMod");
      this.FilterMap = ini.GetString("FilterMap");
      this.TagsInclude = ini.GetString("TagsInclude");
      this.TagsExclude = ini.GetString("TagsExclude");
      this.GetEmptyServers = ini.GetBool("GetEmptyServers", true);
      this.GetFullServers = ini.GetBool("GetFullServers", true);
      this.MasterServerQueryLimit = ini.GetInt("MasterServerQueryLimit", this.MasterServerQueryLimit);
      this.GridFilter = ini.GetString("GridFilter");

      this.CustomDetailColumns.Clear();
      foreach (var detail in (ini.GetString("CustomDetailColumns") ?? "").Split(','))
      {
        if (detail != "")
          CustomRuleColumns.Add(detail);
      }

      this.CustomRuleColumns.Clear();
      foreach (var rule in (ini.GetString("CustomRuleColumns") ?? "").Split(','))
      {
        if (rule != "")
          CustomRuleColumns.Add(rule);
      }

      var layout = ini.GetString("GridLayout");
      if (!string.IsNullOrEmpty(layout))
        this.ServerGridLayout = new MemoryStream(Convert.FromBase64String(layout));

      this.gameExtension = pool.Get((Game) this.InitialGameID);

      if (this.Source == SourceType.CustomList)
      {
        this.servers = new List<ServerRow>();

        // new config format
        var sec = iniFile.GetSection(ini.Name + "_Servers");
        if (sec != null)
        {
          foreach (var key in sec.Keys)
          {
            var row = new ServerRow(Ip4Utils.ParseEndpoint(key), this.gameExtension);
            row.CachedName = sec.GetString(key);
            this.servers.Add(row);
          }
        }
        else
        {
          // old config format
          var oldSetting = ini.GetString("Servers") ?? "";
          foreach (var server in oldSetting.Split('\n', ' '))
          {
            var s = server.Trim();
            if (s == "") continue;
            this.servers.Add(new ServerRow(Ip4Utils.ParseEndpoint(s), this.gameExtension));
          }
        }
      }
    }
    #endregion

    #region WriteToIni()
    public void WriteToIni(StringBuilder ini, string sectionName, string tabName)
    {
      ini.AppendLine();
      ini.AppendLine($"[{sectionName}]");
      ini.Append("TabName=").AppendLine(tabName);
      ini.Append("Type=").Append((int) this.Source).AppendLine();
      if (this.Source == SourceType.MasterServer)
      {
        ini.Append("MasterServer=").AppendLine(this.MasterServer);
        ini.Append("InitialGameID=").Append(this.InitialGameID).AppendLine();
        ini.Append("FilterMod=").AppendLine(this.FilterMod);
        ini.Append("FilterMap=").AppendLine(this.FilterMap);
        ini.Append("TagsInclude=").AppendLine(this.TagsInclude);
        ini.Append("TagsExclude=").AppendLine(this.TagsExclude);
        ini.Append("GetEmptyServers=").AppendLine(this.GetEmptyServers ? "1" : "0");
        ini.Append("GetFullServers=").AppendLine(this.GetFullServers ? "1" : "0");
        ini.Append("MasterServerQueryLimit=").Append(this.MasterServerQueryLimit).AppendLine();
      }
      ini.Append("GridFilter=").AppendLine(this.GridFilter);
      if (this.ServerGridLayout != null)
      {
        var buf = new byte[this.ServerGridLayout.Length];
        this.ServerGridLayout.Seek(0, SeekOrigin.Begin);
        var len = this.ServerGridLayout.Read(buf, 0, buf.Length);
        ini.Append("GridLayout=").AppendLine(Convert.ToBase64String(buf, 0, len));
      }

      var sb = new StringBuilder();
      foreach (var detail in this.CustomDetailColumns)
        sb.Append(detail).Append(",");
      ini.Append("CustomDetailColumns=").AppendLine(sb.ToString().TrimEnd(','));

      sb = new StringBuilder();
      foreach (var rule in this.CustomRuleColumns)
        sb.Append(rule).Append(",");
      ini.Append("CustomRuleColumns=").AppendLine(sb.ToString().TrimEnd(','));

      if (this.Source == SourceType.CustomList)
      {
        ini.AppendLine();
        ini.AppendLine($"[{sectionName}_Servers]");
        foreach (var row in this.servers)
          ini.AppendLine($"{row.EndPoint}={row.ServerInfo?.Name ?? row.CachedName}");
      }
    }
    #endregion
  }
}
