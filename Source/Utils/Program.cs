using System;
using System.IO;

namespace Source.Utils

class Program
{
    static void Main(string[] args)
    {
        string repositoryPath = GetRepositoryPath();
        if (repositoryPath == null)
        {
            Console.WriteLine("Error: Repository path not found.");
            return;
        }

        string summaryFilePath = Path.Combine(repositoryPath, "summary.txt");

        GenerateSummary(repositoryPath, summaryFilePath);

        Console.WriteLine($"Summary file generated: {summaryFilePath}");
    }

    static string GetRepositoryPath()
    {
        // Get the current directory of the application
        string currentDirectory = Directory.GetCurrentDirectory();

        // Search upwards in the directory tree until finding a folder containing a .git directory
        while (!Directory.Exists(Path.Combine(currentDirectory, ".git")))
        {
            currentDirectory = Directory.GetParent(currentDirectory)?.FullName;

            // If no .git directory is found, return null
            if (currentDirectory == null)
            {
                return null;
            }
        }

        return currentDirectory;
    }

    static void GenerateSummary(string repositoryPath, string summaryFilePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(summaryFilePath))
            {
                writer.WriteLine($"Summary of files and folders in {repositoryPath}:\n");
                GenerateSummaryRecursive(repositoryPath, "", writer);
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions gracefully
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void GenerateSummaryRecursive(string directory, string indent, StreamWriter writer)
    {
        try
        {
            // Retrieve all directories and files in the current directory
            string[] directories = Directory.GetDirectories(directory);
            string[] files = Directory.GetFiles(directory);

            // Write directories to the summary file
            foreach (string dir in directories)
            {
                writer.WriteLine($"{indent}{Path.GetFileName(dir)}/");
                GenerateSummaryRecursive(dir, indent + "    ", writer);
            }

            // Write files to the summary file
            foreach (string file in files)
            {
                writer.WriteLine($"{indent}{Path.GetFileName(file)}");
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions gracefully
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
