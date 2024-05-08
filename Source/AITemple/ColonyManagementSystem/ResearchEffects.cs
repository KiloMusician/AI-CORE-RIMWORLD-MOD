using HarmonyLib;
using RimWorld;
using Verse;
using System;
using System.Collections.Generic;

namespace Source.AITemple.ColonyManagementSystem
{
    // Patch the ResearchManager to add our custom logic when research is completed
    [HarmonyPatch(typeof(ResearchManager), "FinishProject")]
    public static class AIResearchHandler
    {
        // Postfix method to execute after the research project is completed
        static void Postfix(ResearchProjectDef proj, bool doCompletionDialog = false)
        {
            // Check if the completed project is one of our AI-related researches
            if (proj.defName == "AIIntegrationBasics" || proj.defName == "AdvancedAISystems")
            {
                // Trigger the AI functionality or unlock associated items/features
                ActivateAIFeatures(proj);
            }
        }

        // Method to activate AI features based on the research project
        private static void ActivateAIFeatures(ResearchProjectDef proj)
        {
            if (proj.defName == "AIIntegrationBasics")
            {
                // Logic to activate basic AI features
                Log.Message("Basic AI Integration features are now available.");
                // Example: Enable basic AI decision making for colonists or machinery
            }
            else if (proj.defName == "AdvancedAISystems")
            {
                // Logic to activate advanced AI features
                Log.Message("Advanced AI Systems are now fully operational.");
                // Example: Implement advanced AI strategies, improve efficiency, etc.
            }
        }
    }

    public class ResearchEffects
    {
        private Dictionary<string, Action> researchImpactMap;

        public ResearchEffects()
        {
            researchImpactMap = new Dictionary<string, Action>()
            {
                {"Hydroponics", ActivateHydroponics},
                {"SolarPanel", ActivateSolarPanels},
                {"AdvancedDefense", EnhanceBaseDefense}
            };
        }

        public void ApplyResearchEffect(string researchId)
        {
            if (researchImpactMap.TryGetValue(researchId, out Action effect))
            {
                effect?.Invoke();
            }
        }

        private void ActivateHydroponics()
        {
            // Code to enable hydroponics farming systems
            Console.WriteLine("Hydroponics activated, increasing food production efficiency.");
        }

        private void ActivateSolarPanels()
        {
            // Code to enable solar panel energy production
            Console.WriteLine("Solar panels activated, providing sustainable energy.");
        }

        private void EnhanceBaseDefense()
        {
            // Code to enhance defensive capabilities of the colony
            Console.WriteLine("Advanced defensive technologies activated, enhancing colony defenses.");
        }
    }
}
