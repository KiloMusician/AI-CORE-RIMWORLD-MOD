using System;
using Verse; // Assuming use of RimWorld's Verse namespace for game entities
using RimWorldAIEnhanced.Source.Treatment; // Import the Treatment namespace

namespace RimWorldAIEnhanced.Source.Health
{
    public class HealthManager
    {
        // Method to evaluate the health of a Pawn and decide on necessary actions
        public void CheckHealthAndManage(Pawn pawn)
        {
            // Example check: if health falls below a threshold, treat
            if (pawn.health.summaryHealth.SummaryHealthPercent < 0.5)
            {
                // Log or handle low health
                Log.Message($"Health is low for {pawn.Name.ToStringShort}. Initiating treatment.");
                TreatPawn(pawn);
            }
            else
            {
                // Log normal health status
                Log.Message($"Health is stable for {pawn.Name.ToStringShort}.");
            }
        }

        // Direct treatment management
        private void TreatPawn(Pawn pawn)
        {
            // Example: Call the TreatmentSimulator to handle specific treatment
            TreatmentSimulator.TreatCondition(pawn, "GeneralTreatment");
        }
    }
}
