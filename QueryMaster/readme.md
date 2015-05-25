QueryMaster
===

This is a modified version of the QueryMaster library found at https://querymaster.codeplex.com/

Modifications:
---
- bugfixes (incorrect parsing of ServerInfo.ExtraData)
- handling of network errors (retries on timeouts)
- handling of steam master server throttling (Steam doesn't allow more than 30 replies per minute and client machine)
- improved multi-threading (calling (UI) thread doesn't block even when the ThreadPool is full)
- resource cleanup (UDP sockets)

Breaking changes:
---
- MasterServer no longer implements IDisposable. Resource allocation and cleanup is automatically handled within GetAddresses().