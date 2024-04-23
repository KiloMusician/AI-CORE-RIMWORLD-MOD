# AutoDependencyInstaller.ps1
# Ensure NuGet CLI is available
$nugetPath = "C:\RimWorld_MODDING\AI-CORE-RIMWORLD-MOD\Tools\nuget.exe"

if (-Not (Test-Path $nugetPath)) {
    Write-Host "NuGet CLI not found. Downloading..."
    $url = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
    Invoke-WebRequest -Uri $url -OutFile $nugetPath
    # Make the nuget.exe file executable
    & cmd.exe /c "chmod +x $nugetPath"
}

# Function to check and install a NuGet package if it's missing
Function Install-NuGetPackage {
    param (
        [string]$packageName,
        [string]$version,
        [string]$projectPath
    )

    $packageExists = & $nugetPath list $packageName -Source "https://api.nuget.org/v3/index.json" | Select-String "$packageName $version"

    if (-Not $packageExists) {
        Write-Host "Package $packageName not found, installing..."
        & $nugetPath install $packageName -Version $version -OutputDirectory "$projectPath\packages"
    }
    else {
        Write-Host "Package $packageName already installed."
    }
}

# Example usage:
# Specify your project path
$projectPath = "C:\RimWorld_MODDING\AI-CORE-RIMWORLD-MOD"

# Call the function to check and install packages
Install-NuGetPackage -packageName "Newtonsoft.Json" -version "12.0.3" -projectPath $projectPath
Install-NuGetPackage -packageName "Harmony" -version "2.0.4" -projectPath $projectPath
Install-NuGetPackage -packageName "HugsLib" -version "7.2.1" -projectPath $projectPath
