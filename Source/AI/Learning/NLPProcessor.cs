using System;
using System.Linq;
using System.Collections.Generic;

namespace RimWorldAIEnhanced.Source.AI.Learning
{
    public class NLPProcessor
    {
        private Random random = new Random(); // Ensures consistent random generation

        // Simulated method to parse and interpret natural language input from players or NPCs
        public Dictionary<string, float> ProcessInput(string input)
        {
            // Check for null or empty input to prevent errors
            if (string.IsNullOrWhiteSpace(input))
            {
                return new Dictionary<string, float>();
            }

            // Split input into words, simulate understanding by assigning relevance scores
            var words = input.Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            var scores = new Dictionary<string, float>();
            foreach (var word in words)
            {
                // Assign a more consistent random score to each unique word
                if (!scores.ContainsKey(word))
                {
                    scores[word] = (float)Math.Round(random.NextDouble(), 2);
                }
            }
            return scores;
        }

        // Simulated method to generate a response based on processed input
        public string GenerateResponse(Dictionary<string, float> processedInput)
        {
            if (processedInput == null || !processedInput.Any())
            {
                return "I didn't catch that, could you repeat?";
            }

            // Select the highest scored word to generate a more meaningful response
            var highestScoredWord = processedInput.OrderByDescending(kvp => kvp.Value).FirstOrDefault();
            return $"It seems you are interested in: {highestScoredWord.Key} ({highestScoredWord.Value * 100}% relevance).";
        }
    }
}
