using System;
using System.Collections.Generic;
using System.Linq;

namespace Source.Core
{
    /// <summary>
    /// An enhanced class to process natural language input and generate responses, summaries,
    /// and visual word clouds based on relevance scoring of words.
    /// </summary>
    public class NLPProcessor
    {
        private Random random = new Random(); // Ensures consistent random generation

        /// <summary>
        /// Parses and interprets natural language input from players or NPCs.
        /// Assigns scores based on word frequency and contextual importance.
        /// </summary>
        /// <param name="input">The string input to process.</param>
        /// <returns>A dictionary where keys are words and values are relevance scores.</returns>
        public Dictionary<string, float> ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new Dictionary<string, float>();
            }

            var words = input.Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            var scores = new Dictionary<string, float>();
            foreach (var word in words)
            {
                if (!scores.ContainsKey(word))
                {
                    // Assign a score based on word length and random factor to simulate context understanding
                    scores[word] = (float)Math.Round(word.Length * random.NextDouble(), 2);
                }
                else
                {
                    // Increase score for repeated words to emphasize their importance
                    scores[word] += (float)Math.Round(0.1 * word.Length, 2);
                }
            }
            return scores;
        }

        /// <summary>
        /// Generates a response based on the highest scored words in the processed input.
        /// </summary>
        /// <param name="processedInput">The processed input with word scores.</param>
        /// <returns>A string representing a response highlighting key topics.</returns>
        public string GenerateResponse(Dictionary<string, float> processedInput)
        {
            if (processedInput == null || !processedInput.Any())
            {
                return "I didn't catch that, could you repeat?";
            }

            var highestScoredWords = processedInput.OrderByDescending(kvp => kvp.Value).Take(3);
            var response = $"You seem to be interested in: {string.Join(", ", highestScoredWords.Select(kvp => $"{kvp.Key} ({kvp.Value * 100}% relevance)"))}.";
            return response;
        }

        /// <summary>
        /// Generates a summary of the processed input, highlighting the top words.
        /// </summary>
        /// <param name="processedInput">The processed input with word scores.</param>
        /// <returns>A string that represents a concise summary of the input.</returns>
        public string GenerateSummary(Dictionary<string, float> processedInput)
        {
            if (processedInput == null || !processedInput.Any())
            {
                return "No input to summarize.";
            }

            var sortedWords = processedInput.OrderByDescending(kvp => kvp.Value).Take(5);
            var summary = $"Summary of interest: {string.Join(", ", sortedWords.Select(kvp => kvp.Key))}.";
            return summary;
        }

        /// <summary>
        /// Generates a word cloud based on the processed input, visually emphasizing frequent and relevant words.
        /// </summary>
        /// <param name="processedInput">The processed input with word scores.</param>
        /// <returns>A string that visually represents a word cloud of the processed input.</returns>
        public string GenerateWordCloud(Dictionary<string, float> processedInput)
        {
            if (processedInput == null || !processedInput.Any())
            {
                return "No input to generate word cloud.";
            }

            var wordCloud = new Dictionary<string, int>();
            foreach (var kvp in processedInput)
            {
                // Each word appears in the word cloud proportionally to its score
                wordCloud[kvp.Key] = (int)(kvp.Value * 10);
            }

            var cloudRepresentation = string.Join(" ", wordCloud.SelectMany(kvp => Enumerable.Repeat(kvp.Key, kvp.Value)));
            return $"Word Cloud: {cloudRepresentation}";
        }
    }
}
