using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace Source.AITemple.ColonyManagementSystem
{
    // Coordinates all defense-related activities and strategies for the colony
    public class DefenseCoordinator
    {
        // Manage and update the colony's defense structures
        public void UpdateDefenseStructures(Map map)
        {
            List<Building> allDefenses = map.listerBuildings.allBuildingsColonist.OfType<Building>().Where(b => b.def.defName.Contains("Turret") || b.def.defName.Contains("Trap")).ToList();
            foreach (var defense in allDefenses)
            {
                if (!defense.IsInWorkingOrder())
                {
                    RepairDefense(defense);
                }
            }

            Log.Message("Defense structures checked and updated.");
        }

        // Handle the training and readiness of military-capable colonists
        public void ConductTrainingSessions(List<Pawn> soldiers)
        {
            foreach (Pawn soldier in soldiers)
            {
                TrainSoldier(soldier);
            }

            Log.Message("Training sessions conducted for all military-capable colonists.");
        }

        // Assign defensive positions during alerts or attacks
        public void AssignDefensivePositions(List<Pawn> soldiers, Map map)
        {
            foreach (Pawn soldier in soldiers)
            {
                AssignToDefensivePost(soldier, map);
            }

            Log.Message("All military-capable colonists assigned to defensive positions.");
        }

        // Repair a damaged defense structure
        private void RepairDefense(Building defense)
        {
            defense.HitPoints = defense.MaxHitPoints; // Simulating repair for example purposes
            Log.Message($"Repaired {defense.def.defName} at {defense.Position}");
        }

        // Conduct training for a soldier
        private void TrainSoldier(Pawn soldier)
        {
            // Simulate training effect
            soldier.skills.Learn(SkillDefOf.Shooting, 250f); // Arbitrary value to simulate training progress
            Log.Message($"{soldier.Name} has been trained in shooting.");
        }

        // Assign a soldier to a defensive position
        private void AssignToDefensivePost(Pawn soldier, Map map)
        {
            IntVec3 defensivePosition = FindStrategicPosition(map);
            soldier.drafter.Drafted = true;
            soldier.pather.StartPath(defensivePosition, PathEndMode.OnCell);
            Log.Message($"{soldier.Name} assigned to defensive position at {defensivePosition}");
        }

        // Find a strategic defensive position on the map
        private IntVec3 FindStrategicPosition(Map map)
        {
            // This would typically involve more complex logic based on map analysis
            return map.AllCells.Where(c => c.Standable(map) && map.reachability.CanReachColony(c)).OrderBy(c => c.DistanceTo(map.mapPawns.FreeColonistsSpawned.First().Position)).FirstOrDefault();
        }
    }
}
