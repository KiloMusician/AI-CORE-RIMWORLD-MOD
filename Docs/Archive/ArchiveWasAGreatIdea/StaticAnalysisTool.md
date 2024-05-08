using System;
using System.IO;
using System.Collections.Generic;

namespace StaticAnalysisTool
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowUsage();
                return;
            }

            string directory = args[0];
            if (!Directory.Exists(directory))
            {
                Console.WriteLine($"Error: The specified directory does not exist: {directory}");
                return;
            }

            ValidateFiles(directory);
        }

        static void ShowUsage()
        {
            Console.WriteLine("Usage: StaticAnalysisTool.exe <directory path>");
            Console.WriteLine("Specify the directory containing the critical files to validate.");
        }

        static void ValidateFiles(string directory)
        {
            var criticalFiles = new Dictionary<string, Func<string, bool>>
            {
                ["ModController.cs"] = path => FileContainsText(path, "class ModController"),
                ["Program.cs"] = path => FileContainsText(path, "static void Main"),
                ["About\\about.xml"] = path => FileContainsText(path, "<ModMetaData>"),
                ["Def\\Items.xml"] = path => FileContainsText(path, "<ThingDef>")
            };

            bool allFilesValid = true;
            foreach (var file in criticalFiles)
            {
                string fullPath = Path.Combine(directory, file.Key);
                if (!File.Exists(fullPath))
                {
                    Console.WriteLine($"Missing critical file: {fullPath}");
                    allFilesValid = false;
                }
                else if (!file.Value(fullPath))
                {
                    Console.WriteLine($"Validation failed for file: {fullPath}");
                    allFilesValid = false;
                }
            }

            Console.WriteLine(allFilesValid ? "All critical files are present and valid." : "Some critical files are missing or invalid.");
        }

        private static bool FileContainsText(string filePath, string text)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(text))
                        return true;
                }
            }
            return false;
        }
    }
}
