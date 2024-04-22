using System;
using System.Collections.Generic;
using Verse;  // Base namespace for many game-related classes in RimWorld
using RimWorld;  // Namespace for RimWorld-specific classes

namespace RimWorldAdvancedAIMod.AI.Algorithms
{
    public class ArtificialBeeColony
    {
        private readonly int colonySize;
        private readonly int maxGenerations;
        private readonly Func<int[], double> objectiveFunction;
        private List<Bee> bees;

        // Constructor to initialize the bee colony
        public ArtificialBeeColony(int colonySize, int maxGenerations, Func<int[], double> objectiveFunction)
        {
            this.colonySize = colonySize;
            this.maxGenerations = maxGenerations;
            this.objectiveFunction = objectiveFunction;
            bees = new List<Bee>(colonySize);
        }

        // Represents a single bee in the colony
        private class Bee
        {
            public int[] Solution;
            public double Fitness;

            public Bee(int[] solution, double fitness)
            {
                Solution = solution;
                Fitness = fitness;
            }
        }

        // Initializes the bee colony with random solutions
        private void InitializeBees(int solutionLength)
        {
            for (int i = 0; i < colonySize; i++)
            {
                int[] solution = new int[solutionLength];
                for (int j = 0; j < solution.Length; j++)
                {
                    solution[j] = Rand.Range(0, 2);  // Assuming binary decision variables for simplicity
                }

                double fitness = objectiveFunction(solution);
                bees.Add(new Bee(solution, fitness));
            }
        }

        // The main algorithm execution method
        public void RunOptimization()
        {
            InitializeBees(10);  // Initialize with a solution length of 10 for example purposes

            for (int generation = 0; generation < maxGenerations; generation++)
            {
                foreach (var bee in bees)
                {
                    // Employed bees phase
                    int[] newSolution = MutateSolution(bee.Solution);
                    double newFitness = objectiveFunction(newSolution);
                    
                    // Greedy selection
                    if (newFitness > bee.Fitness)
                    {
                        bee.Solution = newSolution;
                        bee.Fitness = newFitness;
                    }
                }

                // Optional: Onlooker bees phase and scout bees phase can be added here

                Log.Message($"Generation {generation}: Best fitness = {GetBestFitness()}");
            }
        }

        // Mutation operation to generate a neighbor solution
        private int[] MutateSolution(int[] currentSolution)
        {
            int[] newSolution = (int[])currentSolution.Clone();
            int mutationPoint = Rand.Range(0, newSolution.Length);
            newSolution[mutationPoint] = 1 - newSolution[mutationPoint];  // Flip the binary value
            return newSolution;
        }

        // Utility to fetch the best fitness from the colony
        private double GetBestFitness()
        {
            double bestFitness = double.MinValue;
            foreach (var bee in bees)
            {
                if (bee.Fitness > bestFitness)
                {
                    bestFitness = bee.Fitness;
                }
            }
            return bestFitness;
        }
    }
}
