2.2
---
- moved the more important options to the left
- Quake Live: removing "^1" color codes from player names

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