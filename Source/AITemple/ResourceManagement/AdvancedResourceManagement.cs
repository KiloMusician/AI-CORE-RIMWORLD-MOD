using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Source.AITemple.ResourceManagement
{
    public class AdvancedResourceManagement
    {
        private Dictionary<string, int> _resourceStock;
        private Dictionary<string, ResourceAllocation> _allocations;

        private Dictionary<string, int> resourcesAvailable;

        public AdvancedResourceManagement()
        {
            _resourceStock = new Dictionary<string, int>();
            _allocations = new Dictionary<string, ResourceAllocation>();
            resourcesAvailable = new Dictionary<string, int>();
        }

        public void UpdateResourceStock(string resource, int quantity)
        {
            if (_resourceStock.ContainsKey(resource))
            {
                _resourceStock[resource] += quantity;
            }
            else
            {
                _resourceStock[resource] = quantity;
            }
        }

        public void AllocateResource(string resource, int quantity, string department)
        {
            if (_resourceStock.ContainsKey(resource) && _resourceStock[resource] >= quantity)
            {
                _resourceStock[resource] -= quantity;
                LogAllocation(resource, quantity, department);
            }
            else
            {
                HandleShortage(resource, quantity);
            }
        }

        private void LogAllocation(string resource, int quantity, string department)
        {
            if (!_allocations.ContainsKey(department))
            {
                _allocations[department] = new ResourceAllocation();
            }
            _allocations[department].AddAllocation(resource, quantity);
        }

        private void HandleShortage(string resource, int needed)
        {
            Log.Warning($"Resource shortage: {resource} needed {needed} more.");
            // Implement strategy to handle shortages, e.g., request trade or adjust production priorities
        }

        public void GenerateResourceReport()
        {
            foreach (var dept in _allocations.Keys)
            {
                _allocations[dept].GenerateReport();
            }
        }

        public void UpdateResourceUsage(VirtualMap map)
        {
            // Check for resource levels and adjust tasks accordingly
            if (_resourceStock.ContainsKey("Wood") && _resourceStock["Wood"] < 100)
            {
                AssignTask(map, "ChopWood");
                LogMessage("Low wood supplies detected. Assigning wood chopping tasks.");
            }
            if (_resourceStock.ContainsKey("Steel") && _resourceStock["Steel"] < 50)
            {
                AssignTask(map, "MineSteel");
                LogMessage("Steel supplies are low. Assigning mining tasks.");
            }
        }

        private void AssignTask(VirtualMap map, string taskName)
        {
            // Simulate task assignment
            map.RecentEvents.Add(new GameEvent { Description = $"Task assigned: {taskName}" });
        }

        public void ProcessEndOfDayResources(VirtualMap map)
        {
            // Simulate resource consumption and production
            if (_resourceStock.ContainsKey("Wood"))
            {
                _resourceStock["Wood"] -= map.Population * 2;  // Example consumption rate
            }
            if (_resourceStock.ContainsKey("Steel"))
            {
                _resourceStock["Steel"] -= map.Population * 1; // Example consumption rate
            }
            LogMessage($"End of day resources update: Wood: {_resourceStock["Wood"]}, Steel: {_resourceStock["Steel"]}");
        }

        public void AllocateResources(string resource, int quantity, ColonyDepartment department)
        {
            if (resourcesAvailable.ContainsKey(resource) && resourcesAvailable[resource] >= quantity)
            {
                resourcesAvailable[resource] -= quantity;
                Log.Message($"Resource {resource} allocated to {department}: {quantity}");
            }
            else
            {
                HandleResourceShortage(resource, quantity);
            }
        }

        private void HandleResourceShortage(string resource, int needed)
        {
            Log.Warning($"Resource shortage: {resource} needed {needed} more.");
            // Implement strategy to handle shortages, e.g., request trade or adjust production priorities
        }

        public void ReportResourceStatus()
        {
            foreach (var res in resourcesAvailable)
            {
                Log.Message($"Resource {res.Key} available: {res.Value}");
            }
        }
    }

    public class ResourceAllocation
    {
        private Dictionary<string, int> _allocations;

        public ResourceAllocation()
        {
            _allocations = new Dictionary<string, int>();
        }

        public void AddAllocation(string resource, int quantity)
        {
            if (_allocations.ContainsKey(resource))
            {
                _allocations[resource] += quantity;
            }
            else
            {
                _allocations[resource] = quantity;
            }
        }

        public void GenerateReport()
        {
            // Generate a report detailing all allocations
        }
    }

    public enum ColonyDepartment { Construction, Defense, Agriculture }
}
