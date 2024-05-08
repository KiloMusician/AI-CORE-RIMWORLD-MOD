# GatherProjectInfo.ps1 - Script to gather information about the AI-CORE-RIMWORLD-MOD project

# Define the root path for the project
$projectRoot = "C:\RIMWORLD MODDING\AI-CORE-RIMWORLD-MOD"

# Print the project structure overview
Write-Host "Project Directory Structure:" -ForegroundColor Cyan
Get-ChildItem -Path $projectRoot -Recurse -Directory | ForEach-Object { Write-Host "Directory: $($_.FullName)" }

# Check for critical configuration files
$configFiles = @("settings.json", "about.xml", "AICompanionCore.xml", "ModProject.csproj", "ModProject.sln")
Write-Host "`nChecking for critical configuration files:" -ForegroundColor Cyan
$configFiles | ForEach-Object {
    $filePath = Join-Path -Path $projectRoot -ChildPath $_
    if (Test-Path $filePath) {
        Write-Host "Found: $filePath" -ForegroundColor Green
    } else {
        Write-Host "Missing: $filePath" -ForegroundColor Red
    }
}

# Output the latest log files
$logDirectory = Join-Path -Path $projectRoot -ChildPath "Logs"
if (Test-Path $logDirectory) {
    $logFiles = Get-ChildItem -Path $logDirectory -Filter "*.log"
    Write-Host "`nLatest Log Entries:" -ForegroundColor Cyan
    if ($logFiles) {
        $logFiles | ForEach-Object {
            Write-Host "`nLog File: $($_.FullName)" -ForegroundColor Yellow
            Get-Content $_.FullName -Tail 10
        }
    } else {
        Write-Host "No log files found." -ForegroundColor Yellow
    }
} else {
    Write-Host "`nLog directory not found." -ForegroundColor Red
}

# List installed PowerShell modules related to the project
$requiredModules = @("PSScriptAnalyzer")
Write-Host "`nInstalled PowerShell Modules relevant to the project:" -ForegroundColor Cyan
$requiredModules | ForEach-Object {
    if (Get-Module -ListAvailable -Name $_) {
        Write-Host "$_ is installed." -ForegroundColor Green
    } else {
        Write-Host "$_ is not installed." -ForegroundColor Red
    }
}

# Optionally, output this information to a file
$outputFile = Join-Path -Path $projectRoot -ChildPath "project_info_output.txt"
Write-Host "`nWriting output to file: $outputFile" -ForegroundColor Cyan
Start-Transcript -Path $outputFile -Append
Get-ChildItem -Path $projectRoot -Recurse -Directory | ForEach-Object { Write-Host "Directory: $($_.FullName)" }
$configFiles | ForEach-Object {
    $filePath = Join-Path -Path $projectRoot -ChildPath $_
    if (Test-Path $filePath) {
        Write-Host "Found: $filePath"
    } else {
        Write-Host "Missing: $filePath"
    }
}
if (Test-Path $logDirectory) {
    if ($logFiles) {
        $logFiles | ForEach-Object {
            Write-Host "`nLog File: $($_.FullName)"
            Get-Content $_.FullName -Tail 10
        }
    } else {
        Write-Host "No log files found."
    }
} else {
    Write-Host "`nLog directory not found."
}
$requiredModules | ForEach-Object {
    if (Get-Module -ListAvailable -Name $_) {
        Write-Host "$_ is installed."
    } else {
        Write-Host "$_ is not installed."
    }
}
Stop-Transcript
