using System;
using Verse; // Added missing import statement

namespace RimWorldAIEnhanced.Source.Utils
{
    public static class Logger
    {
        // Method for logging general information with timestamp
        public static void LogInfo(string message)
        {
            Log.Message($"[AIEnhanced - Info] {DateTime.Now}: " + message);
        }

        // Method for logging warnings with timestamp
        public static void LogWarning(string message)
        {
            Log.Warning($"[AIEnhanced - Warning] {DateTime.Now}: " + message);
        }

        // Method for logging errors with timestamp
        public static void LogError(string message)
        {
            Log.Error($"[AIEnhanced - Error] {DateTime.Now}: " + message);
        }

        // Method for logging debug information with timestamp; compiled out in release builds
        public static void LogDebug(string message)
        {
            #if DEBUG
            Log.Message($"[AIEnhanced - Debug] {DateTime.Now}: " + message);
            #endif
        }
    }
}
