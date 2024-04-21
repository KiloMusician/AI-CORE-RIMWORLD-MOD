using UnityEngine;
using Verse;

namespace RimWorldAIEnhanced.Source.UI.Dialogs
{
    public class Dialog_AIInteraction : Window
    {
        private string interactionText = "";
        private string responseText = "Waiting for input...";

        // Constructor
        public Dialog_AIInteraction()
        {
            this.doCloseX = true;
            this.forcePause = true;
            this.absorbInputAroundWindow = true;
            this.closeOnClickedOutside = true;
        }

        public override Vector2 InitialSize => new Vector2(600f, 350f);

        public override void DoWindowContents(Rect inRect)
        {
            // Text field for interaction input
            Rect textFieldRect = new Rect(inRect.x, inRect.y, inRect.width, 30f);
            interactionText = Widgets.TextField(textFieldRect, interactionText);

            // Response display
            Rect responseRect = new Rect(inRect.x, textFieldRect.yMax + 10f, inRect.width, 100f);
            Widgets.Label(responseRect, responseText);

            // Submit button
            Rect buttonRect = new Rect(inRect.x, responseRect.yMax + 10f, inRect.width, 30f);
            if (Widgets.ButtonText(buttonRect, "Interact"))
            {
                // Replace the placeholder code with actual AI response
                responseText = GetAIResponse(interactionText);
            }
        }

        // Method to get AI response
        private string GetAIResponse(string input)
        {
            // Replace this with actual AI logic to generate response
            return "Response: " + input;
        }
    }
}
