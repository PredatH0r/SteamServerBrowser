2.61
---
- fixed support for A2S_INFO responses with a challenge

2.60
---
- supporting A2S_INFO responses with a challenge 
  Valve changing the server browser protocol: https://steamcommunity.com/discussions/forum/14/2974028351344359625/ 
- fixed: for games based on Source engine no server information was shown

2.47
---
- TOXIKK: showing user friendly rule names instead of engine internal property numbers

2.46
---
- upgraded to latest version of DevExpress user controls
- added "Copy steam://... URL to clipboard" (Ctrl+U) to server list context menu

2.45
---
- server lists now persist when switching between game tabs
- fixed issue with UDP master server queries when more servers than the set limit are returned
- fixed: Using Steam Web API as master server showed tags/keywords/description in wrong columns / fields
- Reflex Arena: game type and location can now be extracted from the tags with both comma and pipe as separator
- Reflex Arena: added server rules tab

2.44
---
- "Master Server" can now be set to "<Steam Web API>" for WebAPI XML requests, or a valid host:port
  number to send a UDP request. (Steam master servers seem to respond to UDP again)

2.43
---
- TOXIKK: added support for "Squad Survival" game type and added tooltip for "AR" (Arch Rivals)
- updated to latest DevExpress control library

2.42
---
- fixed steam master server timeout on Win 7

2.41
---
- TOXIKK: players in ArchRival game mode no longer show up as team Red
- fixed exception when hovering over a player and there is no selected server row

2.40
---
- TOXIKK: auto-detect if steam sockets are supported by the server and if not, use IP:port to join the server
- TOXIKK: show "Spec" and "Queue" status in player list

2.39
---
- fixed empty server list when "show timed out servers" was enabled
- calculate ping after server refresh

2.38
---
- using the results from the Steam WebAPI call and skip the redundant A2S_INFO query.
  With this the browser can now show information for servers behind NAT without forwarding,
  when they use SteamSockets.
- fixed wrong server list after switching tabs
- fixed Steam API integration (old appid didn't work anymore)
- fixed TOXIKK player counts (off by 1 when all real players were on team Blue)


2.37
---
- show exception message when master server list could not be retrieved
- .NET 4.0 Client Profile is now sufficient to run the program

2.36
---
- fix for TOXIKK 1.0.5: use IP:port now to connect instead of Steam-UID (public servers no longer use SteamSockets)

2.35
---
- using undocumented Steam Web API function to retrieve server list after the steam master server stopped responding to UDP requests on 2016-11-09

2.34
---
- fixed errors when hovering over timed out TOXIKK servers
- added "Skill Class" filter in Options / TOXIKK

2.33
---
- added "Ver" column for TOXIKK game version

2.32
---
- added option in TOXIKK game configuration to either connect using "open <ip:port>" or "open steam.<steamid>"
- data in server details grid can now be selected and copied

2.30
---
- TOXIKK now supports joining skill-restricted servers. Your skill class is validated by the server after you connect.

2.29
---
- switched to a different Geo-IP service

2.28
---
- added Geo-IP overrides for incorrect Geo-IP data
- upgraded UI control library
- ignoring zombie processes when searching for running game processes and windows

2.27
---
- fixed link to Steam Workshop item page

2.26
---
- QuakeLive: fixed incorrect player counts and join indicators
- QuakeLive: manually corrected a few wrong Geo-IP lookups (in different MS Azure locations)

2.25.2
---
- fixed error in player list context menu when there is no qlstats.net data available for the server

2.25.1
---
- fixed error message popup at startup
- fixed keeping rows at position when only updating single rows

2.25
---
- added control to turn ghost player filter on/off
- servers rows keep their position even when sorted data values change
- improved scrolling during server list updates
- Quake Live: improved ghost player filter using information from qlstats.net
- Quake Live: improved display of player counts using information from qlstats.net
- Quake Live: personal rating information now shows r +/- RD to be consistent with qlstats.net

2.24
---
- Quake Live: added "Score" column
- Quake Live: added "Team" and "Skill" columns to player list (using data from qlstats.net)
- Quake Live: added context menu to Player list with options for steam chat, friend request, steam profile, qlstats profile
- Quake Live: changed ghost-player time limit from 4h to 1h
- Ghost players are also removed from the player list now
- TOXIKK: remove "ghost players" from player counts
- Player list is sorted by score

2.23
---
- Quake Live: remove "ghost players" (dead connections in the player list) from player counts
- Buddy List: can't add blank or duplicate players anymore

2.22
---
- Quake Live: integrated skill rating information from qlstats.net

2.21
---
- Save/restore application window size and layout of docked panels
- Options are now a docking panel, so the right-hand-side panels can be arranged to use the whole window height

2.20
---
- Quake Live: player count and join-status now includes sv_privateClients in the calculations
- Toxikk: short game type names for AD (Area Domination) and SB (Score-the-Banshee)

2.19
---
- Quake Live: open extraQL in the background instead of bringing it to front (requires extraQL 2.7 or later)
- connect on double-click can now be disabled
- open "Connecting to ..." form no longer blocks reconnecting again

2.18
---
- added About / Version History menu item
- using Lock icon instead of a checkbox for private (password protected) servers
- Quake Live: added option to automatically start extraQL when connecting to match
- Quake Live: gametype column "GT" now has a prefixed "i" for InstaGib matches. The Instagib checkbox column was removed
- Quake Live: added columns "Lo" and "Ti" to indicate enabled loadouts and item timers
- Quake Live: removed redundant "FT" column (full teams), as this is already indicated by the traffic light
- fixed: last used skin will be loaded when the program is restarted
- fixed: "Use Default Skin" button in the skin selection dialog had no effect
- moved ServerBrowser.ini and locations.txt from %appdata%\..\Local\ServerBrowser to local directory

2.17
---
- fixed "max players" display for non-team modes (Duel, FFA, Race)
- fixed display of special "modinfo", "specinfo", and "The Ship" fields in Server Details

2.16
---
- improved performance (reduced amout of GC calls by using structs for primitve objects)
- added option to disable use of SteamAPI for in-game check (use of SteamAPI can cause massive FPS drops)
- fixed a couple issues when auto-update timer wasn't stopped/started correctly
- using Release build for steam package

2.15
---
- use long living steam API session to prevent massive FPS drop after a while

2.14
---
- fixed copy&paste shortcuts
- fixed error when a Quake Live servers has a non-integer teamsize (0.5)
- fixed servers not showing up after pasting to a manual server list
- removed option "remember column layout for each tab" (it's now always on)

2.13
---
- Min Player, Max Ping are now stored per-tab
- minor UI tweaks

2.12.1
---
- fixed crash/hang when Steam API could not be called (Steam not running or under a different user account)

2.12
---
- increased size for "Include Tags" and "Exclude Tags"
- added information text about server-side and client side filters
- fixed: when update interval was set to 1 min, servers were updated again immediately after the previous update ended
- fixed: client-side "Include Tags" and "Exclude Tags" was not saved/restored
- fixed: "Maximum Ping" filter was not saved/restored

2.11
---
- added Cancel Update button
- added client-side "Include Tags" and "Exclude Tags", which allow AND and OR conditions using comma and semicolon
- added "About" menu
- remember which columns were hidden by user
- fixed default configuration with missing master server query tabs

2.10
---
- cleaned up menu (only most important function are directly in the menu bar, other items are in the drop downs)
- cleaned up option pane (moved quick filters and alert to a separate pane)
- added option dialogs for Quake Live, Reflex and TOXIKK
- show a splash screen when connecting to a server
- bring existing game window to front of screen when connecting
- option "hide timed out servers" is now applied without setting up table filters
- fixed UI not being updated after a refresh of the current server list

2.9
---
- added option to disable auto-update while you are in-game, which is turned on by default. This should prevent performance issues while playing.
- reduced thread number for concurrent server queries from 100 to 50
- a few smaller optimizations to get a more accurate ping (but still work in progress)

2.8
---
- added traffic light indicator for vacant/full servers
- Quake Live: added columns for showing teamsize and indicating full teams (assuming no specs, for which there is no data available)
- improved display of servers which timed out (now showing all known last values)
- improved filter to hide servers with status "timeout"
- added context menu to "Server Details" to add fields as columns to server list
- custom columns from "Server Details" and "Rules" are now stored per tab (if the "remember column layout for each tab" option is selected)

2.7
---
- added Buddy list, Buddy Count per server and player search functions
- fixed jumping of rows when option "hide unresponsive servers" was enabled

2.5.2
---
- fixed occational error when loading / quitting
- fixed errors after waking the computer up from hibernation (and internet connection is not available yet)

2.5.1
---
- fixed error when adding a favorite which is already in the favorites

2.5
---
- fixed error when launching Steam Server Browser for the first time
- added option to hide unresponsive servers (activated by default)
- improved geo-ip lookup

2.4
---
- added support for official version of Steam-Exlucsive QuakeLive 

2.3
---
- added heuristic to detect whether a game counts bots as players and/or shows them in the player list

2.2
---
- moved the more important options to the left
- Quake Live: removing "^1" color codes from player names
- TOXIKK: fixed connecting to servers when Toxikk is started with "-log" parameter and has 2 windows
- TOXIKK: fixed connecting to servers when there is also a local dedicated server running
- remember server names for all favorite and static list servers
- moved all config settings to a INI file

2.1
---
- fix: servers now stay seleted after updating their data
- added controls to easily add filters for player count and/or ping
- automatically adding Ping as a (last) sort criteria to break ties

2.0
---
- added tabbed browsing with individual search and filter settings for each tab
- added support for custom server lists, which can be edited with copy&paste
- added support for a combined Favorites list
- added multi selection to server list

1.16
---
- added local Geo-IP cache for game server location information
- most text columns now use "contains" as default filter condition (instead of "starts with")
- Ping is no longer updated when changing the selected server to prevent reordering by the new value
- auto-update mode can be set to either full server list update or quick refresh
- result limit can now be changed to show more than 500 servers, at the risk of getting throttled by
  the Steam Master Server (max. 30 UDP packets per minute per client IP ... ~6930 servers)
- fix: application error when clicking on the column filter icon in the server details or rules tab

1.15
---
- Reflex: simulating keystrokes to connect to a server
  (Reflex 0.36.x currently doesn't support steam://connect/ip:port URLs)

1.14
---
- TOXIKK: added column to show mutators
- fix: stale data was shown on screen (with missing location information)
- fix: setting auto-refresh time to 0 caused 1sec refreshes and as a result throttling by Valve

1.13
---
- added support to mark servers as favorites and keep them on top of the server list
- added button for "Quick Refresh" which updates the status of all servers in the current list
- renamed "Reload Servers" to "Find Servers" which should make it clearer that it loads a new server list
- removing leading/trailing spaces from server names

1.12.2
---
- when trying to connect to a skill limited TOXIKK server, a message box informs you that this is not possible
- TOXIKK: the players column now shows the max bot count when the server is empty and bots haven't spawned yet
- fix: no longer trying to start TOXIKK a 2nd time when it's already running and the window minimized

1.12.1
---
- fix: error popup when hovering mouse over a Location cell when the geo info wasn't loaded yet
- fix: failed geo-info requests will be sent again on the next refresh
- added code (but not yet accessible) to send RCON commands to Quake Live Testing through ZeroMQ

1.12
---
- added Geo-IP lookup (API hosted by http://freegeoip.net, flag icon provided by http://www.famfamfam.com)
- improved connecting to QuakeLive Testing game servers

1.11
---
- fixed adding custom game server IP: blank value is now ignored and only IPv4 addresses of the host name are used
- added context menu to Rules table to add selected rules to the Servers grid (which allows client side filtering)

1.10
---
- added (master-)server-side filter critera (map, mod, include tags, exclude tags)
  This is needed for games that have thousands of servers
- improved notification system to look out for particular servers

1.9
---
- added support for CS:GO community servers
- code refactoring to enable lists with different games mixed together
- code refactoring to allow server lists from various sources (URLs, files, master server, ...)

1.8.2
---
- fix: remove game specific columns in Players list when switching games
- fix: detailed human/bots/total/max player counts were always visible after switching games (even if not selected)
- fix: race conditions between data model update and UI update
- fix: disabling auto-update by setting interval to 0

1.8.1
---
- added "Download Bonus Skins" link to get a bunch of additional skins
- automatically send initial UDP query packets twice when an unusual high percentage of retries was necessary in the previous refresh
- TOXIKK: added player list context menu commands: "Open Steam Chat", "Show Steam Profile", "Copy-Name" and "Copy Steam-ID"
- TOXIKK: fixed race condition that could cause blank Team, Rank and SC information in "Players" view
- code refacturing and cleanup

1.7
---
- added auto-refresh interval
- updating server list no longer clears the whole table
- added feature to set an alert when servers are found (after a refresh) which match the filter criteria (e.g. game type = CTF and human players >= 3)
- TOXIKK: showing best player's Skill Class in a "Best" column

1.6.4
---
- added context menu to server list to allow update, connect, connect as spec, copy address to clipboard
- allow game extensions to add additional columns and context menu to Players view
- show player's skill class and current team for TOXIKK
- player context menu for TOXIKK allows adding players to Steam friend list
- show TOXIKK player Rank and Skill Class in player list
- fixed NumberFormatException parsing player SC and Rank when server returns bad information
- fixed NullReferenceException when trying to connect to a server which didn't provide any data

1.5
---
- option to show game port instead of query port in address column
- added context menu to server grid
- player names and server settings can now be copied
- ability to connect to Toxikk servers as ghost/spectator
- added some tool tips to UI elements

1.4
---
- added password dialog for private servers
- improved connecting to TOXIKK matches (skip into screens, connect to servers that Steam incorrectly believes to be full)

1.3
---
- allow custom master servers
- allow adding unlisted game servers

1.2
---
- fixed response handling for the game "The Ship"
- fixed negative port numbers
- no longer showing status "timeout" when a server returns Details, but ignores Players or Rules queries.
- internal code cleanup (moved all retry logic to QueryMaster library)

1.1
---
Game can now be selected form a list of known Steam games or by using one of the favorite radio buttons.
Favorites can be reassigned by using Ctrl+Click.
The browser now also remembers the last game and selects it on its next start.
Improved responsiveness when updating large server lists or fast switching between games.

1.0
---
Generic server browser with extensions for selected games (Reflex, Toxikk)