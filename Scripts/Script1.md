# Define the paths for the project, tools directory, and the log path
$projectRoot = "C:\RimWorld_MODDING\AI-CORE-RIMWORLD-MOD"
$toolsDir = "$projectRoot\Tools"
$toolPath = "$toolsDir\StaticAnalysisTool.exe"  # Verify this path points to the executable
$logPath = "$projectRoot\Logs\dependency_installation.log"

# Ensure the Logs directory exists
if (-Not (Test-Path "$projectRoot\Logs")) {
    New-Item -Path "$projectRoot\Logs" -ItemType Directory
    "Created Logs directory at $projectRoot\Logs" | Out-File $logPath -Append
}

# Check if the static analysis tool exists and is executable
if (-Not (Test-Path $toolPath)) {
    "Error: Static analysis tool not found at $toolPath" | Out-File $logPath -Append
    exit
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
    "Running static analysis on the project..." | Out-File $logPath -Append
    $process.WaitForExit()

    $output = $process.StandardOutput.ReadToEnd()
    $errorOutput = $process.StandardError.ReadToEnd()

    if ($process.ExitCode -eq 0 -and $output) {
        "Static analysis completed successfully." | Out-File $logPath -Append
        $output | Out-File $logPath -Append
    } else {
        "Static analysis reported errors:" | Out-File $logPath -Append
        $errorOutput | Out-File $logPath -Append
    }
} catch {
    $errorMsg = "An error occurred during static analysis: $($_.Exception.Message)"
    $errorMsg | Out-File $logPath -Append
    Write-Host $errorMsg -ForegroundColor Red
}

# Check for results and provide detailed logging
if (Test-Path $logPath) {
    $results = Get-Content $logPath
    if ($results.Count -gt 0) {
        "Analysis Results Found:" | Out-File $logPath -Append
        $results | Out-File $logPath -Append
    } else {
        "No issues found during analysis." | Out-File $logPath -Append
    }
} else {
    "No log file found. Analysis may not have run correctly." | Out-File $logPath -Append
}

# Optionally open the log file in the default text editor for review
Start-Process "notepad.exe" $logPath
