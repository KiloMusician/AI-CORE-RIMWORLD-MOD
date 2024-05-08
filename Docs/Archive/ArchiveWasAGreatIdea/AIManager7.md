using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Verse;

namespace RimWorldMod.AITemple {
    public class AIManager {
        private static AIManager _instance;
        public static AIManager Instance => _instance ?? (_instance = new AIManager());
        private List<IAIBehavior> behaviors;

        private AIManager() {
            behaviors = new List<IAIBehavior>();
            RegisterInitialBehaviors();
            Log.Message("AI Manager initialized and initial behaviors registered.");
        }

        public void RegisterBehavior(IAIBehavior behavior) {
            if (!behaviors.Contains(behavior)) {
                try {
                    behaviors.Add(behavior);
                    behavior.Initialize();
                    Log.Message($"AI Behavior Registered: {behavior.GetType().Name}");
                } catch (Exception ex) {
                    Log.Error($"Failed to initialize behavior {behavior.GetType().Name}: {ex}");
                }
            }
        }

        private void RegisterInitialBehaviors() {
            var behaviorTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
                .Where(t => typeof(IAIBehavior).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            foreach (var type in behaviorTypes) {
                try {
                    var behavior = Activator.CreateInstance(type) as IAIBehavior;
                    RegisterBehavior(behavior);
                } catch (Exception ex) {
                    Log.Error($"Failed to create behavior {type.Name}: {ex}");
                }
            }
        }

        public async void UpdateBehaviorsAsync() {
            var tasks = behaviors.Select(b => Task.Run(() => b.Update())).ToList();
            try {
                await Task.WhenAll(tasks);
                Log.Message("AI Behaviors Updated Successfully.");
            } catch (Exception ex) {
                Log.Error($"Error updating behaviors: {ex}");
            }
        }

        public void AnalyzeAndOptimize() {
            foreach (var behavior in behaviors.Where(b => b.IsCritical)) {
                try {
                    behavior.Optimize();
                } catch (Exception ex) {
                    Log.Error($"Error optimizing behavior {behavior.GetType().Name}: {ex}");
                }
            }
            Log.Message("AI Behaviors Analyzed and Optimized.");
        }
    }

    public interface IAIBehavior {
        bool IsCritical { get; }
        void Initialize();
        void Update();
        void Optimize();
    }

    class AutonomousColonistBehavior : IAIBehavior {
        public bool IsCritical => true;
        public void Initialize() {
            Log.Message("Autonomous Colonist Behavior Initialized.");
        }
        public void Update() { }
        public void Optimize() {
            Log.Message("Optimizing Autonomous Colonist Behavior.");
        }
    }

    class DynamicEventResponseBehavior : IAIBehavior {
        public bool IsCritical => false;
        public void Initialize() {
            Log.Message("Dynamic Event Response Behavior Initialized.");
        }
        public void Update() { }
        public void Optimize() {
            Log.Message("Optimizing Dynamic Event Response Behavior.");
        }
    }
}
