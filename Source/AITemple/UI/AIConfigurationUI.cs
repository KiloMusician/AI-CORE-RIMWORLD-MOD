AI Configuration and User Interface
AITemple\AIConfigurationUI.cs
using RimWorld;
using UnityEngine;
using Verse;

namespace Source.AITemple.UI
{
    public class AIConfigurationUI : Window
    {
        public override Vector2 InitialSize => new Vector2(600f, 400f);

        public AIConfigurationUI()
        {
            this.doCloseX = true;
            this.absorbInputAroundWindow = true;
            this.forcePause = true;
        }

        public override void DoWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.Label("AI Configuration Panel");

            if (listingStandard.ButtonText("Adjust AI Settings"))
            {
                Find.WindowStack.Add(new Dialog_AIAdjustments());
                Log.Message("AI Settings adjustment window opened.");
            }

            listingStandard.End();
        }

        private class Dialog_AIAdjustments : Window
        {
            public Dialog_AIAdjustments()
            {
                this.doCloseX = true;
                this.absorbInputAroundWindow = true;
                this.forcePause = true;
            }

            public override void DoWindowContents(Rect inRect)
            {
                Listing_Standard listing = new Listing_Standard();
                listing.Begin(inRect);

                listing.Label("AI Learning Rate Adjustment");
                AITemplate.Settings.learningRate = listing.Slider(AITemplate.Settings.learningRate, 0.1f, 2.0f);

                listing.Label("Decision Threshold:");
                AITemplate.Settings.decisionThreshold = listing.Slider(AITemplate.Settings.decisionThreshold, 0.01f, 1.0f);

                listing.End();
            }
        }
    }
}
