using System.Diagnostics;
using Verse;

namespace AI_CORE_RIMWORLD_MOD.PerformanceMonitoring
{
    public static class MonitoringSystem
    {
        private static Stopwatch stopwatch = new Stopwatch();

        public static void StartMonitoring()
        {
            stopwatch.Start();
            Log.Message("Performance monitoring started.");
        }

        public static void StopAndReport()
        {
            stopwatch.Stop();
            Log.Message($"Performance monitoring stopped. Total execution time: {stopwatch.ElapsedMilliseconds} ms.");
        }
    }
}
