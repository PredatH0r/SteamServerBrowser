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