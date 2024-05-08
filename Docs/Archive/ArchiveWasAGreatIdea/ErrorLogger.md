using Verse;
using System;
using System.Collections.Generic;

namespace RimWorldMod.Logging
{
    public static class ErrorLogger
    {
        private static Dictionary<string, int> errorCounts = new Dictionary<string, int>();

        public static void LogError(string message, Exception ex = null)
        {
            Log.Error($"Error: {message}" + (ex != null ? $", Exception: {ex.Message}" : ""));
            CountAndCategorizeError(message);
        }

        public static void LogMessage(string message)
        {
            Log.Message(message);
        }

        private static void CountAndCategorizeError(string message)
        {
            if (!errorCounts.ContainsKey(message))
            {
                errorCounts[message] = 1;
            }
            else
            {
                errorCounts[message]++;
                if (errorCounts[message] % 10 == 0) // Every 10 occurrences
                {
                    Log.Warning($"Repeated error occurred {errorCounts[message]} times: {message}");
                }
            }
        }
    }
}

