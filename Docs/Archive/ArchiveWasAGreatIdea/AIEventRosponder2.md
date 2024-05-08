using System;
using Verse;
using RimWorld;

namespace OldestHouse.OmniCore
{
    public static class AIEventResponders
    {
        // Initialize and subscribe to events dynamically
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

        // Handle events dynamically based on the type of incidents
        private static void DynamicEventHandling()
        {
            IncidentQueue queue = Find.Storyteller.incidentQueue;
            foreach (QueuedIncident qi in queue)
            {
                if (qi.FiringIncident.def == IncidentDefOf.RaidEnemy)
                {
                    RespondToRaid(qi.FiringIncident.parms);
                }
                else if (qi.FiringIncident.def == IncidentDefOf.Fire || qi.FiringIncident.def.category.defName.Equals("Environmental"))
                {
                    RespondToEnvironmentalIncidents(qi.FiringIncident.parms);
                }
            }
        }

        // Specific response logic for raids
        private static void RespondToRaid(IncidentParms raidParams)
        {
            Log.Message("AI responding strategically to a raid.");
            // Implementation details based on AI strategy and colony defense status
        }

        // Response logic for environmental incidents such as fire
        private static void RespondToEnvironmentalIncidents(IncidentParms envParams)
        {
            Log.Message("AI responding to environmental challenges.");
            // Responses could involve organizing fire fighting or moving colonists to safety
        }

        // Unsubscribe from events to clean up on mod deactivation or end of game session
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