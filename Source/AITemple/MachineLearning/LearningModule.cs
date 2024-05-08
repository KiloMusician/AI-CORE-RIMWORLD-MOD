using System;
using Verse;  // Ensures access to the Verse namespace
using RimWorld;  // Ensures access to the RimWorld namespace

namespace Source.AITemple.MachineLearning
{
    public class LearningModule
    {
        private NLPProcessor nlpProcessor;

        // Constructor that initializes the NLPProcessor
        public LearningModule()
        {
            nlpProcessor = new NLPProcessor();
        }

        // Method to handle learning from textual interactions
        public void LearnFromInteraction(string interactionText)
        {
            try
            {
                // Ensure interaction text is not null or empty
                if (string.IsNullOrEmpty(interactionText))
                {
                    Log.Warning("LearningModule: Received empty interaction text.");
                    return;
                }

                // Process the text through NLP
                var processedText = nlpProcessor.ProcessInput(interactionText);

                // Check if the processed text is valid
                if (string.IsNullOrEmpty(processedText))
                {
                    Log.Warning("LearningModule: No valid output from NLP processor.");
                    return;
                }

                // Generate a response based on the processed text
                var response = nlpProcessor.GenerateResponse(processedText);

                // Log the response for demonstration and debugging
                Log.Message("LearningModule response: " + response);
            }
            catch (Exception ex)
            {
                Log.Error($"LearningModule: Error during learning from interaction - {ex.Message}");
            }
        }

        // Method to simulate the learning process over time
        public void UpdateLearning()
        {
            try
            {
                // Simulated periodic learning update, which could involve adjusting internal models or parameters
                Log.Message("LearningModule: Periodic learning update performed.");
            }
            catch (Exception ex)
            {
                Log.Error($"LearningModule: Error during update learning - {ex.Message}");
            }
        }
    }
}
