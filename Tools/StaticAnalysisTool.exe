using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaticAnalysisTool
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowUsage();
                return;
            }

            foreach (var directory in args)
            {
                if (!Directory.Exists(directory))
                {
                    Console.WriteLine($"Error: The specified directory does not exist: {directory}");
                    continue;
                }

                await ValidateFilesAsync(directory);
            }
        }

        static void ShowUsage()
        {
            Console.WriteLine("Usage: StaticAnalysisTool.exe <directory path> [additional directory paths]");
            Console.WriteLine("Specify one or more directories containing the critical files to validate.");
        }

        static async Task ValidateFilesAsync(string directory)
        {
            var criticalFiles = LoadCriticalFileDefinitions();

            bool allFilesValid = true;
            foreach (var file in criticalFiles)
            {
                string fullPath = Path.Combine(directory, file.Key);
                if (!File.Exists(fullPath))
                {
                    Console.WriteLine($"Missing critical file: {fullPath}. Ensure the file is present in the specified directory.");
                    allFilesValid = false;
                }
                else if (!await FileContainsTextAsync(fullPath, file.Value))
                {
                    Console.WriteLine($"Validation failed for file: {fullPath}. Check that the required content '{file.Value}' is included in the file.");
                    ProvideSuggestions(file.Key);
                    allFilesValid = false;
                }
            }

            Console.WriteLine(allFilesValid ? "All critical files are present and valid." : "Some critical files are missing or invalid. Refer to the above messages for detailed error information and suggestions.");
        }

        static Dictionary<string, string> LoadCriticalFileDefinitions()
        {
            // This method could be modified to load these mappings from an external configuration file.
            return new Dictionary<string, string>
            {
                ["ModController.cs"] = "class ModController",
                ["Program.cs"] = "static void Main",
                ["About\\about.xml"] = "<ModMetaData>",
                ["Def\\Items.xml"] = "<ThingDef>"
            };
        }

        static async Task<bool> FileContainsTextAsync(string filePath, string text)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (line.Contains(text))
                        return true;
                }
            }
            return false;
        }

        static void ProvideSuggestions(string fileName)
        {
            var suggestions = new Dictionary<string, string>
            {
                ["ModController.cs"] = "Ensure the class declaration for 'ModController' follows proper syntax and visibility modifiers.",
                ["Program.cs"] = "Verify the 'Main' method is correctly defined as static and includes necessary entry point logic.",
                ["About\\about.xml"] = "Check XML structure and ensure the root element 'ModMetaData' is correctly spelled and encapsulated.",
                ["Def\\Items.xml"] = "Review XML file for the correct structure of 'ThingDef' elements, ensuring they are well-formed."
            };

            if (suggestions.ContainsKey(fileName))
            {
                Console.WriteLine($"Suggestion: {suggestions[fileName]}");
            }
        }
    }
}
