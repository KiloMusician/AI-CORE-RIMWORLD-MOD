using System;
using Verse;  // Assuming use of RimWorld's Verse namespace for game entities
using RimWorldAIEnhanced.Source.Treatment; // Import the Treatment namespace

namespace RimWorldAIEnhanced.Source.Health
{
    public class HealthManager
    {
        // Method to evaluate the health of a Pawn and decide on necessary actions
        public void CheckHealthAndManage(Pawn pawn)
        {
            if (pawn == null)
            {
                Log.Error("HealthManager.CheckHealthAndManage called with a null pawn.");
                return;
            }

            // Ensure the pawn has a health tracker component
            if (pawn.health == null)
            {
                Log.Warning($"No health component found for {pawn.LabelShort}.");
                return;
            }

            // Example check: if health falls below a threshold, treat
            float healthPercentage = pawn.health.summaryHealth.SummaryHealthPercent;
            if (healthPercentage < 0.5)
            {
                // Log or handle low health
                Log.Message($"Health is low for {pawn.LabelShort}: {healthPercentage:P0}. Initiating treatment.");
                TreatPawn(pawn);
            }
            else
            {
                // Log normal health status
                Log.Message($"Health is stable for {pawn.LabelShort}: {healthPercentage:P0}.");
            }
        }

        // Direct treatment management
        private void TreatPawn(Pawn pawn)
        {
            if (pawn == null)
            {
                Log.Error("TreatPawn called with a null pawn.");
                return;
            }

            // Example: Call the TreatmentSimulator to handle specific treatment
            try
            {
                TreatmentSimulator.TreatCondition(pawn, "GeneralTreatment");
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to treat {pawn.LabelShort} due to an error: {ex.Message}");
            }
        }
    }
}
