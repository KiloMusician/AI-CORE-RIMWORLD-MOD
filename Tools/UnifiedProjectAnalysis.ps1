# UnifiedProjectAnalysis.ps1 - Script to manage project analysis and information gathering

# Define the root path for the project
$projectRoot = "C:\RIMWORLD MODDING\AI-CORE-RIMWORLD-MOD"
$toolsDir = Join-Path -Path $projectRoot -ChildPath "Tools"
$toolPath = Join-Path -Path $toolsDir -ChildPath "StaticAnalysisTool.exe"
$logsPath = Join-Path -Path $projectRoot -ChildPath "Logs"
$analysisResultsPath = Join-Path -Path $logsPath -ChildPath "analysis_results.log"
$projectInfoOutputPath = Join-Path -Path $projectRoot -ChildPath "project_info_output.txt"

# Ensure necessary directories exist
if (-Not (Test-Path -Path $logsPath)) {
    New-Item -Path $logsPath -ItemType Directory | Out-Null
    Write-Host "Created Logs directory at $logsPath" -ForegroundColor Green
}

# Ensure PSScriptAnalyzer module is installed
if (-Not (Get-Module -ListAvailable -Name PSScriptAnalyzer)) {
    Write-Host "Installing PSScriptAnalyzer module..." -ForegroundColor Yellow
    Install-Module -Name PSScriptAnalyzer -Force | Out-Null
    Write-Host "PSScriptAnalyzer module installed." -ForegroundColor Green
}

# Gather Project Structure and Critical File Information
Write-Host "Project Directory Structure:" -ForegroundColor Cyan
Get-ChildItem -Path $projectRoot -Recurse -Directory | ForEach-Object { Write-Host "Directory: $($_.FullName)" }
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

# Log Project Information to File
Start-Transcript -Path $projectInfoOutputPath
Get-ChildItem -Path $projectRoot -Recurse -Directory | ForEach-Object { Write-Host "Directory: $($_.FullName)" }
Stop-Transcript

# Run Static Analysis Tool
if (-Not (Test-Path -Path $toolPath)) {
    Write-Host "Error: Static analysis tool not found at $toolPath" -ForegroundColor Red
    exit
}
Write-Host "Running Static Analysis Tool..." -ForegroundColor Yellow
& $toolPath "$projectRoot\Source" | Out-File -FilePath $analysisResultsPath
Write-Host "Static analysis completed. Output logged to $analysisResultsPath" -ForegroundColor Green

# Optionally open the log file with notepad.exe
Start-Process "notepad.exe" -ArgumentList $analysisResultsPath

Write-Host "Analysis complete. Check the logs for details." -ForegroundColor Green
