using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace ServerBrowser
{
  class Steamworks : IDisposable
  {
    #region DllImport

    [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
    private extern static bool SteamAPI_Init();

    //[DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //private extern static IntPtr SteamClient();

    [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private extern static IntPtr SteamUser();

    [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private extern static IntPtr SteamFriends();

    [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private extern static void SteamAPI_ISteamFriends_SetPersonaName(IntPtr handle, byte[] utf8name);

    [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private extern static bool SteamAPI_Shutdown();


    [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern ulong SteamAPI_ISteamUser_GetSteamID(IntPtr instancePtr);
    
    /*
    [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern IntPtr SteamAPI_ISteamClient_CreateSteamPipe(IntPtr hInstance);

    [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern IntPtr SteamAPI_ISteamClient_BReleaseSteamPipe(IntPtr hInstance, IntPtr hSteamPipe);

    [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern int SteamAPI_ISteamFriends_GetFriendPersonaState(IntPtr instancePtr, ulong steamIDFriend);


    [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern bool SteamAPI_ISteamFriends_RequestUserInformation(IntPtr instancePtr, ulong steamIDUser, bool bRequireNameOnly);

    [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern void SteamAPI_ISteamFriends_RequestFriendRichPresence(IntPtr instancePtr, ulong steamIDFriend);

    [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern int SteamAPI_ISteamFriends_GetFriendRichPresenceKeyCount(IntPtr instancePtr, ulong steamIDFriend);

    [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern IntPtr SteamAPI_ISteamFriends_GetFriendRichPresenceKeyByIndex(IntPtr instancePtr, ulong streamIDFriend, int iKey);

    [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern IntPtr SteamAPI_ISteamFriends_GetFriendRichPresence(IntPtr instancePtr, ulong steamIDFriend, IntPtr pchKey);
    */

    [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern bool SteamAPI_ISteamFriends_GetFriendGamePlayed(IntPtr instancePtr, ulong steamIDFriend, ref FriendGameInfo_t pFriendGameInfo);


    #endregion

    private bool initialized;

    // using the QL Dedicated Linux Server app-id so it won't block the QL client (282440) from starting
    public int AppID { get; set; } = 349090;

    public bool Init()
    {
      var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\steam_appid.txt";
      //if (!File.Exists(path))
        File.WriteAllText(path, AppID.ToString());
      return initialized = SteamAPI_Init();
    }

    ~Steamworks()
    {
      this.Dispose(false);
    }

    public void Dispose()
    {
      GC.SuppressFinalize(this);
      this.Dispose(true);
    }

    protected void Dispose(bool disposing)
    {
      if (initialized)
        SteamAPI_Shutdown();
    }

    public void SetName(string name)
    {
      var handle = SteamFriends();
      var cName = Encoding.UTF8.GetBytes(name + "\0");
      SteamAPI_ISteamFriends_SetPersonaName(handle, cName);
    }

    public ulong GetUserID()
    {
      var handle = SteamUser();
      return SteamAPI_ISteamUser_GetSteamID(handle);
    }

    public bool IsInGame()
    {
      var myId = GetUserID();
      var handle = SteamFriends();

      FriendGameInfo_t info = new FriendGameInfo_t();
      bool playing = SteamAPI_ISteamFriends_GetFriendGamePlayed(handle, myId, ref info);
      return playing;
    }

    [StructLayout(LayoutKind.Sequential, Pack=8)]
    public struct FriendGameInfo_t
    {
      public ulong m_gameID;
      public UInt32 m_unGameIP;
      public UInt16 m_usGamePort;
      public UInt16 m_usQueryPort;
      public ulong m_steamIDLobby;
    };
  }

}
