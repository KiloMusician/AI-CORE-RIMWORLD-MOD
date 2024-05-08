using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Verse;

namespace Source.AITemple.CoreAISystem
{
    public static class AIBehaviorHandler 
    {
        private static readonly List<IAIBehavior> registeredBehaviors = new List<IAIBehavior>();

        public static void RegisterBehavior(IAIBehavior behavior) 
        {
            if (!registeredBehaviors.Contains(behavior)) 
            {
                registeredBehaviors.Add(behavior);
                try 
                {
                    behavior.Initialize();
                    Log.Message($"Behavior registered: {behavior.GetType().Name}");
                } 
                catch (Exception ex) 
                {
                    Log.Error($"Failed to initialize behavior {behavior.GetType().Name}: {ex}");
                }
            }
        }

        public static async Task UpdateAllBehaviorsAsync() 
        {
            List<Task> tasks = new List<Task>();
            foreach (var behavior in registeredBehaviors) 
            {
                var task = Task.Run(() => 
                {
                    try 
                    {
                        behavior.Update();
                        Log.Message($"Behavior updated: {behavior.GetType().Name}");
                    } 
                    catch (Exception ex) 
                    {
                        Log.Error($"Error updating behavior {behavior.GetType().Name}: {ex}");
                    }
                });
                tasks.Add(task);
            }
            try 
            {
                await Task.WhenAll(tasks);
            } 
            catch (AggregateException ex) 
            {
                foreach (var e in ex.InnerExceptions) 
                {
                    Log.Error($"One or more behavior updates failed: {e.Message}");
                }
            }
        }

        public interface IAIBehavior 
        {
            void Initialize();
            void Update();
            void ApplySettings();  // Clarified settings application process.
        }

        public class PathfindingBehavior : IAIBehavior 
        {
            public void Initialize() => Log.Message("Pathfinding Behavior Initialized");
            public void Update() => Log.Message("Pathfinding Behavior Updated");
            public void ApplySettings() => Log.Message("Settings applied to Pathfinding Behavior");
        }
    }
}