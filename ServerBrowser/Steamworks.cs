using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using static DevExpress.Utils.Diagnostics.GUIResources;

namespace ServerBrowser
{
  public class Steamworks : IDisposable
  {
#if X64
    private const string SteamApiDll = "steam_api64.dll";
#else
    private const string SteamApiDll = "steam_api.dll";
#endif

    #region DllImport

    [DllImport(SteamApiDll, CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
    private extern static bool SteamAPI_Init();

    //[DllImport(SteamApiDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //private extern static IntPtr SteamClient();

    [DllImport(SteamApiDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private extern static IntPtr SteamUser();
    [DllImport(SteamApiDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern ulong SteamAPI_ISteamUser_GetSteamID(IntPtr instancePtr);

    [DllImport(SteamApiDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private extern static IntPtr SteamFriends();

    [DllImport(SteamApiDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private extern static void SteamAPI_ISteamFriends_SetPersonaName(IntPtr handle, byte[] utf8name);

    [DllImport(SteamApiDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private extern static bool SteamAPI_Shutdown();



    // /*
    [DllImport(SteamApiDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern IntPtr SteamAPI_ISteamClient_CreateSteamPipe(IntPtr hInstance);

    [DllImport(SteamApiDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern IntPtr SteamAPI_ISteamClient_BReleaseSteamPipe(IntPtr hInstance, IntPtr hSteamPipe);

    [DllImport(SteamApiDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern int SteamAPI_ISteamFriends_GetFriendPersonaState(IntPtr instancePtr, ulong steamIDFriend);


    [DllImport(SteamApiDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern bool SteamAPI_ISteamFriends_RequestUserInformation(IntPtr instancePtr, ulong steamIDUser, bool bRequireNameOnly);

    [DllImport(SteamApiDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern void SteamAPI_ISteamFriends_RequestFriendRichPresence(IntPtr instancePtr, ulong steamIDFriend);

    [DllImport(SteamApiDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern int SteamAPI_ISteamFriends_GetFriendRichPresenceKeyCount(IntPtr instancePtr, ulong steamIDFriend);

    [DllImport(SteamApiDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern IntPtr SteamAPI_ISteamFriends_GetFriendRichPresenceKeyByIndex(IntPtr instancePtr, ulong streamIDFriend, int iKey);

    [DllImport(SteamApiDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern IntPtr SteamAPI_ISteamFriends_GetFriendRichPresence(IntPtr instancePtr, ulong steamIDFriend, [MarshalAs(UnmanagedType.LPStr)] string pchKey);

    [DllImport(SteamApiDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern void SteamAPI_ISteamFriends_ClearRichPresence(IntPtr instancePtr);

    [DllImport(SteamApiDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern bool SteamAPI_ISteamFriends_SetRichPresence(IntPtr instancePtr, [MarshalAs(UnmanagedType.LPStr)] string pchKey, [MarshalAs(UnmanagedType.LPStr)] string pchValue);
    // */

    [DllImport(SteamApiDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern bool SteamAPI_ISteamFriends_GetFriendGamePlayed(IntPtr instancePtr, ulong steamIDFriend, ref FriendGameInfo_t pFriendGameInfo);


    #endregion

    private bool initialized;

    // using the "NVIDIA.SteamLauncher" app-id, which is under the "Config" category and won't change the status in the steam friend list (unlike 1007 = steamworks SDK [Tool])
    public int AppID { get; set; } = 236600;

    public bool Init()
    {
      try
      {
        if (initialized)
        {
          // sanity check that it still works. steam client could have been terminated/restarted
          if (SteamUser() != IntPtr.Zero)
            return true;
          SteamAPI_Shutdown();
        }

        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\steam_appid.txt";
        File.WriteAllText(path, AppID.ToString());
        initialized = SteamAPI_Init();

#if false
        var handle = SteamFriends();
        ulong steamid = 76561198336882885; // ModratH0r
        //ulong steamid = 76561198137367816; // PredatH0r
        SteamAPI_ISteamFriends_RequestFriendRichPresence(handle, steamid);

        Thread.Sleep(1000);

        int c = SteamAPI_ISteamFriends_GetFriendRichPresenceKeyCount(handle, steamid);
        IntPtr ptr;
        for (int i = 0; i < c; i++)
        {
          ptr = SteamAPI_ISteamFriends_GetFriendRichPresenceKeyByIndex(handle, steamid, i);
          string key = Marshal.PtrToStringAnsi(ptr);
          ptr = SteamAPI_ISteamFriends_GetFriendRichPresence(handle, steamid, key);
          string value = Marshal.PtrToStringAnsi(ptr);
          Console.WriteLine($"{key}={value}");
        }

        SteamAPI_ISteamFriends_SetRichPresence(handle, "foo", "bar");
#endif
        return initialized;
      }
      catch
      {
      }
      return false;
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
      if (handle == IntPtr.Zero) return;
      var cName = Encoding.UTF8.GetBytes(name + "\0");
      SteamAPI_ISteamFriends_SetPersonaName(handle, cName);
    }

    public ulong GetUserID()
    {
      var handle = SteamUser();
      return handle == IntPtr.Zero ? 0 : SteamAPI_ISteamUser_GetSteamID(handle);
    }

    public bool IsInGame()
    {
      var myId = GetUserID();
      if (myId == 0) return false;
      var handle = SteamFriends();
      if (handle == IntPtr.Zero) return false;
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
