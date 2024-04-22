using UnityEngine;
using Verse;

namespace RimWorldAIEnhanced.Source.UI.Dialogs
{
    public class Dialog_AIInteraction : Window
    {
        private string interactionText = "";
        private string responseText = "Waiting for input...";
        private Vector2 responseScrollPosition = Vector2.zero;
        private bool isProcessing = false;

        public override Vector2 InitialSize => new Vector2(600f, 400f);

        public Dialog_AIInteraction()
        {
            this.doCloseX = true;
            this.forcePause = true;
            this.absorbInputAroundWindow = true;
            this.closeOnClickedOutside = true;
        }

        public override void DoWindowContents(Rect inRect)
        {
            Rect textFieldRect = new Rect(inRect.x + 10, inRect.y + 10, inRect.width - 20, 30f);
            interactionText = Widgets.TextField(textFieldRect, interactionText);

            Rect buttonRect = new Rect(inRect.x + 10, textFieldRect.yMax + 10, inRect.width - 20, 30f);
            if (Widgets.ButtonText(buttonRect, "Interact") && !isProcessing)
            {
                responseText = "Processing...";
                isProcessing = true;
                LongEventHandler.QueueLongEvent(() => 
                {
                    responseText = GetAIResponse(interactionText);
                    isProcessing = false;
                }, "AIProcessing", false, null);
            }

            Rect responseLabelRect = new Rect(inRect.x + 10, buttonRect.yMax + 10, inRect.width - 20, inRect.height - buttonRect.yMax - 20);
            Widgets.LabelScrollable(responseLabelRect, responseText, ref responseScrollPosition);
        }

        private string GetAIResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please enter some text to interact with AI.";

            // Simulate processing delay
            System.Threading.Thread.Sleep(1000); // This would be replaced with actual asynchronous AI processing call

            return "Response: " + input; // Replace this with actual logic to generate response
        }
    }
}
