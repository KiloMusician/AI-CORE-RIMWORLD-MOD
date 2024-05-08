using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace RimWorldMod.AITemple {
    public static class AIBehaviorHandler {
        private static List<IAIBehavior> registeredBehaviors = new List<IAIBehavior>();

        // Dynamically register behaviors at runtime
        public static void RegisterBehavior(IAIBehavior behavior) {
            if (!registeredBehaviors.Contains(behavior)) {
                registeredBehaviors.Add(behavior);
                behavior.Initialize();
                Log.Message($"Behavior registered: {behavior.GetType().Name}");
            }
        }

        // Update all behaviors, typically called each game tick or in response to specific events
        public static void UpdateAllBehaviors() {
            registeredBehaviors.ForEach(behavior => {
                behavior.Update();
                Log.Message($"Behavior updated: {behavior.GetType().Name}");
            });
        }

        // Apply AI behavior settings across the game
        public static void ApplyAISettings() {
            var settings = LoadedModManager.GetMod<Mod_AIOmniCore>().GetSettings<ModSettings_AIOmniCore>();
            PawnsFinder.AllMaps_FreeColonists.ForEach(colonist => {
                if (colonist.health.capacities.CapableOf(PawnCapacityDefOf.Manipulation)) {
                    var statModifier = new StatModifier {
                        stat = StatDefOf.WorkSpeedGlobal,
                        value = settings.aiWorkSpeedModifier - 1
                    };
                    ApplyStatModifier(colonist, statModifier);
                }
            });
            Log.Message($"AI settings applied: Work speed adjusted to {settings.aiWorkSpeedModifier:P0}");
        }

        private static void ApplyStatModifier(Pawn pawn, StatModifier modifier) {
            pawn.health.AddHediff(HediffMaker.MakeHediff(HediffDefOf.Hypothermia, pawn));
            if (pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Hypothermia) is Hediff hediff) {
                hediff.Severity = modifier.value;
            }
        }

        public interface IAIBehavior {
            void Initialize();
            void Update();
            void ApplySettings();
        }

        public class PathfindingBehavior : IAIBehavior {
            public void Initialize() => Log.Message("Pathfinding Behavior Initialized");
            public void Update() => Log.Message("Pathfinding Behavior Updated");
            public void ApplySettings() => Log.Message("Pathfinding Settings Applied");
        }

        [HarmonyPatch(typeof(ResearchManager), "FinishProject")]
        public static class Patch_ResearchManager_FinishProject {
            static void Postfix() => ApplyAISettings();
        }
    }
}
