using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace RimWorld.Mods.AI_CORE_RIMWORLD_MOD.DevelopmentTools
{
    class Program
    {
        // Define a new class to hold file information
        class FileInfoEx
        {
            public string FilePath { get; set; }
            public int Order { get; set; }
            public string Status { get; set; }
            public List<string> ToDo { get; set; } = new List<string>();
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowUsage();
                return;
            }

            // Read the report file generated by StaticAnalysisTool2.cs
            var reportFile = args[0];
            if (!File.Exists(reportFile))
            {
                Console.WriteLine($"Error: The specified report file does not exist: {reportFile}");
                return;
            }

            // Generate the repository representation
            var repoRepresentation = GenerateRepositoryRepresentation(reportFile);
            foreach (var fileInfo in repoRepresentation)
            {
                Console.WriteLine($"File: {fileInfo.FilePath}, Order: {fileInfo.Order}, Status: {fileInfo.Status}");
                foreach (var todo in fileInfo.ToDo)
                {
                    Console.WriteLine($"  ToDo: {todo}");
                }
            }
        }

        static void ShowUsage()
        {
            Console.WriteLine("Usage: RepositoryAnalysisTool.exe <report file path>");
            Console.WriteLine("Specify the path to the report file generated by StaticAnalysisTool2.exe.");
        }

        static List<FileInfoEx> GenerateRepositoryRepresentation(string reportFile)
        {
            var repoRepresentation = new List<FileInfoEx>();
            int order = 0;

            var lines = File.ReadLines(reportFile);
            foreach (var line in lines)
            {
                var fileInfo = new FileInfoEx
                {
                    FilePath = line,
                    Order = order++,
                    Status = File.Exists(line) ? "Functional" : "Missing", // Simplified status determination
                    ToDo = new List<string> { "Review file", "Check dependencies" } // Simplified ToDo list
                };
                repoRepresentation.Add(fileInfo);
            }

            return repoRepresentation;
        }
    }
}