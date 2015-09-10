using System.Collections.Generic;
using System.Text;
using ChanSort.Api;

namespace ServerBrowser
{
  class TabViewModel : IViewModel
  {
    internal List<ServerRow> servers;
    internal ServerRow lastSelectedServer;
    internal ServerRow currentServer;
    internal IServerSource serverSource;
    internal GameExtension gameExtension;

    public string MasterServer { get; set; }
    public int InitialGameID { get; set; }
    public string FilterMod { get; set; }
    public string FilterMap { get; set; }
    public string TagsInclude { get; set; }
    public string TagsExclude { get; set; }
    public bool GetEmptyServers { get; set; }
    public bool GetFullServers { get; set; }
    public int MasterServerQueryLimit { get; set; }

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
    }
    #endregion

    #region LoadFromIni()
    public void LoadFromIni(IniFile.Section ini)
    {
      this.MasterServer = ini.GetString("MasterServer") ?? "hl2master.steampowered.com:27011";
      this.InitialGameID = ini.GetInt("InitialGameID");
      this.FilterMod = ini.GetString("FilterMod");
      this.FilterMap = ini.GetString("FilterMap");
      this.TagsInclude = ini.GetString("TagsInclude");
      this.TagsExclude = ini.GetString("TagsExclude");
      this.GetEmptyServers = ini.GetBool("GetEmptyServers", true);
      this.GetFullServers = ini.GetBool("GetFullServers", true);
      this.MasterServerQueryLimit = ini.GetInt("MasterServerQueryLimit", 500);
    }
    #endregion

    public void WriteToIni(StringBuilder ini)
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
  }
}
