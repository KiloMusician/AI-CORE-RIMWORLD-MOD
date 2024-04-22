using System;
using System.Collections.Generic;
using Verse;  // RimWorld's base namespace for many game-related classes
using RimWorld;  // Namespace for RimWorld-specific classes

namespace RimWorldAdvancedAIMod.AI.Algorithms
{
    public class ParticleSwarmOptimization
    {
        private List<Particle> particles = new List<Particle>();
        private int dimension;
        private double[] globalBestPosition;
        private double globalBestScore;

        public ParticleSwarmOptimization(int numberOfParticles, int dimension)
        {
            this.dimension = dimension;
            globalBestPosition = new double[dimension];
            globalBestScore = double.MaxValue;

            InitializeParticles(numberOfParticles);
        }

        private void InitializeParticles(int numberOfParticles)
        {
            for (int i = 0; i < numberOfParticles; i++)
            {
                Particle particle = new Particle(dimension);
                particles.Add(particle);

                if (particle.BestScore < globalBestScore)
                {
                    globalBestScore = particle.BestScore;
                    Array.Copy(particle.BestPosition, globalBestPosition, dimension);
                }
            }
        }

        public void Optimize()
        {
            foreach (Particle particle in particles)
            {
                particle.UpdateVelocity(globalBestPosition);
                particle.UpdatePosition();

                // Evaluate the new position
                double score = EvaluatePosition(particle.Position);
                particle.UpdatePersonalBest(score);

                // Update global best if necessary
                if (score < globalBestScore)
                {
                    globalBestScore = score;
                    Array.Copy(particle.Position, globalBestPosition, dimension);
                }
            }
        }

        private double EvaluatePosition(double[] position)
        {
            // Here you would define how you evaluate a position's score
            // For example, assessing a pawn's position for safety or resource optimization
            return position[0] * position[1];  // Simplified example
        }

        public void RunOptimizationCycle(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                Optimize();
            }
        }

        private class Particle
        {
            public double[] Position { get; private set; }
            public double[] Velocity { get; private set; }
            public double[] BestPosition { get; private set; }
            public double BestScore { get; private set; }

            private static readonly Random rnd = new Random();

            public Particle(int dimension)
            {
                Position = new double[dimension];
                Velocity = new double[dimension];
                BestPosition = new double[dimension];
                BestScore = double.MaxValue;

                InitializeRandomly(dimension);
            }

            private void InitializeRandomly(int dimension)
            {
                for (int i = 0; i < dimension; i++)
                {
                    Position[i] = rnd.NextDouble() * 100;
                    Velocity[i] = rnd.NextDouble() * 10;
                }
                BestScore = EvaluatePosition(Position);
                Array.Copy(Position, BestPosition, dimension);
            }

            public void UpdateVelocity(double[] globalBestPosition)
            {
                for (int i = 0; i < Position.Length; i++)
                {
                    double cognitiveComponent = 2.0 * rnd.NextDouble() * (BestPosition[i] - Position[i]);
                    double socialComponent = 2.0 * rnd.NextDouble() * (globalBestPosition[i] - Position[i]);
                    Velocity[i] = 0.729 * (Velocity[i] + cognitiveComponent + socialComponent);
                }
            }

            public void UpdatePosition()
            {
                for (int i = 0; i < Position.Length; i++)
                {
                    Position[i] += Velocity[i];
                }
            }

            public void UpdatePersonalBest(double score)
            {
                if (score < BestScore)
                {
                    BestScore = score;
                    Array.Copy(Position, BestPosition, Position.Length);
                }
            }
        }
    }
}
