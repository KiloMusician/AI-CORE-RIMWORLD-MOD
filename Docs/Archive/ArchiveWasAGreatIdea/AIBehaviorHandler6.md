using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Verse;

public static class AIBehaviorHandler {
    private static readonly List<IAIBehavior> registeredBehaviors = new List<IAIBehavior>();

    public static void RegisterBehavior(IAIBehavior behavior) {
        if (!registeredBehaviors.Contains(behavior)) {
            registeredBehaviors.Add(behavior);
            try {
                behavior.Initialize();
                Log.Message($"Behavior registered: {behavior.GetType().Name}");
            } catch (Exception ex) {
                Log.Error($"Failed to initialize behavior {behavior.GetType().Name}: {ex}");
            }
        }
    }

    public static async void UpdateAllBehaviorsAsync() {
        List<Task> tasks = new List<Task>();
        foreach (var behavior in registeredBehaviors) {
            var task = Task.Run(() => {
                try {
                    behavior.Update();
                    Log.Message($"Behavior updated: {behavior.GetType().Name}");
                } catch (Exception ex) {
                    Log.Error($"Error updating behavior {behavior.GetType().Name}: {ex}");
                }
            });
            tasks.Add(task);
        }
        try {
            await Task.WhenAll(tasks);
        } catch (Exception ex) {
            Log.Error($"One or more behavior updates failed: {ex}");
        }
    }

    public interface IAIBehavior {
        void Initialize();
        void Update();
        void ApplySettings();  // Ensure implementations detail when and how settings are applied.
    }

    public class PathfindingBehavior : IAIBehavior {
        public void Initialize() => Log.Message("Pathfinding Behavior Initialized");
        public void Update() => Log.Message("Pathfinding Behavior Updated");
        public void ApplySettings() => Log.Message("Settings applied to Pathfinding Behavior");
    }
}
