using RimWorld;
using Verse;
using System.Collections.Generic;

namespace RimWorldMod.AITemple {
    public static class AIBehaviorHandler {
        private static readonly List<IAIBehavior> registeredBehaviors = new List<IAIBehavior>();

        public static void RegisterBehavior(IAIBehavior behavior) {
            if (!registeredBehaviors.Contains(behavior)) {
                registeredBehaviors.Add(behavior);
                behavior.Initialize();
                Log.Message($"Behavior registered: {behavior.GetType().Name}");
            }
        }

        public static void UpdateAllBehaviors() {
            registeredBehaviors.ForEach(behavior => {
                try {
                    behavior.Update();
                    Log.Message($"Behavior updated: {behavior.GetType().Name}");
                } catch (System.Exception ex) {
                    Log.Error($"Error updating behavior {behavior.GetType().Name}: {ex}");
                }
            });
        }

        // Simplified and focused method without direct Pawn health manipulation
        public static void ApplyAISettings() {
            var settings = LoadedModManager.GetMod<Mod_AIOmniCore>().GetSettings<ModSettings_AIOmniCore>();
            Log.Message($"AI settings applied: Work speed modifier set to {settings.aiWorkSpeedModifier:P0}");
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
    }
}
