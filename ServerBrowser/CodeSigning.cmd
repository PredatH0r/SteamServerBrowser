@echo off

rem -----------------------------
rem If you want to sigitally sign the generated .exe and .dll files, 
retm you need to have your code signing certificate installed in the Windows certificate storage
rem -----------------------------

set olddir=%cd%
rem disabled for development
rem goto:eof

set signtool="C:\Program Files\Microsoft SDKs\Windows\v6.0A\Bin\signtool.exe"
cd /d "%~dp0\bin\Release"
set files=ServerBrowser.exe QueryMaster.dll
%signtool% sign /a /t "http://timestamp.comodoca.com/authenticode" %files%

cd /d %olddir%