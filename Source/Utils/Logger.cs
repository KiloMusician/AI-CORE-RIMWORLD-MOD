using System;
using Verse;

namespace RimWorldAIEnhanced.Source.Utils
{
    public static class Logger
    {
        private static bool logInfo = true;
        private static bool logWarnings = true;
        private static bool logErrors = true;
        private static bool logDebug = true;

        public static void SetLogLevel(bool info, bool warnings, bool errors, bool debug)
        {
            logInfo = info;
            logWarnings = warnings;
            logErrors = errors;
            logDebug = debug;
        }

        public static void LogInfo(string message)
        {
            if (logInfo)
                Log.Message($"[AIEnhanced - Info] {DateTime.Now}: " + message);
        }

        public static void LogWarning(string message)
        {
            if (logWarnings)
                Log.Warning($"[AIEnhanced - Warning] {DateTime.Now}: " + message);
        }

        public static void LogError(string message)
        {
            if (logErrors)
                Log.Error($"[AIEnhanced - Error] {DateTime.Now}: " + message);
        }

        public static void LogDebug(string message)
        {
            if (logDebug)
            {
                #if DEBUG
                Log.Message($"[AIEnhanced - Debug] {DateTime.Now}: " + message);
                #endif
            }
        }
    }
}
