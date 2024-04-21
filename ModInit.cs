using System;
using Verse;
using RimWorld;
using System.Diagnostics;

namespace RimWorldAdvancedAIMod
{
    [StaticConstructorOnStartup]
    public static class ModInit
    {
        static ModInit()
        {
            try
            {
                // Start a stopwatch for performance measurement
                Stopwatch stopwatch = Stopwatch.StartNew();

                // Verify game and mod dependencies are loaded properly before initialization
                if (!DependenciesLoadedCorrectly())
                {
                    Log.Error("RimWorld Advanced AI Mod initialization aborted: Dependencies not loaded.");
                    return;  // Stop further initialization to prevent crashes due to missing dependencies
                }

                // Attempt to initialize the AI Manager
                AIManager.Initialize();
                stopwatch.Stop();

                // Log successful initialization and time taken
                Log.Message($"RimWorld Advanced AI Mod Loaded and AI Manager initialized in {stopwatch.ElapsedMilliseconds} ms.");

                // Additional setup can be performed here, add as needed
                PerformAdditionalSetup();
            }
            catch (Exception ex)
            {
                // Log the error in detail
                Log.Error($"Failed to initialize RimWorld Advanced AI Mod: {ex}");

                // Attempt to handle known issues or reinitialize components if safe
                HandleInitializationFailure(ex);

                // Optionally, notify the user via dialog or keep silent depending on the severity
                NotifyUserOfError(ex);
            }
        }

        // Check if all necessary dependencies are loaded correctly
        private static bool DependenciesLoadedCorrectly()
        {
            // Example check (you should implement actual checks against the mod's dependencies)
            return ModsConfig.ActiveModsInLoadOrder.Any(mod => mod.Name.Contains("RequiredDependency"));
        }

        // Method to handle additional setup tasks
        private static void PerformAdditionalSetup()
        {
            // Placeholder for additional setup operations
            Log.Message("Performing additional setup tasks for the Advanced AI Mod.");
            // Implement any additional setups here
        }

        // Method to handle failures during initialization
        private static void HandleInitializationFailure(Exception ex)
        {
            // Implement specific logic for handling and possibly recovering from different types of exceptions
            if (ex is NullReferenceException)
            {
                Log.Warning("Null reference encountered during initialization. Attempting to bypass...");
            }
            else if (ex is TimeoutException)
            {
                Log.Warning("Initialization timeout. Trying to reinitialize...");
            }
            else
            {
                Log.Warning("Encountered an unexpected error during initialization.");
            }
        }

        // Method to notify the user of a critical error if necessary
        private static void NotifyUserOfError(Exception ex)
        {
            Messages.Message("AI Initialization Error: " + ex.Message, MessageTypeDefOf.NegativeEvent, false);
        }
    }
}
