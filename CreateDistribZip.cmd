@echo off
cd /d %~dp0
set cwd=%cd%
set curdate=%date:~6,4%-%date:~3,2%-%date:~0,2%
set target=%cd%\SteamServerBrowser_%curdate%
mkdir "%target%" 2>nul
del /s /q "%target%\*"
cd ServerBrowser
call CodeSigning.cmd
cd .\bin\debug
copy ServerBrowser.exe* "%target%"
copy QueryMaster.dll "%target%"
copy Ionic.BZip2.dll "%target%"
copy DevExpress*.dll "%target%"
del "%target%\*BonusSkins*"
cd %cwd%
copy *.md "%target%"

del "%target%.zip"
"c:\program files\7-Zip\7z.exe" a -tzip "%target%.zip" SteamServerBrowser_%curdate%

cd %cwd%
pause
