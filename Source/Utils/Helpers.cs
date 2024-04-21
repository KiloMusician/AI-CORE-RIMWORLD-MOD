using System;
using UnityEngine;
using Verse;

namespace RimWorldAIEnhanced.Source.Utils
{
    public static class Helpers
    {
        // Method to convert temperature from Celsius to Fahrenheit
        public static float CelsiusToFahrenheit(float celsius)
        {
            return (celsius * 9 / 5) + 32;
        }

        // Method to check if a pawn is healthy (simplified check for demonstration purposes)
        public static bool IsPawnHealthy(Pawn pawn)
        {
            return pawn.health.summaryHealth.SummaryHealthPercent > 0.95;
        }

        // A generic method to safely try an action and log any exceptions
        public static void TryAction(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed to execute action: " + ex.Message);
            }
        }

        // Method for creating a deep copy of an object (simplified version using JSON serialization)
        public static T DeepCopy<T>(T obj)
        {
            string json = JsonUtility.ToJson(obj);
            return JsonUtility.FromJson<T>(json);
        }
    }
}
