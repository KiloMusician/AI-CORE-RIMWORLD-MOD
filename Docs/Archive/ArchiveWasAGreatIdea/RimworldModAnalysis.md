# RimWorldModAnalysis.ps1

# Define the paths for the project and static analysis tool
$projectRoot = "C:\RimWorld_MODDING\AI-CORE-RIMWORLD-MOD"
$toolsDir = Join-Path -Path $projectRoot -ChildPath "Tools"
$toolPath = Join-Path -Path $toolsDir -ChildPath "StaticAnalysisTool.exe"
$logPath = Join-Path -Path $projectRoot -ChildPath "Logs\analysis_results.log"

# Ensure the Logs directory exists
if (-Not (Test-Path -Path (Join-Path -Path $projectRoot -ChildPath "Logs"))) {
    New-Item -Path "$projectRoot\Logs" -ItemType Directory | Out-Null
    "Created Logs directory at $projectRoot\Logs" | Out-File -FilePath $logPath -Append
}

# Check if the static analysis tool exists and is executable
if (-Not (Test-Path -Path $toolPath)) {
    "Error: Static analysis tool not found at $toolPath" | Out-File -FilePath $logPath -Append
    exit
}

# Check if the PSScriptAnalyzer module is installed, and if not, install it
if (-not (Get-Module -ListAvailable -Name PSScriptAnalyzer)) {
    Write-Host "Installing PSScriptAnalyzer module..."
    Install-Module -Name PSScriptAnalyzer -Force | Out-Null
}

# Prepare arguments for running the static analysis tool with detailed output
$arguments = "--input `"$projectRoot\Source`" --output `"$logPath`" --format detailed"

# Configure ProcessStartInfo for external process execution
$processInfo = New-Object System.Diagnostics.ProcessStartInfo
$processInfo.FileName = $toolPath
$processInfo.Arguments = $arguments
$processInfo.RedirectStandardOutput = $true
$processInfo.RedirectStandardError = $true
$processInfo.UseShellExecute = $false
$processInfo.CreateNoWindow = $true

# Execute the static analysis tool
$process = New-Object System.Diagnostics.Process
$process.StartInfo = $processInfo

try {
    $process.Start() | Out-Null
    "Running static analysis on the project..." | Out-File -FilePath $logPath -Append
    $process.WaitForExit()

    $output = $process.StandardOutput.ReadToEnd()
    $errorOutput = $process.StandardError.ReadToEnd()

    if ($process.ExitCode -eq 0) {
        "Static analysis completed successfully." | Out-File -FilePath $logPath -Append
        $output | Out-File -FilePath $logPath -Append
    } else {
        "Static analysis reported errors:" | Out-File -FilePath $logPath -Append
        $errorOutput | Out-File -FilePath $logPath -Append
    }
} catch {
    $errorMsg = "An error occurred during static analysis: $($_.Exception.Message)"
    $errorMsg | Out-File -FilePath $logPath -Append
    Write-Host $errorMsg -ForegroundColor Red
}

# Check for results and provide detailed logging
if (Test-Path -Path $logPath) {
    $results = Get-Content -Path $logPath
    if ($results.Count -gt 0) {
        "Analysis Results Found:" | Out-File -FilePath $logPath -Append
        $results | Out-File -FilePath $logPath -Append
    } else {
        "No issues found during analysis." | Out-File -FilePath $logPath -Append
    }
} else {
    "No log file found. Analysis may not have run correctly." | Out-File -FilePath $logPath -Append
}

# Optionally open the log file with notepad.exe
Start-Process "notepad.exe" -ArgumentList $logPath
