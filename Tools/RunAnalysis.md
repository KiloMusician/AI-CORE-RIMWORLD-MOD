# RunAnalysis.ps1

# Check if the PSScriptAnalyzer module is installed, and if not, install it
if (-not (Get-Module -ListAvailable -Name PSScriptAnalyzer)) {
    Write-Host "Installing PSScriptAnalyzer module..." -ForegroundColor Yellow
    Install-Module -Name PSScriptAnalyzer -Force | Out-Null
    Write-Host "PSScriptAnalyzer module installed." -ForegroundColor Green
}

# Define paths
$projectRoot = "C:\RimWorld_MODDING\AI-CORE-RIMWORLD-MOD"
$toolPath = Join-Path -Path $projectRoot -ChildPath "Tools\StaticAnalysisTool.exe"
$psiScriptPath = Join-Path -Path $projectRoot -ChildPath "RimWorldModAnalysis.ps1"
$logsPath = Join-Path -Path $projectRoot -ChildPath "Logs"


# Ensure the Logs directory exists
if (-Not (Test-Path -Path $logsPath)) {
    New-Item -Path $logsPath -ItemType Directory | Out-Null
    Write-Host "Created Logs directory at $logsPath" -ForegroundColor Green
}

# Check if $projectRoot directory exists
if (-Not (Test-Path -Path $projectRoot)) {
    Write-Host "Error: Project root directory not found at $projectRoot" -ForegroundColor Red
    exit
}

# Check if StaticAnalysisTool.exe exists
if (-Not (Test-Path -Path $toolPath)) {
    Write-Host "Error: StaticAnalysisTool.exe not found at $toolPath" -ForegroundColor Red
    exit
}

# Run StaticAnalysisTool.exe and log the output
Write-Host "Running Static Analysis Tool..." -ForegroundColor Yellow

# Check if the Source directory exists, and if not, create it
$sourceDirectory = Join-Path -Path $projectRoot -ChildPath "Source"
if (-Not (Test-Path -Path $sourceDirectory)) {
    New-Item -Path $sourceDirectory -ItemType Directory | Out-Null
    Write-Host "Created Source directory at $sourceDirectory" -ForegroundColor Green
}

& $toolPath "$sourceDirectory" | Out-File -FilePath "$logsPath\tool_output.log"
Write-Host "Static analysis completed. Output logged to $logsPath\tool_output.log" -ForegroundColor Green

# Check if RimWorldModAnalysis.ps1 exists
Import-Module Microsoft.PowerShell.Management
if (-Not (Test-Path -Path $psiScriptPath)) {
    Write-Host "Error: RimWorldModAnalysis.ps1 not found at $psiScriptPath" -ForegroundColor Red
    exit
}

# Execute the RimWorldModAnalysis.ps1 script
Import-Module Microsoft.PowerShell.Management
Write-Host "Executing RimWorldModAnalysis script..." -ForegroundColor Yellow
& $psiScriptPath
Write-Host "RimWorldModAnalysis script completed." -ForegroundColor Green

Write-Host "Analysis complete. Check the logs for details." -ForegroundColor Green
