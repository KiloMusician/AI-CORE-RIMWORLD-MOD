using System;
using System.Collections.Generic;
using Verse;

namespace OldestHouse.AITemple
{
    public class SocialInteractionSystem
    {
        private Dictionary<Colonist, SocialStats> colonistSocialData = new Dictionary<Colonist, SocialStats>();

        public SocialInteractionSystem(List<Colonist> colonists)
        {
            // Initialize social data for each colonist
            foreach (var colonist in colonists)
            {
                colonistSocialData[colonist] = new SocialStats();
            }
        }

        public void ProcessSocialInteractions(Colonist colonist, List<Colonist> colonists)
        {
            foreach (var other in colonists)
            {
                if (colonist != other && colonist.CanInteractWith(other))
                {
                    var interactionType = DetermineInteractionType(colonist, other);
                    colonist.Interact(other, interactionType);
                    UpdateSocialStats(colonist, other, interactionType);
                }
            }
        }

        private InteractionType DetermineInteractionType(Colonist colonist, Colonist other)
        {
            // Logic to determine the type of interaction based on colonist traits, mood, and relationship
            if (colonistSocialData[colonist].Relationships[other].Friendship > 80)
                return InteractionType.FriendlyChat;
            else if (colonistSocialData[colonist].Relationships[other].Friendship < 20)
                return InteractionType.Argument;
            else
                return InteractionType.NeutralTalk;
        }

        private void UpdateSocialStats(Colonist colonist, Colonist other, InteractionType interactionType)
        {
            // Adjust social stats based on the type of interaction
            switch (interactionType)
            {
                case InteractionType.FriendlyChat:
                    colonistSocialData[colonist].Mood += 5;
                    colonistSocialData[other].Mood += 5;
                    colonistSocialData[colonist].Relationships[other].Friendship += 1;
                    break;
                case InteractionType.Argument:
                    colonistSocialData[colonist].Mood -= 5;
                    colonistSocialData[other].Mood -= 5;
                    colonistSocialData[colonist].Relationships[other].Friendship -= 1;
                    break;
                case InteractionType.NeutralTalk:
                    // Neutral talk does not significantly affect mood or relationship
                    break;
            }
        }
    }

    public class SocialStats
    {
        public int Mood { get; set; }
        public Dictionary<Colonist, RelationshipStats> Relationships { get; set; }

        public SocialStats()
        {
            Relationships = new Dictionary<Colonist, RelationshipStats>();
        }
    }

    public class RelationshipStats
    {
        public int Friendship { get; set; }
    }

    public enum InteractionType
    {
        FriendlyChat,
        NeutralTalk,
        Argument
    }
}
