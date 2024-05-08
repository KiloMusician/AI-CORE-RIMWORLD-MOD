using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace RimWorldMod.AITemple
{
    public static class AIBehaviorHandler
    {
        private static List<IAIBehavior> registeredBehaviors = new List<IAIBehavior>();

        // Register behaviors dynamically at runtime
        public static void RegisterBehavior(IAIBehavior behavior)
        {
            if (!registeredBehaviors.Contains(behavior))
            {
                registeredBehaviors.Add(behavior);
                behavior.Initialize();
                Log.Message($"Behavior registered: {behavior.GetType().Name}");
            }
        }

        // Update all behaviors, typically called each game tick or in response to specific events
        public static void UpdateAllBehaviors()
        {
            foreach (var behavior in registeredBehaviors)
            {
                behavior.Update();
                Log.Message($"Behavior updated: {behavior.GetType().Name}");
            }
        }

        // Apply AI behavior settings across the game
        public static void ApplyAISettings()
        {
            var settings = LoadedModManager.GetMod<Mod_AIOmniCore>().GetSettings<ModSettings_AIOmniCore>();

            // Adjust all colonists' work speed based on the AI work speed modifier
            foreach (Pawn colonist in PawnsFinder.AllMaps_FreeColonists)
            {
                var statModifier = new StatModifier {
                    stat = StatDefOf.WorkSpeedGlobal,
                    value = settings.aiWorkSpeedModifier - 1  // Adjusting by the difference from default value
                };

                if (colonist.health.capacities.CapableOf(PawnCapacityDefOf.Manipulation))
                {
                    ApplyStatModifier(colonist, statModifier);
                }
            }

            Log.Message("AI settings applied: Work speed adjusted to " + settings.aiWorkSpeedModifier.ToString("P0"));
        }

        // Method to apply a stat modifier to a pawn
        private static void ApplyStatModifier(Pawn pawn, StatModifier modifier)
        {
            pawn.health.AddHediff(HediffMaker.MakeHediff(HediffDefOf.Hypothermia, pawn));
            Hediff hediff = pawn.health.hediffSet.hediffs.FirstOrDefault(h => h.def == HediffDefOf.Hypothermia);
            if (hediff != null)
            {
                hediff.Severity = modifier.value;
            }
        }

        // Interface that all AI behaviors must implement
        public interface IAIBehavior
        {
            void Initialize();
            void Update();
            void ApplySettings();
        }

        // Example of a specific AI behavior implementation
        public class PathfindingBehavior : IAIBehavior
        {
            public void Initialize()
            {
                Log.Message("Pathfinding Behavior Initialized");
            }

            public void Update()
            {
                // Update logic for pathfinding, such as recalculating paths
            }

            public void ApplySettings()
            {
                // Apply dynamic settings, possibly adjusting algorithm parameters
                Log.Message("Pathfinding Settings Applied");
            }
        }

        // Ensure that AI settings are applied at appropriate game events
        [HarmonyPatch(typeof(ResearchManager), "FinishProject")]
        public static class Patch_ResearchManager_FinishProject
        {
            static void Postfix()
            {
                ApplyAISettings();
            }
        }
    }
}
