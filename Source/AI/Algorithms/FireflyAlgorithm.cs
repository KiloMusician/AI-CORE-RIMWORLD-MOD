using System;
using System.Collections.Generic;
using Verse;  // Base namespace for many game-related classes in RimWorld
using UnityEngine;  // Necessary for mathematical computations and vector handling

namespace RimWorldAdvancedAIMod.AI
{
    public class FireflyAlgorithm
    {
        private int populationSize;
        private float attractionCoefficient;
        private float absorptionCoefficient;
        private float randomizationParameter;
        private List<Firefly> fireflies;

        public FireflyAlgorithm(int populationSize, float attractionCoefficient, float absorptionCoefficient, float randomizationParameter)
        {
            this.populationSize = populationSize;
            this.attractionCoefficient = attractionCoefficient;
            this.absorptionCoefficient = absorptionCoefficient;
            this.randomizationParameter = randomizationParameter;
            fireflies = new List<Firefly>();

            InitializePopulation();
        }

        // Initialize fireflies with random positions
        private void InitializePopulation()
        {
            for (int i = 0; i < populationSize; i++)
            {
                Vector3 position = new Vector3(Rand.Value, Rand.Value, Rand.Value); // Random positions in a hypothetical 3D space
                fireflies.Add(new Firefly(position));
            }
        }

        // Main method to run the algorithm
        public void RunIteration()
        {
            foreach (var firefly in fireflies)
            {
                foreach (var otherFirefly in fireflies)
                {
                    if (firefly != otherFirefly && firefly.Intensity < otherFirefly.Intensity)
                    {
                        MoveFirefly(firefly, otherFirefly);
                    }
                }
            }

            // Optional: Log current best solution or positions of fireflies
            LogBestSolution();
        }

        // Move a firefly towards another based on attractiveness
        private void MoveFirefly(Firefly firefly, Firefly otherFirefly)
        {
            Vector3 direction = otherFirefly.Position - firefly.Position;
            float distance = direction.magnitude;
            float lightIntensity = Mathf.Exp(-absorptionCoefficient * Mathf.Pow(distance, 2));

            firefly.Position += attractionCoefficient * lightIntensity * direction + randomizationParameter * (Rand.Value - 0.5f) * Vector3.one;
        }

        // Log or visualize the best solution
        private void LogBestSolution()
        {
            Firefly best = fireflies[0];
            foreach (var firefly in fireflies)
            {
                if (firefly.Intensity > best.Intensity)
                {
                    best = firefly;
                }
            }

            Log.Message($"Best firefly position: {best.Position} with intensity: {best.Intensity}");
        }

        // Represents an individual firefly
        private class Firefly
        {
            public Vector3 Position;
            public float Intensity;  // Could be determined by the problem context

            public Firefly(Vector3 position)
            {
                Position = position;
                Intensity = CalculateIntensity(position);
            }

            // Method to calculate intensity based on position
            private float CalculateIntensity(Vector3 position)
            {
                // Hypothetical intensity function based on the position
                return Mathf.Max(1.0f - position.magnitude, 0);
            }
        }
    }
}
