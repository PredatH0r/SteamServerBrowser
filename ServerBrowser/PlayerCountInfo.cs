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
      if (row.ServerInfo != null)
      {
        Bots = row.ServerInfo.Bots;
        MaxPlayers = row.ServerInfo.MaxPlayers;
        RealPlayers = row.ServerInfo.Players;
      }

      if (row.Players != null)
        RealPlayers = row.Players.Count(p => !string.IsNullOrEmpty(p.Name));
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
      if (row.ServerInfo == null && row.Players == null)
        return "";
      var sb = new StringBuilder();
      sb.Append(this.RealPlayers);
      if (this.Bots > 0)
        sb.Append('+').Append(this.Bots);
      if (this.MaxPlayers > 0)
        sb.Append(" / ").Append(this.MaxPlayers);
      return sb.ToString();
    }
  }
}
