using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AI_CORE_RIMWORLD_MOD.OmniCore
{
    public static class UpdateSystem
    {
        private static Dictionary<string, Script> scripts = new Dictionary<string, Script>();
        private static string scriptDirectory = @"C:\path\to\scripts";
        private static string backupDirectory = @"C:\path\to\backups";
        private static string patchDirectory = @"C:\path\to\patches";

        public static void CheckForUpdates()
        {
            Console.WriteLine("Checking for updates.");

            // Load the current versions of all scripts
            LoadScripts();

            // Check for updates to each script
            foreach (var script in scripts.Values)
            {
                CheckForScriptUpdate(script);
            }
        }

        public static void ApplyPatch(Script script, Patch patch)
        {
            Console.WriteLine($"Applying patch: {patch.Name}");

            // Validate the patch
            if (!ValidatePatch(script, patch))
            {
                Console.WriteLine($"Patch {patch.Name} failed validation.");
                return;
            }

            // Backup the script
            BackupScript(script);

            // Apply the patch to the script
            ApplyScriptPatch(script, patch);

            // Update the version of the script
            UpdateScriptVersion(script, patch.NewVersion);
        }

        private static void LoadScripts()
        {
            // Load script versions from a file
            scripts = LoadScriptVersions();
        }

        private static Dictionary<string, Script> LoadScriptVersions()
        {
            var scriptVersions = new Dictionary<string, Script>();

            foreach (var file in Directory.GetFiles(scriptDirectory, "*.txt"))
            {
                var name = Path.GetFileNameWithoutExtension(file);
                var version = File.ReadLines(file).First();

                scriptVersions[name] = new Script { Name = name, Version = version };
            }

            return scriptVersions;
        }

        private static void CheckForScriptUpdate(Script script)
        {
            // Check for updates to the script
            var latestVersion = GetLatestScriptVersion(script);

            if (latestVersion != script.Version)
            {
                Console.WriteLine($"Update available for {script.Name}");
            }
        }

        private static string GetLatestScriptVersion(Script script)
        {
            // Get the latest version of the script from the patch directory
            var patchFiles = Directory.GetFiles(patchDirectory, script.Name + "*.patch");
            var latestVersion = script.Version;

            foreach (var patchFile in patchFiles)
            {
                var patchVersion = Path.GetFileNameWithoutExtension(patchFile).Split('_')[1];
                if (CompareVersions(patchVersion, latestVersion) > 0)
                {
                    latestVersion = patchVersion;
                }
            }

            return latestVersion;
        }

        private static bool ValidatePatch(Script script, Patch patch)
        {
            // Validate the patch
            return patch.OldVersion == script.Version && patch.NewVersion == GetLatestScriptVersion(script);
        }

        private static void BackupScript(Script script)
        {
            // Backup the script
            File.Copy(scriptDirectory + script.Name + ".txt", backupDirectory + script.Name + ".backup", true);
        }

        private static void ApplyScriptPatch(Script script, Patch patch)
        {
            // Apply the patch to the script
            File.WriteAllText(scriptDirectory + script.Name + ".txt", patch.Content);
        }

        private static void UpdateScriptVersion(Script script, string newVersion)
        {
            // Update the version of the script
            script.Version = newVersion;
            Console.WriteLine($"Updated version of {script.Name} to {newVersion}");
        }

        private static int CompareVersions(string version1, string version2)
        {
            var version1Parts = version1.Split('.').Select(int.Parse).ToArray();
            var version2Parts = version2.Split('.').Select(int.Parse).ToArray();

            for (int i = 0; i < Math.Max(version1Parts.Length, version2Parts.Length); i++)
            {
                int part1 = i < version1Parts.Length ? version1Parts[i] : 0;
                int part2 = i < version2Parts.Length ? version2Parts[i] : 0;

                if (part1 > part2) return 1;
                if (part1 < part2) return -1;
            }

            return 0;
        }
    }

    public class Script
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }

    public class Patch
    {
        public string Name { get; set; }
        public string OldVersion { get; set; }
        public string NewVersion { get; set; }
        public string Content { get; set; }
    }
}