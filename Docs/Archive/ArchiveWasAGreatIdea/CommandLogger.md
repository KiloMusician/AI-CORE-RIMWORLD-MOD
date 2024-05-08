using System;
using System.Collections.Generic;
using System.IO;

namespace OldestHouse.Utils
{
    public static class CommandLogger
    {
        private static HashSet<string> loggedCommands = new HashSet<string>();
        private static string logFilePath = @"C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\OldestHouse\Logging\CommandsLog.txt";

        public static void LogCommand(string command)
        {
            if (loggedCommands.Add(command)) // Only add if not already present
            {
                File.AppendAllText(logFilePath, command + Environment.NewLine);
            }
        }

        // Load previous commands from file to HashSet to maintain state between sessions
        public static void Initialize()
        {
            if (File.Exists(logFilePath))
            {
                var commands = File.ReadAllLines(logFilePath);
                foreach (var cmd in commands)
                {
                    loggedCommands.Add(cmd);
                }
            }
        }
    }
}
