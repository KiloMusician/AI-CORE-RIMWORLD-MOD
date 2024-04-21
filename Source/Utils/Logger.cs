using System;
using Verse; // Added missing import statement

namespace RimWorldAIEnhanced.Source.Utils
{
    public static class Logger
    {
        // Method for logging general information
        public static void LogInfo(string message)
        {
            Log.Message("[AIEnhanced]: " + message);
        }

        // Method for logging warnings
        public static void LogWarning(string message)
        {
            Log.Warning("[AIEnhanced - Warning]: " + message);
        }

        // Method for logging errors
        public static void LogError(string message)
        {
            Log.Error("[AIEnhanced - Error]: " + message);
        }

        // Method for logging debug information (could be compiled out in release builds)
        public static void LogDebug(string message)
        {
            #if DEBUG
            Log.Message("[AIEnhanced - Debug]: " + message);
            #endif
        }
    }
}
