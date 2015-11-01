@echo off
cd /d %~dp0
set cwd=%cd%
set curdate=%date:~6,4%-%date:~3,2%-%date:~0,2%
set target=%cd%\SteamServerBrowser_%curdate%
mkdir "%target%" 2>nul
del /s /q "%target%\*"

cd "%cwd%\ServerBrowser\bin\x86\debug"
copy ServerBrowser.exe "%target%"
copy QueryMaster.dll "%target%"
copy Ionic.BZip2.dll "%target%"
copy steam_api.dll "%target%"

del "DevExpress*Office*"
rem del "DevExpress*Rich*"
del "DevExpress*Spark*"
del "DevExpress*Tree*"
copy "DevExpress*.dll" "%target%"
del "%target%\*BonusSkins*"
cd "%target%"
call :CodeSigning

cd "%cwd%"
copy *.md "%target%"

del "%target%.zip" >nul
"c:\program files\7-Zip\7z.exe" a -tzip "%target%.zip" SteamServerBrowser_%curdate%

cd "%cwd%"
pause
goto :eof

@echo off

:CodeSigning
rem -----------------------------
rem If you want to digitally sign the generated .exe and .dll files, 
rem you need to have your code signing certificate installed in the Windows certificate storage
rem -----------------------------
set signtool="C:\Program Files\Microsoft SDKs\Windows\v6.0A\Bin\signtool.exe"
set files=ServerBrowser.exe QueryMaster.dll
%signtool% sign /a /t "http://timestamp.comodoca.com/authenticode" %files%
goto :eof