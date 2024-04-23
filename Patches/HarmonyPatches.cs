using HarmonyLib;
using System.Reflection;
using Verse;  // This using directive ensures access to RimWorld and Verse specific classes and attributes like StaticConstructorOnStartup.

namespace RimWorldAIEnhanced.Patches
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            // Create a new Harmony instance with a unique identifier for your mod
            var harmony = new Harmony("com.yourmodid.rimworldaienhanced");

            // Apply all patches defined in this assembly
            harmony.PatchAll(Assembly.GetExecutingAssembly());  // Explicitly defining the assembly can be helpful for clarity and to avoid unintended behavior in complex projects.
        }
    }
}
