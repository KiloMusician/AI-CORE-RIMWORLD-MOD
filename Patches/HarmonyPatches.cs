using HarmonyLib;
using System.Reflection;
using Verse;

namespace RimWorldAIEnhanced.Patches
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("com.yourname.rimworldaienhanced");
            harmony.PatchAll(Assembly.Get
using HarmonyLib;
using Verse;

namespace RimWorldAIEnhanced.Patches
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            // Create a new Harmony instance
            Harmony harmony = new Harmony("com.yourmodid.rimworldaienhanced");

            // Apply all patches
            harmony.PatchAll();
        }
    }
}
