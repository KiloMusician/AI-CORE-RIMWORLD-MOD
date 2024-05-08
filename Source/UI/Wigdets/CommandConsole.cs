using UnityEngine;
using Verse;
using RimWorld; // Add missing import for RimWorld namespace

namespace Source.UI.Widgets
{
    public class CommandConsole : Window
    {
        private string commandInput = "";

        // Constructor
        public CommandConsole()
        {
            this.doCloseX = true;
            this.forcePause = true;
            this.absorbInputAroundWindow = true;
            this.closeOnClickedOutside = true;
        }

        public override Vector2 InitialSize => new Vector2(600f, 350f);

        public override void DoWindowContents(Rect inRect)
        {
            // Command input field
            Rect textFieldRect = new Rect(inRect.x, inRect.y, inRect.width, 30f);
            commandInput = Widgets.TextField(textFieldRect, commandInput);

            // Submit button
            Rect buttonRect = new Rect(inRect.x, textFieldRect.yMax + 10f, inRect.width, 30f);
            if (Widgets.ButtonText(buttonRect, "Submit Command"))
            {
                ProcessCommand(commandInput);
            }
        }

        // Method to process commands input by the user
        private void ProcessCommand(string command)
        {
            Log.Message("Processing command: " + command);
            // Here, integrate the command processing logic or interaction with the AI
        }
    }
}
