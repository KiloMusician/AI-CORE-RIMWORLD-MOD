using System;
using System.Collections.Generic;
using Verse;

namespace Source.AITemple.ColonyManagementSystem
{
    public class ColonistAI
    {
        public Pawn Colonist { get; set; }
        public ColonistState State { get; set; }

        public ColonistAI(Pawn colonist)
        {
            if (colonist == null)
            {
                throw new ArgumentNullException(nameof(colonist));
            }

            Colonist = colonist;
            State = new ColonistState();
        }

        public void Update()
        {
            CheckNeeds();
            UpdateMood();
            DecideActions();
            ComprehendMeaning();
        }

        private void CheckNeeds()
        {
            // Example: Check if the colonist needs to eat or rest
            if (Colonist.needs.food.CurLevelPercentage < 0.3)
            {
                State.IsHungry = true;
            }
            if (Colonist.needs.rest.CurLevelPercentage < 0.3)
            {
                State.IsTired = true;
            }
        }

        private void UpdateMood()
        {
            // Adjust mood based on needs, environment, and interactions
            if (State.IsHungry || State.IsTired)
            {
                State.Mood -= 10;
            }
            else
            {
                // Check for positive mood modifiers
                State.Mood += Environment.PositiveInteractions(Colonist);
            }
        }

        private void DecideActions()
        {
            // Decide next action based on the state and mood
            if (State.IsHungry)
            {
                ActionManager.TakeAction(Colonist, ActionType.Eat);
            }
            else if (State.IsTired)
            {
                ActionManager.TakeAction(Colonist, ActionType.Sleep);
            }
        }

        private void ComprehendMeaning()
        {
            // Interpret what actions and mood changes mean in a broader narrative context
            if (State.HasChangedSignificantly())
            {
                Log.Info($"{Colonist.Label} felt {State.MoodDescription()} due to {State.RecentChanges()}.");
            }
        }
    }

    public class ColonistState
    {
        public int Mood { get; set; } = 50;
        public bool IsHungry { get; set; }
        public bool IsTired { get; set; }

        public bool HasChangedSignificantly()
        {
            // Determine if recent changes are significant
            return IsHungry || IsTired; // Simplified example
        }

        public string MoodDescription()
        {
            // Return a description of the current mood
            return IsHungry ? "hungry" : "tired";
        }

        public string RecentChanges()
        {
            // Return a string describing recent changes in state
            return IsHungry ? "lack of food" : "lack of sleep";
        }
    }
}
