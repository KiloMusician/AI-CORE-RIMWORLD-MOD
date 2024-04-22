using System;
using System.Collections.Generic;
using Verse;  // RimWorld's base namespace for managing game components

namespace RimWorldAdvancedAIMod.AI
{
    public class AITagManager
    {
        public SuperMegaHyperTag CurrentTag { get; private set; }

        public AITagManager()
        {
            CurrentTag = new SuperMegaHyperTag
            {
                PrimerInfo = "Initialize AI Enhanced Gameplay",
                FAQsCovered = new List<string>(),
                PendingAction = "Define Initial Parameters",
                PriorExchange = "None",
                SpecialInstructions = "Initialize Advanced Level",
                DepthLevel = "High"
            };
        }

        // Updates the SuperMegaHyperTag with new data
        public void UpdateTag(string primerInfo, string pendingAction, string specialInstructions, string depthLevel)
        {
            CurrentTag.PrimerInfo = primerInfo;
            CurrentTag.PendingAction = pendingAction;
            CurrentTag.SpecialInstructions = specialInstructions;
            CurrentTag.DepthLevel = depthLevel;

            // Example: Automatically add to FAQsCovered after updating
            CurrentTag.FAQsCovered.Add("Updated Tag with new primer info and actions");
        }

        // Retrieve formatted tag information for debugging or display
        public string GetFormattedTagInfo()
        {
            return $"Primer Info: {CurrentTag.PrimerInfo}\n" +
                   $"Pending Action: {CurrentTag.PendingAction}\n" +
                   $"Special Instructions: {CurrentTag.SpecialInstructions}\n" +
                   $"Depth Level: {CurrentTag.DepthLevel}\n" +
                   $"FAQs Covered: {string.Join(", ", CurrentTag.FAQsCovered)}";
        }
    }

    public class SuperMegaHyperTag
    {
        public string PrimerInfo { get; set; }
        public List<string> FAQsCovered { get; set; }
        public string PendingAction { get; set; }
        public string PriorExchange { get; set; }
        public string SpecialInstructions { get; set; }
        public string DepthLevel { get; set; }

        public SuperMegaHyperTag()
        {
            FAQsCovered = new List<string>();
        }
    }
}
