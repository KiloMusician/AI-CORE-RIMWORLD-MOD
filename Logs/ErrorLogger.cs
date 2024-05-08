using Verse;
using System;
using System.Collections.Generic;

namespace AI_CORE_RIMWORLD_MOD.Logging
{
    public static class ErrorLogger
    {
        private static Dictionary<string, int> errorCounts = new Dictionary<string, int>();

        public static void LogError(string message, Exception ex = null)
        {
            string fullMessage = $"Error: {message}" + (ex != null ? $", Exception: {ex.Message}" : "");
            Log.Error(fullMessage);

            // Log stack trace for deeper insights if available
            if (ex != null && ex.StackTrace != null)
            {
                Log.Error("Stack Trace: " + ex.StackTrace);
            }

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
            }

            // Log a warning at every 10 occurrences
            if (errorCounts[message] % 10 == 0)
            {
                Log.Warning($"Repeated error occurred {errorCounts[message]} times: {message}");
            }

            // Provide actionable feedback if error becomes frequent
            if (errorCounts[message] == 50)
            {
                Log.Warning($"Error '{message}' has occurred 50 times. Consider reviewing related systems or components.");
            }
        }
    }
}
