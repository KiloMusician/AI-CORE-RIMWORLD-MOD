# Check and Install Dependencies Script

# Define the paths for the project and tools
$projectRoot = "C:\RimWorld_MODDING\AI-CORE-RIMWORLD-MOD"
$toolsDir = "$projectRoot\Tools"
$logPath = "$projectRoot\Logs\dependency_check.log"

# Ensure the Logs directory exists
if (-Not (Test-Path "$projectRoot\Logs")) {
    New-Item -Path "$projectRoot\Logs" -ItemType Directory
    "Created Logs directory at $projectRoot\Logs" | Out-File $logPath -Append
}

# List of required PowerShell modules
$requiredModules = @("PSScriptAnalyzer")

# Check and install PowerShell modules
foreach ($module in $requiredModules) {
    if (-Not (Get-Module -ListAvailable -Name $module)) {
        try {
            Install-Module -Name $module -Force
            "Installed PowerShell module: $module" | Out-File $logPath -Append
        } catch {
            "Failed to install module ${module}: $($_.Exception.Message)" | Out-File $logPath -Append
        }
    }
    else {
        "$module module is already installed." | Out-File $logPath -Append
    }
}

# Optionally open the log file in the default text editor for review
if (Test-Path "notepad.exe") {
    Start-Process "notepad.exe" $logPath
} else {
    "Error: notepad.exe not found. Unable to open log file." | Out-File $logPath -Append
}
