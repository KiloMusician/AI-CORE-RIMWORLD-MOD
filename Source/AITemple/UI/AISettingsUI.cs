using UnityEngine;
using Verse;

namespace Source.AITemple.UI
{
    // Defines the settings UI for AI configurations
    class ModSettings_AIOmniCore : ModSettings
    {
        public float aiWorkSpeedModifier = 1.0f;
        public bool advancedAIEnabled = false;
        public float decisionAccuracyThreshold = 0.5f;

        // Method to write settings to a file
        public override void ExposeData()
        {
            Scribe_Values.Look(ref aiWorkSpeedModifier, "aiWorkSpeedModifier", 1.0f);
            Scribe_Values.Look(ref advancedAIEnabled, "advancedAIEnabled", false);
            Scribe_Values.Look(ref decisionAccuracyThreshold, "decisionAccuracyThreshold", 0.5f);
            base.ExposeData();
        }
    }

    // Mod class to add a settings part to the options menu
    class Mod_AIOmniCore : Mod
    {
        ModSettings_AIOmniCore settings;

        public Mod_AIOmniCore(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<ModSettings_AIOmniCore>();
        }

        public override string SettingsCategory() => "AI OmniCore Settings";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            // Slider for AI Work Speed Modifier
            listingStandard.Label($"AI Work Speed Modifier: {settings.aiWorkSpeedModifier:P0}");
            settings.aiWorkSpeedModifier = listingStandard.Slider(settings.aiWorkSpeedModifier, 0.5f, 2.0f);

            // Checkbox for Advanced AI
            listingStandard.CheckboxLabeled("Enable Advanced AI Systems", ref settings.advancedAIEnabled, "Toggle whether advanced AI systems are active.");

            // Slider for Decision Accuracy Threshold
            listingStandard.Label($"Decision Accuracy Threshold: {settings.decisionAccuracyThreshold:P0}");
            settings.decisionAccuracyThreshold = listingStandard.Slider(settings.decisionAccuracyThreshold, 0.1f, 1.0f);

            listingStandard.End();
            settings.Write();
        }
    }
}