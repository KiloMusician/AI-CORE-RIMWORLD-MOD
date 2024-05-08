using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Verse;

public static class AIBehaviorHandler {
    private static readonly List<IAIBehavior> registeredBehaviors = new List<IAIBehavior>();

    public static void RegisterBehavior(IAIBehavior behavior) {
        if (!registeredBehaviors.Contains(behavior)) {
            registeredBehaviors.Add(behavior);
            behavior.Initialize();
            Log.Message($"Behavior registered: {behavior.GetType().Name}");
        }
    }

    // Asynchronously update all behaviors
    public static async void UpdateAllBehaviorsAsync() {
        List<Task> tasks = new List<Task>();
        foreach (var behavior in registeredBehaviors) {
            tasks.Add(Task.Run(() => {
                try {
                    behavior.Update();
                    Log.Message($"Behavior updated: {behavior.GetType().Name}");
                } catch (Exception ex) {
                    Log.Error($"Error updating behavior {behavior.GetType().Name}: {ex}");
                }
            }));
        }
        await Task.WhenAll(tasks); // Wait for all tasks to complete
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
