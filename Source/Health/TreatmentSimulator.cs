using System;
using Verse;  // Namespace for many game-related classes
using RimWorld;  // Namespace for RimWorld-specific classes

namespace Source.Health
{
    public static class TreatmentSimulator
    {
        // Static method to simulate treatment application to a Pawn
        public static void TreatCondition(Pawn pawn, string treatmentType)
        {
            if (pawn == null)
            {
                Log.Error("TreatCondition called with a null pawn.");
                return;
            }

            // Log the treatment application
            Log.Message($"Applying {treatmentType} to {pawn.Name.ToStringShort}.");

            // Placeholder for treatment logic based on the type of treatment
            switch (treatmentType)
            {
                case "GeneralTreatment":
                    ApplyGeneralTreatment(pawn);
                    break;
                case "EmergencyTreatment":
                    ApplyEmergencyTreatment(pawn);
                    break;
                default:
                    Log.Warning($"Unknown treatment type: {treatmentType}.");
                    break;
            }
        }

        // Method to apply general treatment
        private static void ApplyGeneralTreatment(Pawn pawn)
        {
            // Check if the Hediff exists; if not, add it
            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("Healed"));
            if (hediff == null)
            {
                pawn.health.AddHediff(HediffDef.Named("Healed"));
            }
            else
            {
                // Increment the severity of the existing Hediff if already applied
                hediff.Severity += 0.1f;
            }
        }

        // Method to apply emergency treatment
        private static void ApplyEmergencyTreatment(Pawn pawn)
        {
            // Apply a more severe treatment type
            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("Healed"));
            if (hediff == null)
            {
                hediff = pawn.health.AddHediff(HediffDef.Named("Healed"));
            }
            hediff.Severity = Math.Min(hediff.Severity + 0.5f, 1.0f);  // Ensure it does not exceed maximum severity
        }
    }
}
