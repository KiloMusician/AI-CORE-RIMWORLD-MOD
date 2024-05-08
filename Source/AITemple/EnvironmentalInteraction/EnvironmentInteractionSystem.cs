using System;
using RimWorld;
using Verse;

namespace Source.AITemple.EnvironmentalInteraction 
{
    public static class EnvironmentInteractionSystem
    {
        // Method to initialize environment interactions
        public static void InitializeInteractionSystem()
        {
            Log.Message("Initializing Environment Interaction System...");

            try
            {
                SetupInteractionRules();
                Log.Message("Environment Interaction System Initialized Successfully.");
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to initialize environment interactions: {ex.Message}");
            }
        }

        // Define the rules for how AI interacts with different environmental elements
        private static void SetupInteractionRules()
        {
            // Setup interaction rules, such as how AI should respond to weather changes, terrain types, etc.
            Log.Message("Interaction rules set up for varying environmental conditions.");
        }

        // Update method to adjust AI's interaction with the environment as it changes
        public static void UpdateInteractions()
        {
            RespondToWeatherChanges();
            AdjustToTerrainChanges();
            Log.Message("Environment interactions updated according to current conditions.");
        }

        // Example methods that respond to specific environmental changes
        private static void RespondToWeatherChanges()
        {
            // AI responses to weather changes, e.g., seeking shelter in bad weather
            Log.Message("AI responding to weather changes.");
        }

        private static void AdjustToTerrainChanges()
        {
            // Adjust AI behavior based on terrain changes, e.g., navigating obstacles
            Log.Message("AI adjusting to terrain changes.");
        }
        public static void AdjustToWeatherConditions(Pawn pawn)
        {
            var currentWeather = pawn.Map.weatherManager.curWeather;
            if (currentWeather == WeatherDefOf.Rain || currentWeather == WeatherDefOf.SnowHard)
            {
                if (!pawn.apparel.WornApparel.Any(a => a.def == ThingDefOf.Apparel_Parka))
                {
                    var parka = (Apparel)ThingMaker.MakeThing(ThingDefOf.Apparel_Parka);
                    pawn.apparel.Wear(parka);
                }
            }
        }
    }
}