using System;
using System.Collections.Generic;
using System.IO;

namespace AI_Core.Utils
{
    public static class CommandLogger
    {
        private static HashSet<string> loggedCommands = new HashSet<string>();
        private static string logFilePath = @"C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\OldestHouse\Logging\CommandsLog.txt";

        public static void LogCommand(string command)
        {
            if (!string.IsNullOrEmpty(command) && loggedCommands.Add(command)) // Check for null or empty command and uniqueness
            {
                try
                {
                    File.AppendAllText(logFilePath, $"{DateTime.UtcNow}: {command}{Environment.NewLine}"); // Include a timestamp and ensure thread safety
                }
                catch (Exception ex)
                {
                    // Handle possible IO exceptions in a user-friendly way
                    Log.Error($"Failed to log command to file: {ex.Message}");
                }
            }
        }

        // Load previous commands from file to HashSet to maintain state between sessions
        public static void Initialize()
        {
            try
            {
                if (File.Exists(logFilePath))
                {
                    var commands = File.ReadAllLines(logFilePath);
                    foreach (var cmd in commands)
                    {
                        loggedCommands.Add(cmd);
                    }
                    Log.Message("Command log initialized successfully.");
                }
                else
                {
                    // Create the file if it does not exist
                    File.Create(logFilePath).Close();
                    Log.Message("Command log file created successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log initialization failures
                Log.Error($"Failed to initialize command log: {ex.Message}");
            }
        }
    }
}
