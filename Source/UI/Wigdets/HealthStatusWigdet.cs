using UnityEngine;
using Verse;
using RimWorld; // Added missing using statement for RimWorld namespace

namespace Source.UI.Widgets
{
    public class HealthStatusWidget
    {
        // Method to draw the health status widget on the screen
        public static void DrawHealthStatus(Rect rect, Pawn pawn)
        {
            // Drawing the background rectangle
            Widgets.DrawBoxSolid(rect, new Color(0.1f, 0.1f, 0.1f, 0.5f));

            // Drawing health status bar
            Rect barRect = new Rect(rect.x + 10, rect.y + 10, rect.width - 20, 25f);
            float healthPct = pawn.health.summaryHealth.SummaryHealthPercent;
            Widgets.FillableBar(barRect, healthPct, SolidColorMaterials.SimpleSolidColorMaterial(Color.green));

            // Label for health percentage
            Text.Anchor = TextAnchor.MiddleCenter;
            Widgets.Label(barRect, "Health: " + (healthPct * 100f).ToString("F0") + "%");
            Text.Anchor = TextAnchor.UpperLeft;
        }
    }
}
