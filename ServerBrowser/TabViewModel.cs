using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ChanSort.Api;
using QueryMaster;

namespace ServerBrowser
{
  class TabViewModel : IViewModel
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

    public TabViewModel()
    {
      this.GetEmptyServers = true;
      this.GetFullServers = true;
      this.MasterServerQueryLimit = 500;
    }

    #region AssignFrom()
    public void AssignFrom(IViewModel opt)
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

      var vm = opt as TabViewModel;
      if (vm == null)
        return;

      this.Source = vm.Source;
      this.serverSource = vm.serverSource;
      this.servers = vm.servers;
      this.gameExtension = vm.gameExtension;
      this.GridFilter = vm.GridFilter;
      this.ServerGridLayout = vm.ServerGridLayout;
    }
    #endregion

    #region LoadFromIni()
    public void LoadFromIni(IniFile.Section ini, GameExtensionPool pool)
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
      this.MasterServerQueryLimit = ini.GetInt("MasterServerQueryLimit", 500);
      this.GridFilter = ini.GetString("GridFilter");

      var layout = ini.GetString("GridLayout");
      if (!string.IsNullOrEmpty(layout))
        this.ServerGridLayout = new MemoryStream(Convert.FromBase64String(layout));

      this.gameExtension = pool.Get((Game) this.InitialGameID);

      if (this.Source == SourceType.CustomList)
      {
        this.servers = new List<ServerRow>();
        foreach (var server in ini.GetString("Servers").Split('\n', ' '))
        {
          var s = server.Trim();
          if (s == "") continue;
          this.servers.Add(new ServerRow(Ip4Utils.ParseEndpoint(s), this.gameExtension));
        }
      }
    }
    #endregion

    #region WriteToIni()
    public void WriteToIni(StringBuilder ini)
    {
      ini.Append("Type=").Append((int) this.Source).AppendLine();
      ini.Append("MasterServer=").AppendLine(this.MasterServer);
      ini.Append("InitialGameID=").Append(this.InitialGameID).AppendLine();
      ini.Append("FilterMod=").AppendLine(this.FilterMod);
      ini.Append("FilterMap=").AppendLine(this.FilterMap);
      ini.Append("TagsInclude=").AppendLine(this.TagsInclude);
      ini.Append("TagsExclude=").AppendLine(this.TagsExclude);
      ini.Append("GetEmptyServers=").AppendLine(this.GetEmptyServers ? "1" : "0");
      ini.Append("GetFullServers=").AppendLine(this.GetFullServers ? "1" : "0");
      ini.Append("MasterServerQueryLimit=").Append(this.MasterServerQueryLimit).AppendLine();
      ini.Append("GridFilter=").AppendLine(this.GridFilter);
      if (this.ServerGridLayout != null)
        ini.Append("GridLayout=").AppendLine(Convert.ToBase64String(this.ServerGridLayout.GetBuffer(), 0, (int) this.ServerGridLayout.Length));
      if (this.Source == SourceType.CustomList)
      {
        ini.Append("Servers=");
        foreach (var row in this.servers)
          ini.Append("\\\n ").Append(row.EndPoint);
        ini.AppendLine();
      }
    }
    #endregion
  }
}
