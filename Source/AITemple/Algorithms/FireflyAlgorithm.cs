using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.AITemple.Algorithms
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
                Vector3 position = GenerateRandomPosition(); // Random positions in a hypothetical 3D space
                fireflies.Add(new Firefly(position));
            }
        }

        // Generate a random position in a hypothetical 3D space
        private Vector3 GenerateRandomPosition()
        {
            return new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
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

            LogBestSolution();
        }

        // Move a firefly towards another based on attractiveness
        private void MoveFirefly(Firefly firefly, Firefly otherFirefly)
        {
            Vector3 direction = otherFirefly.Position - firefly.Position;
            float distance = direction.magnitude;
            float lightIntensity = CalculateLightIntensity(distance);

            firefly.Position += attractionCoefficient * lightIntensity * direction + randomizationParameter * (UnityEngine.Random.value - 0.5f) * Vector3.one;
        }

        // Calculate light intensity based on distance
        private float CalculateLightIntensity(float distance)
        {
            return Mathf.Exp(-absorptionCoefficient * Mathf.Pow(distance, 2));
        }

        // Log the best solution
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

            Debug.Log($"Best firefly position: {best.Position} with intensity: {best.Intensity}");
        }

        // Represents an individual firefly
        private class Firefly
        {
            public Vector3 Position;
            public float Intensity;

            public Firefly(Vector3 position)
            {
                Position = position;
                Intensity = CalculateIntensity(position);
            }

            // Calculate intensity based on position
            private float CalculateIntensity(Vector3 position)
            {
                return Mathf.Max(1.0f - position.magnitude, 0);
            }
        }
    }
}
