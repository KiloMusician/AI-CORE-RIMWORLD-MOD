using NUnit.Framework;
using Verse;
using RimWorldAIEnhanced.Source.Health;
using RimWorld;

namespace RimWorldAIEnhanced.Tests.UnitTests
{
    [TestFixture]
    public class HealthTests
    {
        [Test]
        public void TestHealthOptimization()
        {
            Pawn pawn = PawnGenerator.GeneratePawn(PawnKindDefOf.Colonist, Faction.OfPlayer);
            HealthManager healthManager = new HealthManager();

            // Simulate health optimization
            healthManager.CheckHealthAndManage(pawn);

            // Assert health has been optimized
            Assert.IsTrue(pawn.health.summaryHealth.SummaryHealthPercent > 0.9, "Pawn health should be optimized to above 90%");
        }

        [Test]
        public void TestTreatmentApplication()
        {
            Pawn pawn = PawnGenerator.GeneratePawn(PawnKindDefOf.Colonist, Faction.OfPlayer);
            TreatmentSimulator.TreatCondition(pawn, "GeneralTreatment");

            // Assert that treatment was applied successfully
            Assert.IsTrue(pawn.health.HasHediff(HediffDefOf.Healed), "Pawn should have healed hediff after treatment");
        }
    }
}
