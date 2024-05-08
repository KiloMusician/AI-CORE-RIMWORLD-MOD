using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace Source.AITemple.ColonyManagementSystem
{
    // Manages the assignment of tasks based on colonist skills and schedules
    public static class WorkforceManager
    {
        private static List<Pawn> colonists;

        // Initialize the Workforce Manager with the current list of colonists
        public static void InitializeWorkforce(List<Pawn> allColonists)
        {
            colonists = allColonists;
            Log.Message("Workforce Manager initialized with colonist data.");
        }

        // Assign tasks to colonists based on their skills and current workload
        public static void AssignTasks()
        {
            foreach (var colonist in colonists)
            {
                AssignBestTask(colonist);
            }
        }

        // Assign the most suitable task to a colonist based on their skills
        private static void AssignBestTask(Pawn colonist)
        {
            var bestTask = FindBestTaskForColonist(colonist);
            if (bestTask != null)
            {
                // Simulating task assignment (implementation would depend on RimWorld's mechanics)
                Log.Message($"Assigned task {bestTask.defName} to colonist {colonist.Name}");
                colonist.workSettings.EnableAndInitializeIfNotAlreadyInitialized();
                colonist.workSettings.SetPriority(bestTask, 1); // Highest priority
            }
        }

        // Find the best task for the colonist based on their skill levels
        private static WorkTypeDef FindBestTaskForColonist(Pawn colonist)
        {
            return DefDatabase<WorkTypeDef>.AllDefs
                .Where(workType => colonist.workSettings.WorkIsActive(workType) && IsColonistSkilledForTask(colonist, workType))
                .OrderByDescending(workType => colonist.skills.GetSkill(workType.relevantSkills[0]).Level)
                .FirstOrDefault();
        }

        // Check if the colonist is skilled for the task
        private static bool IsColonistSkilledForTask(Pawn colonist, WorkTypeDef workType)
        {
            return workType.relevantSkills.All(skill => colonist.skills.GetSkill(skill).Level >= 10); // Arbitrary skill level threshold
        }

        // Update colonist task assignments dynamically based on colony needs or events
        public static void UpdateTaskAssignments()
        {
            Log.Message("Updating task assignments based on current colony needs.");
            AssignTasks();
        }
    }
}
