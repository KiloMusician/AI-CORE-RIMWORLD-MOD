using NUnit.Framework;
using Verse;
using RimWorld;

namespace RimWorldAIEnhanced.Tests.IntegrationTests
{
    [TestFixture]
    public class GameplayTests
    {
        [Test]
        public void TestAICoreIntegrationWithGameplay()
        {
            // Setup test environment
            AICore aiCore = AICore.Instance;
            Assert.IsNotNull(aiCore, "AICore instance should not be null");

            // Simulate a gameplay scenario where the AI core is utilized
            aiCore.ProcessDecisions();

            // Verify outcomes
            Assert.IsTrue(aiCore.DecisionsMade, "Decisions should have been made by the AI Core");
        }

        [Test]
        public void TestColonistInteractionWithAI()
        {
            // Create a pawn and simulate interaction
            Pawn pawn = PawnGenerator.GeneratePawn(PawnKindDefOf.Colonist, Faction.OfPlayer);
            AICore aiCore = AICore.Instance; // Add this line to declare the aiCore variable
            string interactionResult = aiCore.HandleInteraction(pawn);

            // Assert expected interaction result
            Assert.AreEqual("Interaction Successful", interactionResult, "Pawn interaction with AI did not return expected result");
        }
    }
}
