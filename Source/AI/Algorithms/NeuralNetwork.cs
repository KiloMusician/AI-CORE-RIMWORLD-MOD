using System;
using System.Collections.Generic;
using Verse;  // Base namespace for many game-related classes in RimWorld
using RimWorld;  // Namespace for RimWorld-specific classes

namespace RimWorldAdvancedAIMod.AI.Algorithms
{
    public class NeuralNetwork
    {
        private int[] layers;  // Layers of this network including input, hidden, and output layers
        private float[][] neurons;  // Neuron matrix
        private float[][][] weights;  // Weight matrix
        private Random random = new Random();

        public NeuralNetwork(int[] layers)
        {
            this.layers = new int[layers.Length];
            for (int i = 0; i < layers.Length; i++)
                this.layers[i] = layers[i];

            InitializeNeurons();
            InitializeWeights();
        }

        private void InitializeNeurons()
        {
            neurons = new float[layers.Length][];
            for (int i = 0; i < layers.Length; i++)
            {
                neurons[i] = new float[layers[i]];
            }
        }

        private void InitializeWeights()
        {
            weights = new float[layers.Length - 1][][];
            for (int i = 1; i < layers.Length; i++)
            {
                weights[i - 1] = new float[neurons[i].Length][];
                for (int j = 0; j < neurons[i].Length; j++)
                {
                    weights[i - 1][j] = new float[neurons[i - 1].Length];
                    for (int k = 0; k < neurons[i - 1].Length; k++)
                    {
                        weights[i - 1][j][k] = (float)random.NextDouble() - 0.5f;  // Assign a random weight between -0.5 and 0.5
                    }
                }
            }
        }

        public float[] FeedForward(float[] inputs)
        {
            Array.Copy(inputs, neurons[0], inputs.Length);

            for (int i = 1; i < layers.Length; i++)
            {
                for (int j = 0; j < neurons[i].Length; j++)
                {
                    float value = 0f;
                    for (int k = 0; k < neurons[i - 1].Length; k++)
                    {
                        value += weights[i - 1][j][k] * neurons[i - 1][k];
                    }
                    neurons[i][j] = Sigmoid(value);
                }
            }

            return neurons[neurons.Length - 1];
        }

        private float Sigmoid(float value)
        {
            return 1f / (1f + (float)Math.Exp(-value));
        }

        private float SigmoidDerivative(float value)
        {
            float sig = Sigmoid(value);
            return sig * (1 - sig);
        }

        public void BackPropagate(float[] expected)
        {
            // Calculate output layer deltas
            float[] outputDeltas = new float[neurons[neurons.Length - 1].Length];
            for (int i = 0; i < outputDeltas.Length; i++)
            {
                float error = expected[i] - neurons[neurons.Length - 1][i];
                outputDeltas[i] = error * SigmoidDerivative(neurons[neurons.Length - 1][i]);
            }

            // Calculate deltas for hidden layers
            for (int i = neurons.Length - 2; i >= 0; i--)
            {
                float[] layerDeltas = new float[neurons[i].Length];
                for (int j = 0; j < neurons[i].Length; j++)
                {
                    float error = 0;
                    for (int k = 0; k < neurons[i + 1].Length; k++)
                    {
                        error += weights[i][k][j] * outputDeltas[k];
                    }
                    layerDeltas[j] = error * SigmoidDerivative(neurons[i][j]);
                }
                outputDeltas = layerDeltas;
            }

            // Update weights
            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    for (int k = 0; k < weights[i][j].Length; k++)
                    {
                        weights[i][j][k] += neurons[i][k] * outputDeltas[j];
                    }
                }
            }
        }
    }
}
