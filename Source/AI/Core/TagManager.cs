using System;
using System.Collections.Generic;
using Verse;  // RimWorld's base namespace for managing game components

namespace RimWorldAdvancedAIMod.AI
{
    // Central manager for dynamic AI interactions and tag management
    public class TagManager
    {
        private Dictionary<string, SuperMegaHyperTag> tags;

        public TagManager()
        {
            tags = new Dictionary<string, SuperMegaHyperTag>();
            InitializeDefaultTags();
        }

        private void InitializeDefaultTags()
        {
            // Initial complex tag setup as an example
            UpdateTag("Day9", new Dictionary<string, string> {
                {"A", "Day9"},
                {"B", "EXP↑"},
                {"C", "Leadership"},
                // Additional default attributes can be added here
            });
        }

        public void UpdateTag(string tagName, Dictionary<string, string> attributes)
        {
            if (tags.ContainsKey(tagName))
            {
                // Merging new attributes with existing ones
                foreach (var attr in attributes)
                {
                    tags[tagName].Attributes[attr.Key] = attr.Value;
                }
            }
            else
            {
                // Adding a new tag if it does not exist
                tags[tagName] = new SuperMegaHyperTag { TagName = tagName, Attributes = attributes };
            }
        }

        public bool RemoveTag(string tagName)
        {
            return tags.Remove(tagName);
        }

        public string GetFormattedTagInfo(string tagName)
        {
            if (tags.TryGetValue(tagName, out SuperMegaHyperTag tag))
            {
                return $"Tag: {tag.TagName}, Attributes: {string.Join(", ", tag.Attributes)}";
            }
            return "Tag not found.";
        }
    }

    // Defines a complex tagging system with multiple attributes
    public class SuperMegaHyperTag
    {
        public string TagName { get; set; }
        public Dictionary<string, string> Attributes { get; set; }

        public SuperMegaHyperTag()
        {
            Attributes = new Dictionary<string, string>();
        }
    }
}
