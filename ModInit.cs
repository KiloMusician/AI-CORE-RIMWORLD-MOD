using Verse;
using RimWorld;

namespace RimWorldAdvancedAIMod
{
    [StaticConstructorOnStartup]
    public static class ModInit
    {
        static ModInit()
        {
            // Initialize the AI Manager
            AIManager.Initialize();
            Log.Message("RimWorld Advanced AI Mod Loaded and AI Manager initialized.");
        }
    }
}
