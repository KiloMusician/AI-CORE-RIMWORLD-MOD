using System;
using Verse;
using RimWorld;

namespace Source.AITemple.CoreAISystem
{
    public static class AIEventResponders
    {
        public static void InitializeAndSubscribe()
        {
            Log.Message("Initializing and subscribing to game events...");
            try
            {
                Find.Storyteller.incidentQueue.IncidentQueueTick += DynamicEventHandling;
                Log.Message("Subscribed to game events successfully.");
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to subscribe to game events: {ex.Message}");
            }
        }

        private static void DynamicEventHandling()
        {
            IncidentQueue queue = Find.Storyteller.incidentQueue;
            foreach (QueuedIncident qi in queue)
            {
                switch (qi.FiringIncident.def)
                {
                    case IncidentDef raid when raid == IncidentDefOf.RaidEnemy:
                        RespondToRaid(qi.FiringIncident.parms);
                        break;
                    case IncidentDef fire when fire == IncidentDefOf.Fire:
                    case IncidentDef environmental when environmental.category.defName.Equals("Environmental"):
                        RespondToEnvironmentalIncidents(qi.FiringIncident.parms);
                        break;
                }
            }
        }

        private static void RespondToRaid(IncidentParms raidParams)
        {
            Log.Message("AI responding strategically to a raid.");
            // Enhanced logic based on AI strategy and colony defense status
        }

        private static void RespondToEnvironmentalIncidents(IncidentParms envParams)
        {
            Log.Message("AI responding to environmental challenges.");
            // Enhanced logic for firefighting or moving colonists to safety
        }

        public static void CleanupSubscriptions()
        {
            try
            {
                Find.Storyteller.incidentQueue.IncidentQueueTick -= DynamicEventHandling;
                Log.Message("Unsubscribed from game events successfully.");
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to unsubscribe from game events: {ex.Message}");
            }
        }
    }
}
