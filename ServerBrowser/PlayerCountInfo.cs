using System;
using System.Linq;
using System.Text;

namespace ServerBrowser
{
  /// <summary>
  /// This class is used for the data in the "Players" column and allows 
  /// special sorting based on real players, bots and max players
  /// </summary>
  public class PlayerCountInfo : IComparable
  {
    private readonly ServerRow row;

    public int? RealPlayers { get; private set; }
    public int? Bots { get; private set; }
    public int? TotalPlayers => RealPlayers + Bots;
    public int? MaxPlayers { get; private set; }
    public int? MaxClients { get; private set; }
    public int? PrivateClients { get; private set; }

    public PlayerCountInfo(ServerRow row)
    {
      this.row = row;
      this.Update();
    }

    public void Update()
    {
      if (row.ServerInfo == null)
        RealPlayers = Bots = MaxPlayers = MaxClients = PrivateClients = null;
      else
      {
        Bots = row.GameExtension.GetBotCount(row);
        MaxPlayers = row.GameExtension.GetMaxPlayers(row);
        MaxClients = row.GameExtension.GetMaxClients(row);
        RealPlayers = row.ServerInfo.Players;
        PrivateClients = row.GameExtension.GetPrivateClients(row);

        // some games count bots as players, others don't
        if (RealPlayers >= Bots)
        {
          if (RealPlayers + Bots > MaxPlayers && RealPlayers - Bots <= MaxPlayers)
            row.GameExtension.BotsIncludedInPlayerCount = true; // heuristic
          if (row.GameExtension.BotsIncludedInPlayerCount)
            RealPlayers -= Bots;
        }
      }

      if (row.Players != null)
      {
        // CS:GO with default server settings returns a single fake "Max Players" entry with score=MaxPlayers and time=server up-time
        if (row.Players.Count == 1 && row.Players[0].Name == "Max Players")
        {          
        }
        else if (row.Players.Count > 0) // some games always return an empty list
        {
          int? count = row.Players.Count(p => !string.IsNullOrEmpty(p.Name) && row.GameExtension.IsValidPlayer(row, p));

          // some games (CS:GO, TF2, QuakeLive) return bots in the player list
          if (count >= Bots)
          {
            if (count > RealPlayers && count - Bots <= RealPlayers)
              row.GameExtension.BotsIncludedInPlayerList = true; // heuristic
            if (row.GameExtension.BotsIncludedInPlayerList)
              count -= Bots;
          }
          RealPlayers = count;
        }
      }
    }

    public int CompareTo(object b)
    {
      PlayerCountInfo other = (PlayerCountInfo)b;
      if (this.row.ServerInfo == null)
        return other.row.ServerInfo == null ? 0 : -1;
      if (other.row.ServerInfo == null)
        return +1;
      if (this.RealPlayers < other.RealPlayers) return -1;
      if (this.RealPlayers > other.RealPlayers) return +1;
      if (this.Bots < other.Bots) return -1;
      if (this.Bots > other.Bots) return +1;
      //if (this.MaxPlayers < other.MaxPlayers) return -1;
      //if (this.MaxPlayers > other.MaxPlayers) return +1;
      return 0;
    }

    public override string ToString()
    {
      if (row.ServerInfo == null && row.Players == null)
        return "";
      var sb = new StringBuilder();
      sb.Append(this.RealPlayers);
      if (this.Bots > 0)
        sb.Append('+').Append(this.Bots);

      if (this.MaxPlayers > 0)
      {
        var privateInUse = Math.Max(0, (this.TotalPlayers - (this.MaxClients - this.PrivateClients)) ?? 0);
        int max = Math.Min(this.MaxPlayers ?? 0, (this.MaxClients ?? 0) - (this.PrivateClients ?? 0) + privateInUse);
        sb.Append(" / ").Append(max);
        if (this.MaxClients - this.PrivateClients + privateInUse > max)
          sb.Append('+').Append(this.MaxClients - this.PrivateClients + privateInUse - max);
      }
      return sb.ToString();
    }
  }
}
