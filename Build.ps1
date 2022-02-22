$ErrorActionPreference = "Stop"

$ISCC = "C:\Program Files (x86)\Inno Setup 6\ISCC.exe"

if (-Not (Test-Path -Path "$ISCC" -PathType Leaf))
{
    throw "Path to Inno Setup not found"
}

"Building dotnet"
./Dotnet/PublishToInnoSetup.ps1 -Configuration Debug

"Building Installer"
cmd /c $ISCC Setup.iss