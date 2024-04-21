using System;
using Verse;
using RimWorld;

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
                System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

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
            // Check for specific exception types or conditions
            if (ex is NullReferenceException)
            {
                Log.Warning("Handling null reference encountered during initialization. Attempting to bypass...");
                // Implement logic to bypass or correct the issue
            }
            else if (ex is TimeoutException)
            {
                Log.Warning("Initialization timeout. Trying to reinitialize...");
                // Re-attempt initialization or modify configuration settings
            }
            else
            {
                Log.Warning("Encountered an unexpected error during initialization.");
                // Handle other unexpected errors
            }
        }

        // Method to notify the user of a critical error if necessary
        private static void NotifyUserOfError(Exception ex)
        {
            // Decide if user notification is necessary based on the exception severity
            if (ex is NullReferenceException || ex is TimeoutException)
            {
                // Implement user notification, e.g., through a dialog box
                Messages.Message("AI Initialization Error: " + ex.Message, MessageTypeDefOf.NegativeEvent, false);
            }
        }
    }
}
