using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Source.AITemple.Yggdrasil
{
    class BehaviourTreeUpdater
    {
        static async Task Main(string[] args)
        {
            // Base paths
            string repositoryPath = @"C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\AI-CORE-RIMWORLD-MOD";
            string behaviourTreePath = Path.Combine(repositoryPath, "BehaviourTree.cs");
            string logFilePath = Path.Combine(repositoryPath, "logs", "action_states.log");

            // Main operational loop
            await ScheduledUpdate(logFilePath, behaviourTreePath);
        }

        // Function to parse log file and extract new actions and states
        static Dictionary<string, List<string>> GetNewActionsAndStates(string logFilePath)
        {
            string[] logContent = File.ReadAllLines(logFilePath);
            Dictionary<string, List<string>> newItems = new Dictionary<string, List<string>>();

            foreach (string line in logContent)
            {
                Match match = Regex.Match(line, @"(?<category>\w+): (?<item>.+)");
                if (match.Success)
                {
                    string category = match.Groups["category"].Value;
                    string item = match.Groups["item"].Value;

                    if (!newItems.ContainsKey(category))
                    {
                        newItems[category] = new List<string>();
                    }

                    newItems[category].Add(item);
                }
            }

            return newItems;
        }

        // Update BehaviourTree.cs with new actions and states
        static void UpdateBehaviourTreeFile(Dictionary<string, List<string>> newItems, string behaviourTreePath)
        {
            List<string> existingCategories = new List<string>();
            List<string> newBehaviourTreeContent = new List<string>();

            foreach (string line in File.ReadLines(behaviourTreePath))
            {
                Match match = Regex.Match(line, @"public List<string> (\w+)");
                if (match.Success)
                {
                    string category = match.Groups[1].Value;
                    existingCategories.Add(category);
                }

                newBehaviourTreeContent.Add(line);
            }

            using (StreamWriter stream = File.AppendText(behaviourTreePath))
            {
                foreach (string category in newItems.Keys)
                {
                    if (!existingCategories.Contains(category))
                    {
                        stream.WriteLine($"public List<string> {category} = new List<string>();");
                        stream.WriteLine("{");
                    }

                    foreach (string item in newItems[category])
                    {
                        string itemEntry = $"\t\"{item}\",";
                        stream.WriteLine(itemEntry);
                    }

                    if (!existingCategories.Contains(category))
                    {
                        stream.WriteLine("};");
                    }
                }
            }
        }

        // Scheduled update loop
        static async Task ScheduledUpdate(string logFilePath, string behaviourTreePath)
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromMinutes(5)); // Check every 5 minutes
                Dictionary<string, List<string>> newActionsAndStates = GetNewActionsAndStates(logFilePath);
                UpdateBehaviourTreeFile(newActionsAndStates, behaviourTreePath);
                Console.WriteLine("BehaviourTree.cs has been updated with new actions and states.");
            }
        }
    }
}
