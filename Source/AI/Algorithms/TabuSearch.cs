using System;
using System.Collections.Generic;
using Verse;  // RimWorld's base namespace for many game-related classes

namespace RimWorldAdvancedAIMod.AI
{
    public class TabuSearch
    {
        private int maxIterations;
        private int tabuListSize;
        private int numberOfVariables;
        private Func<int[], int> objectiveFunction;
        private List<int[]> tabuList;

        public TabuSearch(int maxIterations, int tabuListSize, int numberOfVariables, Func<int[], int> objectiveFunction)
        {
            this.maxIterations = maxIterations;
            this.tabuListSize = tabuListSize;
            this.numberOfVariables = numberOfVariables;
            this.objectiveFunction = objectiveFunction;
            this.tabuList = new List<int[]>();
        }

        // Generates an initial random solution based on the problem's number of variables
        private int[] GenerateInitialSolution()
        {
            var random = new Random();
            int[] solution = new int[numberOfVariables];
            for (int i = 0; i < numberOfVariables; i++)
            {
                solution[i] = random.Next(0, 100);  // Assuming each variable's domain is from 0 to 100
            }
            return solution;
        }

        // Creates a neighbor solution by slightly modifying the current solution
        private int[] GenerateNeighbor(int[] currentSolution)
        {
            var random = new Random();
            int[] neighbor = new int[currentSolution.Length];
            currentSolution.CopyTo(neighbor, 0);
            int indexToChange = random.Next(0, numberOfVariables);
            neighbor[indexToChange] = random.Next(0, 100);  // Assuming each variable's domain is from 0 to 100
            return neighbor;
        }

        // Main method to execute the Tabu Search
        public int[] PerformSearch()
        {
            int[] bestSolution = GenerateInitialSolution();
            int bestCost = objectiveFunction(bestSolution);
            int[] currentSolution = bestSolution;
            int currentCost = bestCost;

            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                int[] neighbor = GenerateNeighbor(currentSolution);
                int neighborCost = objectiveFunction(neighbor);

                if (!IsTabu(neighbor) && neighborCost < currentCost)
                {
                    currentSolution = neighbor;
                    currentCost = neighborCost;
                    
                    if (currentCost < bestCost)
                    {
                        bestSolution = currentSolution;
                        bestCost = currentCost;
                    }

                    UpdateTabuList(neighbor);
                }

                // Log progress for debugging
                Log.Message($"Iteration {iteration}: Current Best Cost = {bestCost}");
            }

            return bestSolution;
        }

        // Checks if a solution is in the tabu list
        private bool IsTabu(int[] solution)
        {
            foreach (var tabuItem in tabuList)
            {
                if (AreSolutionsEqual(tabuItem, solution))
                    return true;
            }
            return false;
        }

        // Adds a new solution to the tabu list and ensures the list size is maintained
        private void UpdateTabuList(int[] solution)
        {
            tabuList.Add(solution);
            if (tabuList.Count > tabuListSize)
                tabuList.RemoveAt(0);
        }

        // Helper method to compare two solutions
        private bool AreSolutionsEqual(int[] solution1, int[] solution2)
        {
            for (int i = 0; i < solution1.Length; i++)
            {
                if (solution1[i] != solution2[i])
                    return false;
            }
            return true;
        }
    }
}
