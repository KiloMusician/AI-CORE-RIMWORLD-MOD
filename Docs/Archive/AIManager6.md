using System;
using System.Collections.Generic;
using System.Linq;
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
                behaviors.Add(behavior);
                behavior.Initialize();
                Log.Message($"AI Behavior Registered: {behavior.GetType().Name}");
            }
        }
        private void RegisterInitialBehaviors() {
            // Dynamically load behaviors to allow for easy expansion
            var behaviorTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
                .Where(t => typeof(IAIBehavior).IsAssignableFrom(t) && !t.IsInterface);
            foreach (var type in behaviorTypes) {
                var behavior = Activator.CreateInstance(type) as IAIBehavior;
                RegisterBehavior(behavior);
            }
        }
        public void UpdateBehaviors() {
            behaviors.ForEach(behavior => behavior.Update());
            Log.Message("AI Behaviors Updated Successfully.");
        }
        public void AnalyzeAndOptimize() {
            behaviors.Where(b => b.IsCritical).ToList().ForEach(behavior => behavior.Optimize());
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



