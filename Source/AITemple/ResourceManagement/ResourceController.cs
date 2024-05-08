using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace Source.AITemple.ResourceManagement
{
    // Manages the stockpiling, distribution, and efficient use of resources in the colony
    public static class ResourceController
    {
        private static Dictionary<ThingDef, ResourceStockpile> resourceStockpiles = new Dictionary<ThingDef, ResourceStockpile>();

        // Initialize stockpiles for key resources
        public static void InitializeResourceStockpiles()
        {
            // Define key resources and initial quantities
            RegisterResource(ThingDefOf.WoodLog, 500);
            RegisterResource(ThingDefOf.Steel, 300);
            RegisterResource(ThingDefOf.MedicineHerbal, 50);
            Log.Message("Resource stockpiles initialized.");
        }

        // Register a new resource and its initial stockpile
        private static void RegisterResource(ThingDef resource, int initialQuantity)
        {
            if (!resourceStockpiles.ContainsKey(resource))
            {
                resourceStockpiles[resource] = new ResourceStockpile(resource, initialQuantity);
                Log.Message($"Resource registered: {resource.defName} with initial quantity {initialQuantity}");
            }
        }

        // Adjust stockpile levels
        public static void AdjustResourceQuantity(ThingDef resource, int adjustment)
        {
            if (resourceStockpiles.TryGetValue(resource, out ResourceStockpile stockpile))
            {
                stockpile.AdjustQuantity(adjustment);
                Log.Message($"Resource adjusted: {resource.defName} by {adjustment}. New quantity: {stockpile.Quantity}");
            }
        }

        // Check resource levels and notify if below threshold
        public static void CheckResourceLevels()
        {
            foreach (var stockpile in resourceStockpiles)
            {
                if (stockpile.Value.Quantity < stockpile.Value.Threshold)
                {
                    Log.Warning($"Resource low: {stockpile.Key.defName}. Current quantity: {stockpile.Value.Quantity}");
                }
            }
        }
    }

    // Represents a stockpile of a specific resource
    public class ResourceStockpile
    {
        public ThingDef Resource { get; private set; }
        public int Quantity { get; private set; }
        public int Threshold { get; private set; }

        public ResourceStockpile(ThingDef resource, int initialQuantity)
        {
            Resource = resource;
            Quantity = initialQuantity;
            Threshold = (int)(initialQuantity * 0.25);  // Default to 25% of initial quantity as low threshold
        }

        // Adjust the quantity of the stockpile
        public void AdjustQuantity(int adjustment)
        {
            Quantity += adjustment;
            Quantity = Quantity < 0 ? 0 : Quantity;  // Ensure quantity does not go negative
        }
    }
}
