# Enhanced PowerShell script to automatically find RimWorld assemblies, update project references, and invoke analysis tools.

# Define the base path to the RimWorld installation and project directory
$rimWorldPath = "C:\Program Files (x86)\Steam\steamapps\common\RimWorld"
$projectPath = "C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\AI-CORE-RIMWORLD-MOD"
$csprojFile = "$projectPath\ModProject.csproj"

# Define DLLs to search for
$dllNames = @("UnityEngine.dll", "UnityEngine.CoreModule.dll", "Assembly-CSharp.dll", "RimWorld.dll")

function Update-CsProjReferences {
    Param ([string]$csprojPath, [string]$dllName, [string]$dllFullPath)
    # Load the XML content of the .csproj file
    [xml]$csprojXml = Get-Content $csprojPath
    # Check and update the HintPath for each reference
    foreach ($element in $csprojXml.Project.ItemGroup.Reference) {
        if ($element.Include -eq $dllName.Split('.')[0]) {
            $element.HintPath = $dllFullPath
        }
    }
    # Save the updated .csproj file
    $csprojXml.Save($csprojPath)
    Write-Host "Updated $dllName reference in $csprojPath"
}

# Search and update references
foreach ($dll in $dllNames) {
    $foundDll = Get-ChildItem -Path $rimWorldPath -Recurse -Filter $dll -ErrorAction SilentlyContinue | Select-Object -First 1
    if ($foundDll) {
        Write-Host "$dll found: $($foundDll.FullName)"
        Update-CsProjReferences -csprojPath $csprojFile -dllName $dll -dllFullPath $foundDll.FullName
    } else {
        Write-Host "$dll not found in the RimWorld directory."
    }
}

# Run other scripts and tools
& "$projectPath\Scripts\AutoDependencyInstaller.ps1"
& "$projectPath\Tools\UnifiedProjectAnalysis.ps1"
& "$projectPath\Tools\StaticAnalysisTool.exe"

# Additional XML manipulation for AiCompanionCore.xml if needed
[xml]$aiXml = Get-Content "$projectPath\AiCompanionCore.xml"
# Example XML data manipulation
$aiXml.Root.SomeNode = "NewValue"
$aiXml.Save("$projectPath\AiCompanionCore.xml")

Write-Host "Dependency installation and project analysis complete."
