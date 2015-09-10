namespace ServerBrowser
{
  public interface ISettings
  {
    bool ShowOptions { get; set; }
    string Skin { get; set; }
    int ShowAddressMode { get; set; }
    bool RefreshSelected { get; set; }
    int RefreshInterval { get; set; }
    bool AutoUpdateList { get; set; }
    bool AutoUpdateInfo { get; set; }
    string MasterServerList { get; set; }
    bool KeepFavServersOnTop { get; set; }

    string FavServers { get; set; }
    int ToxikkConsoleKey { get; set; }
  }

  public interface IViewModel
  {
    string MasterServer { get; set; }
    int InitialGameID { get; set; }
    string FilterMod { get; set; }
    string FilterMap { get; set; }
    string TagsInclude { get; set; }
    string TagsExclude { get; set; }
    bool GetEmptyServers { get; set; }
    bool GetFullServers { get; set; }
    int MasterServerQueryLimit { get; set; }
  }
}