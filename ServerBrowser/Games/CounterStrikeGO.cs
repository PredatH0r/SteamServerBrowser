using QueryMaster;

namespace ServerBrowser
{
  public class CounterStrikeGO : GameExtension
  {
    public CounterStrikeGO()
    {
      this.supportsRulesQuery = false;
      this.BotsIncludedInPlayerCount = false;
      this.BotsIncludedInPlayerList = true;
    }

    public override void CustomizeServerFilter(IpFilter filter)
    {
      // Valve's matchmaking servers don't respond to A2S_PLAYER queries, can't be joined from outside a lobby 
      // and the Valve network throttles clients to 100 requests per minute across all their servers and there are thousands of them.
      // They can be identified by the tag "valve_ds", which we use to filter them out on the server side.

      // skip over user's filter and append our own
      while (filter.Nor != null)
        filter = filter.Nor;

      filter.Nor = new IpFilter();
      filter.Nor.Sv_Tags = "valve_ds";
    }

    public override bool SupportsPlayersQuery(ServerRow server)
    {
      // in case the matchmaking servers weren't filtered out on the server side, we also have a client-side check here
      if (server?.ServerInfo?.Extra.Keywords != null && server.ServerInfo.Extra.Keywords.Contains("valve_ds"))
        return false;

      return true;
    }
  }
}
