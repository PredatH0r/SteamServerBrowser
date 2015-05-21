using System;
using System.Linq;

namespace ServerBrowser
{
  /// <summary>
  /// This class is used for the data in the "Players" column and allows 
  /// special sorting based on real players, bots and max players
  /// </summary>
  public class PlayerCountInfo : IComparable
  {
    private readonly ServerRow row;

    public int RealPlayers { get; private set; }
    public int Bots { get; private set; }
    public int MaxPlayers { get; private set; }

    public PlayerCountInfo(ServerRow row)
    {
      this.row = row;
      this.Update();
    }

    public void Update()
    {
      if (row.ServerInfo == null)
        return;
      Bots = row.ServerInfo.Bots;
      MaxPlayers = row.ServerInfo.MaxPlayers;
      RealPlayers = row.Players == null ? row.ServerInfo.Players : row.Players.Count(p => !string.IsNullOrEmpty(p.Name));
    }

    public int CompareTo(object b)
    {
      PlayerCountInfo other = (PlayerCountInfo)b;
      if (this.RealPlayers < other.RealPlayers) return -1;
      if (this.RealPlayers > other.RealPlayers) return +1;
      if (this.Bots < other.Bots) return -1;
      if (this.Bots > other.Bots) return +1;
      if (this.MaxPlayers < other.MaxPlayers) return -1;
      if (this.MaxPlayers > other.MaxPlayers) return +1;
      return 0;
    }

    public override string ToString()
    {
      return this.RealPlayers + (this.Bots > 0 ? "+" + this.Bots : "") + " / " + this.MaxPlayers;
    }
  }
}
