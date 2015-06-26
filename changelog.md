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