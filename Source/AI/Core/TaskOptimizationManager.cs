using System;
using System.Collections.Generic;
using Verse;  // RimWorld's base namespace for managing game components
using RimWorld;  // Namespace for RimWorld-specific classes
using UnityEngine; // Added for potential future enhancements requiring Unity's libraries

namespace RimWorldAdvancedAIMod.AI
{
    public class TaskOptimizationManager
    {
        // Optimizes task execution by finding an efficient route among various task locations
        public static void OptimizeTasks(Map map)
        {
            if (map.mapPawns.FreeColonistsSpawned.Count == 0)
            {
                Log.Warning("No free colonists available to perform tasks.");
                return;
            }

            List<IntVec3> tasks = GetTasksLocations(map); // Fetch all task locations
            IntVec3 start = map.mapPawns.FreeColonistsSpawned[0].Position; // Start at the first free colonist's position
            List<IntVec3> path = HeuristicTSP(start, tasks);
            Log.Message("Optimal Task Path: " + PathToString(path));
        }

        // Applies a simple nearest neighbor heuristic to determine an efficient path for task completion
        private static List<IntVec3> HeuristicTSP(IntVec3 start, List<IntVec3> tasks)
        {
            List<IntVec3> path = new List<IntVec3> { start };
            IntVec3 current = start;

            while (tasks.Count > 0)
            {
                IntVec3 nextTask = FindClosestTask(current, tasks);
                tasks.Remove(nextTask);
                path.Add(nextTask);
                current = nextTask;
            }

            return path;
        }

        // Find the closest task to the current position
        private static IntVec3 FindClosestTask(IntVec3 current, List<IntVec3> tasks)
        {
            IntVec3 closest = tasks[0];
            float minDistance = float.MaxValue;

            foreach (var task in tasks)
            {
                float distance = (task - current).LengthHorizontalSquared;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = task;
                }
            }

            return closest;
        }

        // Converts a list of grid positions into a human-readable string format
        private static string PathToString(List<IntVec3> path)
        {
            return string.Join(" -> ", path.ConvertAll(p => $"({p.x}, {p.z})"));
        }

        // Retrieves task locations, which could be resource collection points, repair sites, etc.
        private static List<IntVec3> GetTasksLocations(Map map)
        {
            // Example: Select resource collection points
            return new List<IntVec3>
            {
                new IntVec3(10, 0, 10),
                new IntVec3(20, 0, 20),
                new IntVec3(5, 0, 15)
            };
        }
    }
}
