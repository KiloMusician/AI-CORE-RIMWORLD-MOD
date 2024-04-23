using System;
using Verse;  // Base namespace for many game-related classes in RimWorld
using RimWorld;  // Namespace for RimWorld-specific classes
using SomeExternalSVM;  // Hypothetical namespace for an external SVM library

namespace RimWorldAdvancedAIMod.AI.Algorithms
{
    // Enhanced Support Vector Machine class with better integration into RimWorld's systems
    public class SupportVectorMachine
    {
        private SVMModel model; // Hypothetical model class from an external SVM library
        private double C = 1.0; // Regularization parameter
        private double tolerance = 1e-3; // Tolerance for stopping criterion

        public SupportVectorMachine(int featureCount)
        {
            // Assuming the external library handles initialization internally
            model = new SVMModel(featureCount, C, tolerance);
        }

        // Train the SVM with sample data, adapted for RimWorld's data types
        public void Train(List<Pawn> pawns, Func<Pawn, double[]> featureSelector, Func<Pawn, int> targetSelector)
        {
            double[][] inputs = pawns.Select(pawn => featureSelector(pawn)).ToArray();
            int[] targets = pawns.Select(pawn => targetSelector(pawn)).ToArray();
            
            model.Train(inputs, targets);
            Log.Message("SVM training completed with " + pawns.Count + " data points.");
        }

        // Predict the class label for a single pawn's data
        public int Predict(Pawn pawn, Func<Pawn, double[]> featureSelector)
        {
            double[] input = featureSelector(pawn);
            return model.Predict(input);
        }

        // Evaluate the SVM on a set of test data, returning accuracy
        public double Evaluate(List<Pawn> testPawns, Func<Pawn, double[]> featureSelector, Func<Pawn, int> targetSelector)
        {
            double[][] inputs = testPawns.Select(pawn => featureSelector(pawn)).ToArray();
            int[] targets = testPawns.Select(pawn => targetSelector(pawn)).ToArray();
            
            int correctCount = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                int predicted = model.Predict(inputs[i]);
                if (predicted == targets[i])
                {
                    correctCount++;
                }
            }
            return (double)correctCount / inputs.Length;
        }
    }

    // Example usage within the RimWorld mod context
    class SVMUsageExample
    {
        public void RunExample()
        {
            SupportVectorMachine svm = new SupportVectorMachine(3); // Assume 3 features for simplicity

            List<Pawn> pawns = Find.AllMaps.SelectMany(map => map.mapPawns.AllPawnsSpawned).ToList();
            svm.Train(pawns, ExtractFeatures, DetermineTarget);
            double accuracy = svm.Evaluate(pawns, ExtractFeatures, DetermineTarget);

            Log.Message("SVM Evaluation Accuracy: " + accuracy.ToString("P2"));
        }

        private double[] ExtractFeatures(Pawn pawn)
        {
            // Extract features from a pawn based on your criteria, e.g., health, mood, equipment
            return new double[] {
                pawn.health.summaryHealth.SummaryHealthPercent,
                (double)pawn.needs.mood.CurLevelPercentage,
                pawn.equipment.Primary?.MarketValue ?? 0
            };
        }

        private int DetermineTarget(Pawn pawn)
        {
            // Determine target class based on your criteria, e.g., aggressive vs non-aggressive
            return pawn.MentalStateDef == MentalStateDefOf.Berserk ? 1 : 0;
        }
    }
}
