using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace Source.AITemple.Tasks
{
    public class TaskOptimizationManager
    {
        private int memorySize;
        private double harmonyMemoryConsiderationRate;
        private double pitchAdjustingRate;
        private List<int[]> harmonyMemory;
        private Random rand = new Random();

        public List<Task> TaskList { get; private set; }
        public int TaskCount => TaskList.Count;

        public TaskOptimizationManager(int memorySize, double considerationRate, double pitchRate, List<Task> tasks)
        {
            this.memorySize = memorySize;
            this.harmonyMemoryConsiderationRate = considerationRate;
            this.pitchAdjustingRate = pitchRate;
            this.TaskList = tasks;
            this.harmonyMemory = new List<int[]>(memorySize);
        }

        public void InitializeTaskAssignments()
        {
            for (int i = 0; i < memorySize; i++)
            {
                int[] assignment = new int[TaskCount];
                for (int taskIndex = 0; taskIndex < TaskCount; taskIndex++)
                {
                    assignment[taskIndex] = rand.Next(0, TaskCount); // Random assignment
                }
                harmonyMemory.Add(assignment);
            }
        }

        public int[] GenerateNewHarmony()
        {
            int[] newHarmony = new int[TaskCount];
            for (int taskIndex = 0; taskIndex < TaskCount; taskIndex++)
            {
                if (rand.NextDouble() < harmonyMemoryConsiderationRate)
                {
                    int memoryIndex = rand.Next(memorySize);
                    newHarmony[taskIndex] = harmonyMemory[memoryIndex][taskIndex];

                    if (rand.NextDouble() < pitchAdjustingRate)
                    {
                        newHarmony[taskIndex] = (newHarmony[taskIndex] + rand.Next(-1, 2)) % TaskCount; // Adjust the assignment randomly
                    }
                }
                else
                {
                    newHarmony[taskIndex] = rand.Next(0, TaskCount); // Random assignment
                }
            }
            return newHarmony;
        }

        public void UpdateHarmonyMemory(int[] newHarmony)
        {
            int worstIndex = 0;
            double worstValue = CalculateObjectiveValue(harmonyMemory[0]);
            for (int i = 1; i < memorySize; i++)
            {
                double currentValue = CalculateObjectiveValue(harmonyMemory[i]);
                if (currentValue > worstValue)
                {
                    worstValue = currentValue;
                    worstIndex = i;
                }
            }

            double newHarmonyValue = CalculateObjectiveValue(newHarmony);
            if (newHarmonyValue < worstValue)
            {
                harmonyMemory[worstIndex] = newHarmony;
            }
        }

        private double CalculateObjectiveValue(int[] assignment)
        {
            // Calculate the objective value based on the task assignments
            // This function should be defined according to specific optimization needs
            double result = 0;
            // Example: Sum of task priorities
            for (int taskIndex = 0; taskIndex < TaskCount; taskIndex++)
            {
                result += TaskList[taskIndex].Priority;
            }
            return result;
        }

        private int[] GetOptimizedTaskAssignments()
        {
            int bestIndex = 0;
            double bestValue = CalculateObjectiveValue(harmonyMemory[0]);
            for (int i = 1; i < memorySize; i++)
            {
                double currentValue = CalculateObjectiveValue(harmonyMemory[i]);
                if (currentValue < bestValue)
                {
                    bestValue = currentValue;
                    bestIndex = i;
                }
            }

            return harmonyMemory[bestIndex];
        }

        public void RunOptimization()
        {
            InitializeTaskAssignments();
            for (int iteration = 0; iteration < 100; iteration++)
            {
                int[] newHarmony = GenerateNewHarmony();
                UpdateHarmonyMemory(newHarmony);
            }
            Log.Message("Task Optimization Completed.");
        }
    }
}
