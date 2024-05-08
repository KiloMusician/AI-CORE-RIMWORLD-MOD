using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace OldestHouse.AITemple
{
    public class IntelligentArchitectPlanner
    {
        private Map map;
        private List<BuildPlan> plans;

        public IntelligentArchitectPlanner(Map map)
        {
            this.map = map;
            plans = new List<BuildPlan>();
            GenerateInitialPlans();
        }

        public static void PlanAndBuildStructure(Pawn planner)
        {
            if (!planner.IsColonistPlayerControlled || planner.WorkTypeIsDisabled(WorkTypeDefOf.Construction))
            {
                return;
            }

            var suitableLocation = FindSuitableBuildingLocation(planner);
            if (suitableLocation != IntVec3.Invalid)
            {
                BuildStructureAt(suitableLocation, planner);
            }
        }

        private static IntVec3 FindSuitableBuildingLocation(Pawn planner)
        {
            var map = planner.Map;
            var storageBuildings = map.listerBuildings.AllBuildingsColonistOfDef(ThingDefOf.StorageUnit);
            if (storageBuildings.Count() > 5)
            {
                return IntVec3.Invalid;
            }

            CellRect cellRect = CellRect.CenteredOn(planner.Position, 10);
            foreach (IntVec3 cell in cellRect)
            {
                if (cell.Standable(map) && cell.GetFirstBuilding(map) == null)
                {
                    return cell;
                }
            }
            return IntVec3.Invalid;
        }

        private static void BuildStructureAt(IntVec3 location, Pawn planner)
        {
            ThingDef buildingDef = ThingDefOf.StorageUnit;
            var blueprint = GenConstruct.PlaceBlueprintForBuild(buildingDef, location, planner.Map, Rot4.North, planner.Faction, ThingDefOf.WoodLog);
            if (blueprint != null)
            {
                planner.jobs.StartJob(new Job(JobDefOf.PlaceBlueprint, blueprint), JobCondition.Ongoing);
            }
        }

        private void GenerateInitialPlans()
        {
            plans.Add(new BuildPlan("Solar Generator", new IntVec3(10, 0, 10)));
            plans.Add(new BuildPlan("Wind Turbine", new IntVec3(20, 0, 20)));
            Log.Message("Initial architectural plans generated.");
        }

        public void UpdatePlanning()
        {
            AdjustPlansForWeatherConditions();
            Log.Message("Architectural plans updated based on weather conditions.");
        }

        private void AdjustPlansForWeatherConditions()
        {
            if (map.weatherManager.curWeather.rainRate > 0.5f)
            {
                plans.ForEach(plan => plan.Location += new IntVec3(5, 0, 5));
                Log.Message("Plans adjusted for rainy weather.");
            }
        }

        public void ExecutePlans()
        {
            foreach (var plan in plans)
            {
                BuildStructure(plan);
            }
            Log.Message("All architectural plans have been executed.");
        }

        private void BuildStructure(BuildPlan plan)
        {
            Log.Message($"Building {plan.StructureType} at {plan.Location}.");
        }

        private class BuildPlan
        {
            public string StructureType { get; }
            public IntVec3 Location { get; set; }

            public BuildPlan(string structureType, IntVec3 location)
            {
                StructureType = structureType;
                Location = location;
            }
        }
    }
}
