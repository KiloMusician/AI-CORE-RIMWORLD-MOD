using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Buffers;
using System.Threading;

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
            globalBestPosition = ArrayPool<double>.Shared.Rent(dimension);
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
            Parallel.ForEach(particles, particle =>
            {
                particle.UpdateVelocity(globalBestPosition);
                particle.UpdatePosition();

                double score = EvaluatePosition(particle.Position);
                particle.UpdatePersonalBest(score);

                if (score < globalBestScore)
                {
                    globalBestScore = score;
                    Array.Copy(particle.Position, globalBestPosition, dimension);
                }
            });
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

        ~ParticleSwarmOptimization()
        {
            ArrayPool<double>.Shared.Return(globalBestPosition);
        }

        private class Particle
        {
            public double[] Position { get; private set; }
            public double[] Velocity { get; private set; }
            public double[] BestPosition { get; private set; }
            public double BestScore { get; private set; }

            private static readonly Lazy<Random> rnd = new Lazy<Random>(() => new Random(), LazyThreadSafetyMode.ExecutionAndPublication);

            public Particle(int dimension)
            {
                Position = ArrayPool<double>.Shared.Rent(dimension);
                Velocity = ArrayPool<double>.Shared.Rent(dimension);
                BestPosition = ArrayPool<double>.Shared.Rent(dimension);
                BestScore = double.MaxValue;

                InitializeRandomly(dimension);
            }

            private void InitializeRandomly(int dimension)
            {
                for (int i = 0; i < dimension; i++)
                {
                    Position[i] = rnd.Value.NextDouble() * 100;
                    Velocity[i] = rnd.Value.NextDouble() * 10;
                }
                BestScore = EvaluatePosition(Position);
                Array.Copy(Position, BestPosition, dimension);
            }

            public void UpdateVelocity(double[] globalBestPosition)
            {
                for (int i = 0; i < Position.Length; i++)
                {
                    double cognitiveComponent = 2.0 * rnd.Value.NextDouble() * (BestPosition[i] - Position[i]);
                    double socialComponent = 2.0 * rnd.Value.NextDouble() * (globalBestPosition[i] - Position[i]);
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

            ~Particle()
            {
                ArrayPool<double>.Shared.Return(Position);
                ArrayPool<double>.Shared.Return(Velocity);
                ArrayPool<double>.Shared.Return(BestPosition);
            }
        }
    }
}
