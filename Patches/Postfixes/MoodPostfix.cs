using HarmonyLib;
using RimWorld;
using System.Reflection;

namespace RimWorldAIEnhanced.Patches.Postfixes
{
    [HarmonyPatch(typeof(Pawn_NeedsTracker))]
    [HarmonyPatch("ShouldHaveNeed")]
    public static class MoodPostfix
    {
        // Add missing import statement for NeedDefOf
        using static RimWorld.NeedDefOf;

        [HarmonyPostfix]
        public static void ShouldHaveNeedPostfix(ref bool __result, NeedDef nd, Pawn ___pawn)
        {
            // Adjust the need for mood based on AI enhancements
            if (nd == Mood && ___pawn.IsAI())
            {
                // Example modification: AI pawns have reduced mood swings
                __result = false;
            }
        }
    }
}
