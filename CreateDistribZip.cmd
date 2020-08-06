@echo off
cd /d %~dp0
set cwd=%cd%
set curdate=%date:~6,4%-%date:~3,2%-%date:~0,2%
set target=%cd%\SteamServerBrowser
mkdir "%target%" 2>nul
del /s /q "%target%\*"

cd "%cwd%\ServerBrowser\bin\x86\Release"
copy ServerBrowser.exe "%target%"
copy QueryMaster.dll "%target%"
copy Ionic.BZip2.dll "%target%"
copy steam_api.dll "%target%"

rem del "DevExpress*Rich*"
rem del "DevExpress*Office*"
del "DevExpress*Spark*"
del "DevExpress*Tree*"
copy "DevExpress*.dll" "%target%"
del "%target%\*BonusSkins*"
cd "%target%"
call :CodeSigning

cd "%cwd%"
copy *.md "%target%"

del "%target%.zip" >nul
"c:\program files\7-Zip\7z.exe" a -tzip "%target%.zip" SteamServerBrowser

cd "%cwd%"
pause
goto :eof

@echo off

:CodeSigning
rem -----------------------------
rem If you want to digitally sign the generated .exe and .dll files, 
rem you need to have your code signing certificate installed in the Windows certificate storage
rem -----------------------------
set signtool="C:\Program Files (x86)\Windows Kits\10\bin\10.0.18362.0\x86\signtool.exe"
set files=ServerBrowser.exe QueryMaster.dll
if not exist %signtool% (
  echo %signtool% not found
  pause
  exit /b 1
)
%signtool% sign /a /t "http://timestamp.digicert.com" %files%
goto :eof