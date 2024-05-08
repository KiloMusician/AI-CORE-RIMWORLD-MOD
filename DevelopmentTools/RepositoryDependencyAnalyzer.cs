using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using NLog;

public class DependencyAnalyzer
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    public static void Main(string[] args)
    {
        string projectPath = args[0];
        List<string> csFiles = Directory.GetFiles(projectPath, "*.cs", SearchOption.AllDirectories).ToList();

        // Store all class names
        HashSet<string> classNames = new HashSet<string>();

        // First pass: find all class names
        foreach (var file in csFiles)
        {
            try
            {
                SyntaxTree tree = CSharpSyntaxTree.ParseText(File.ReadAllText(file));
                var root = (CompilationUnitSyntax)tree.GetRoot();

                var classNodes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
                foreach (var classNode in classNodes)
                {
                    classNames.Add(classNode.Identifier.Text);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Error processing file {file}");
            }
        }

        // Second pass: find dependencies
        foreach (var file in csFiles)
        {
            try
            {
                SyntaxTree tree = CSharpSyntaxTree.ParseText(File.ReadAllText(file));
                var root = (CompilationUnitSyntax)tree.GetRoot();

                var usingNodes = root.DescendantNodes().OfType<UsingDirectiveSyntax>();
                foreach (var usingNode in usingNodes)
                {
                    var identifier = usingNode.DescendantNodes().OfType<IdentifierNameSyntax>().LastOrDefault();
                    if (identifier != null && classNames.Contains(identifier.Identifier.Text))
                    {
                        logger.Info($"File {file} depends on class {identifier.Identifier.Text}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Error processing file {file}");
            }
        }

        // Output results
        logger.Info("Dependency analysis complete.");
    }
}