@echo off

setlocal

set VSWHERE="%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -latest -prerelease -products * -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe

@REM VSWhere command only returns one line as output, but cmd requires a loop to capture it..
for /f "delims=" %%i in ('%VSWHERE%') do set MSBUILD="%%i"

echo Using MSBuild: %MSBUILD%
%MSBUILD% %*

endlocal

EXIT /B %ERRORLEVEL%