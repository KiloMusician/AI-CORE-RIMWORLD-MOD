using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace RimWorldAdvancedAIMod.AI
{
    /// <summary>
    /// Represents a class that manages swarm intelligence algorithms to simulate complex collective behavior among NPCs or other game entities.
    /// </summary>
    public class SwarmIntelligence
    {
        private List<SwarmAgent> agents;
        private Map map;

        /// <summary>
        /// Constructor for the SwarmIntelligence class.
        /// </summary>
        /// <param name="map">The map where the swarm operates.</param>
        public SwarmIntelligence(Map map)
        {
            this.map = map;
            agents = new List<SwarmAgent>();
            InitializeSwarm();
        }

        /// <summary>
        /// Initializes the swarm with a set number of agents.
        /// </summary>
        private void InitializeSwarm()
        {
            int numberOfAgents = 10; // Example: Initialize with 10 agents
            for (int i = 0; i < numberOfAgents; i++)
            {
                IntVec3 startPosition = map.AllCells.RandomElement();
                agents.Add(new SwarmAgent(startPosition, map));
            }
        }

        /// <summary>
        /// Runs the swarm intelligence logic to allow agents to interact and make decisions collectively.
        /// </summary>
        public void RunSwarm()
        {
            foreach (var agent in agents)
            {
                agent.UpdateAgent();
                // Here you might implement behaviors such as moving towards a goal, avoiding obstacles, or following trails.
            }
        }

        /// <summary>
        /// Represents a single agent in the swarm.
        /// </summary>
        private class SwarmAgent
        {
            public IntVec3 Position { get; private set; }
            private Map map;

            public SwarmAgent(IntVec3 startPosition, Map map)
            {
                Position = startPosition;
                this.map = map;
            }

            /// <summary>
            /// Updates the state and position of the agent based on swarm rules.
            /// </summary>
            public void UpdateAgent()
            {
                // Example simple behavior: move randomly
                IntVec3 newPos = Position + GenAdj.AdjacentCellsAndInside.RandomElement();
                if (newPos.InBounds(map) && newPos.Standable(map))
                {
                    Position = newPos;
                }
                // Log movement for debugging
                Log.Message($"SwarmAgent moved to {Position}");
            }
        }
    }
}
