using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace RimWorldAdvancedAIMod.AI
{
    public class GeneticProgramming
    {
        private List<Individual> population;
        private int populationSize = 50;
        private float mutationRate = 0.01f;
        private float crossoverRate = 0.7f;
        private int generations = 100;

        public GeneticProgramming()
        {
            InitializePopulation();
        }

        private void InitializePopulation()
        {
            population = new List<Individual>();
            for (int i = 0; i < populationSize; i++)
            {
                var individual = new Individual();
                individual.RandomizeGenes();
                population.Add(individual);
            }
        }

        public void Evolve()
        {
            for (int g = 0; g < generations; g++)
            {
                List<Individual> newPopulation = new List<Individual>();

                for (int i = 0; i < population.Count; i++)
                {
                    var parent1 = SelectParent();
                    var parent2 = SelectParent();
                    var child = Crossover(parent1, parent2);
                    Mutate(child);
                    newPopulation.Add(child);
                }

                population = newPopulation;
                EvaluatePopulation();
            }
        }

        private Individual SelectParent()
        {
            return population.RandomElementByWeight(p => p.Fitness);
        }

        private Individual Crossover(Individual parent1, Individual parent2)
        {
            if (Rand.Value < crossoverRate)
            {
                return parent1.Combine(parent2);
            }
            return Rand.Bool ? parent1 : parent2;
        }

        private void Mutate(Individual individual)
        {
            if (Rand.Value < mutationRate)
            {
                individual.Mutate();
            }
        }

        private void EvaluatePopulation()
        {
            foreach (var individual in population)
            {
                individual.Fitness = CalculateFitness(individual);
            }
        }

        private float CalculateFitness(Individual individual)
        {
            // Evaluate the fitness of the individual in the context of RimWorld
            // Example: Simulate the behavior of the NPC using the individual's genes
            // and calculate a fitness score based on the NPC's performance in the simulation
            return individual.Evaluate();
        }
    }

    public class Individual
    {
        public float Fitness { get; set; }
        public List<int> Genes { get; set; }

        public Individual()
        {
            Genes = new List<int>();
        }

        public void RandomizeGenes()
        {
            // Randomize genes at initialization
            for (int i = 0; i < 10; i++)
            {
                Genes.Add(Rand.RangeInclusive(0, 1));
            }
        }

        public Individual Combine(Individual other)
        {
            // Combine genes of two parents
            var child = new Individual();
            int midpoint = Genes.Count / 2;
            for (int i = 0; i < Genes.Count; i++)
            {
                child.Genes.Add(i < midpoint ? this.Genes[i] : other.Genes[i]);
            }
            return child;
        }

        public void Mutate()
        {
            // Mutate a gene
            int geneIndex = Rand.RangeInclusive(0, Genes.Count - 1);
            Genes[geneIndex] = 1 - Genes[geneIndex];
        }

        public float Evaluate()
        {
            // Evaluate the fitness of the individual in the context of RimWorld
            // Example: Simulate the behavior of the NPC using the individual's genes
            // and calculate a fitness score based on the NPC's performance in the simulation
            return Genes.Count(g => g == 1);
        }
    }
}
