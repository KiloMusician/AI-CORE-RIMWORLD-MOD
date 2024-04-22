using System;
using System.Collections.Generic;
using Verse;
using RimWorld;
using UnityEngine; // Add missing using statement for UnityEngine namespace

namespace RimWorldAdvancedAIMod.AI
{
    public class TaskOptimizationManager
    {
        // Method to calculate the shortest path for tasks using a simple heuristic
        public static void OptimizeTasks(Map map)
        {
            List<IntVec3> tasks = GetTasksLocations(map); // Method to collect all task locations
            IntVec3 start = map.mapPawns.FreeColonistsSpawned[0].Position; // Starting position of a free colonist
            List<IntVec3> path = HeuristicTSP(start, tasks);
            Log.Message("Optimal Task Path: " + PathToString(path));
        }

        private static List<IntVec3> HeuristicTSP(IntVec3 start, List<IntVec3> tasks)
        {
            List<IntVec3> path = new List<IntVec3>();
            IntVec3 current = start;
            path.Add(current);

            while (tasks.Count > 0)
            {
                tasks.Sort((x, y) => (current.DistanceToSquared(x).CompareTo(current.DistanceToSquared(y))));
                current = tasks[0];
                tasks.RemoveAt(0);
                path.Add(current);
            }

            return path;
        }

        private static string PathToString(List<IntVec3> path)
        {
            return string.Join(" -> ", path);
        }

        private static List<IntVec3> GetTasksLocations(Map map)
        {
            // Placeholder: Assume tasks are resource collection points
            return new List<IntVec3>
            {
                new IntVec3(10, 0, 10),
                new IntVec3(20, 0, 20),
                new IntVec3(5, 0, 15)
            };
        }
    }
}
