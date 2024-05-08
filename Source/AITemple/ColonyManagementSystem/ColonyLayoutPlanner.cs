using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace Source.AITemple.ColonyManagementSystem
{
    // Manages the blueprint planning and modular expansion of the colony
    public static class ColonyLayoutPlanner
    {
        // Store for planned layouts and expansion modules
        private static Dictionary<string, DesignModule> colonyModules = new Dictionary<string, DesignModule>();

        // Initialize the planner with default colony layout
        public static void InitializeDefaultLayout()
        {
            // Define basic zones: Housing, Farming, Production
            RegisterModule("Housing", new Vector2(1, 1), new Vector2(10, 10));
            RegisterModule("Farming", new Vector2(12, 1), new Vector2(20, 10));
            RegisterModule("Production", new Vector2(1, 12), new Vector2(10, 20));
            Log.Message("Default colony layout initialized.");
        }

        // Method to register new modular sections
        public static void RegisterModule(string moduleName, Vector2 start, Vector2 end)
        {
            if (!colonyModules.ContainsKey(moduleName))
            {
                colonyModules[moduleName] = new DesignModule(start, end);
                Log.Message($"Module {moduleName} registered: {start} to {end}");
            }
        }

        // Adjust existing module dimensions
        public static void AdjustModule(string moduleName, Vector2 newStart, Vector2 newEnd)
        {
            if (colonyModules.TryGetValue(moduleName, out DesignModule module))
            {
                module.AdjustDimensions(newStart, newEnd);
                Log.Message($"Module {moduleName} adjusted: {newStart} to {newEnd}");
            }
        }

        // Display all current modules
        public static void DisplayModules()
        {
            foreach (var module in colonyModules)
            {
                Log.Message($"Module {module.Key}: Start {module.Value.StartPoint} to End {module.Value.EndPoint}");
            }
        }
    }

    // Represents a modular design segment of the colony
    public class DesignModule
    {
        public Vector2 StartPoint { get; private set; }
        public Vector2 EndPoint { get; private set; }

        public DesignModule(Vector2 start, Vector2 end)
        {
            StartPoint = start;
            EndPoint = end;
        }

        // Adjust the dimensions of the module
        public void AdjustDimensions(Vector2 newStart, Vector2 newEnd)
        {
            StartPoint = newStart;
            EndPoint = newEnd;
        }
    }
}
