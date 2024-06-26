using NUnit.Framework;
using RimWorldAIEnhanced.Source.AI;
using RimWorldAIEnhanced.Source.AI.LearningModule; // Moved here

namespace RimWorldAIEnhanced.Tests.UnitTests
{
    [TestFixture]
    public class AITests
    {
        [Test]
        public void TestNLPProcessingAccuracy()
        {
            // Create instance of NLPProcessor
            NLPProcessor processor = new NLPProcessor();
            var output = processor.ProcessInput("Hello world");

            // Expect some defined outputs for known inputs
            Assert.Greater(output["world"], 0.5, "NLPProcessor should assign high relevance to 'world' in 'Hello world'");
        }

        [Test]
        public void TestLearningModuleAdaptation()
        {
            LearningModule learningModule = new LearningModule();
            learningModule.LearnFromInteraction("Learning Test");
            
            // Verify learning outcome
            Assert.IsTrue(learningModule.HasAdapted, "Learning Module should adapt from interaction");
        }
    }
}