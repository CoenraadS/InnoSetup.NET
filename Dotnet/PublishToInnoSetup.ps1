# This script will publish this project into the appropiate InnoSetup locations
<# .SYNOPSIS #>
param(
    [Parameter(Mandatory=$true,HelpMessage="Debug | Release")][string]$Configuration
) 

$ErrorActionPreference = "Stop"
$prevPwd = $PWD; Set-Location -ErrorAction Stop -LiteralPath $PSScriptRoot

# Resolve-Path only works for directories that already exist, use this instead
function Get-FullPath {
    param (
        [string]$path
    )
    return $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath($path)
}

try {
    # Output location of the file list
    $absFileListPath = Get-FullPath(".\..\Files\Dotnet.iss")

    # Output location of the files
    $relativeOutputDir = "Files\Dotnet\"
    $absOutputDir = Get-FullPath(".\..\$relativeOutputDir")

    # Ensure clean folder for release
    if (Test-Path $absOutputDir) 
    {
        Remove-Item $absOutputDir -Recurse
    }

    if (Test-Path $absFileListPath) 
    {
        Remove-Item $absFileListPath
    }

    ./run-msbuild.bat dotnet.sln -t:Restore
    ./run-msbuild.bat dotnet.sln /p:OutputPath=$absOutputDir /p:Configuration=$Configuration

    if ($LASTEXITCODE -ne 0)
    {
        throw "MSBuild failed"
    }

    Add-Content $absFileListPath @"
// This file was generated from /Dotnet/PublishToInnoSetup.ps1
// Modifications will be lost
[Files]
Source: ${relativeOutputDir}InnoSetup.Bindings.dll; Flags: dontcopy
Source: ${relativeOutputDir}InnoSetup.Logic.dll; Flags: dontcopy
"@
    
}finally {
  # Restore the previous location.
  $prevPwd | Set-Location
}