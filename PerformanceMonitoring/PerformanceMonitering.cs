// PerformanceMonitoring.cs - Monitoring the performance impact of AI enhancements in RimWorld
using System;
using System.Diagnostics;
using Verse;

namespace AI_CORE_RIMWORLD_MOD.PerformanceMonitoring
{
    public static class PerformanceMonitoring
    {
        private static Stopwatch stopwatch = new Stopwatch();
        private static long memoryUsage;
        private static double cpuUsage;

        // Start monitoring performance
        public static void StartMonitoring()
        {
            stopwatch.Start();
            Console.WriteLine("Performance monitoring started.");
        }

        // Perform a performance check
        public static void PerformCheck()
        {
            memoryUsage = GC.GetTotalMemory(false);
            cpuUsage = Process.GetCurrentProcess().TotalProcessorTime.TotalMilliseconds;

            Console.WriteLine($"Memory Usage: {memoryUsage} bytes");
            Console.WriteLine($"CPU Usage: {cpuUsage} ms");

            if (memoryUsage > 1000000000) // Example threshold: 1GB
            {
                Console.WriteLine("Warning: High memory usage detected.");
            }
        }

        // Stop monitoring and log results
        public static void StopMonitoring()
        {
            stopwatch.Stop();
            Console.WriteLine($"Monitoring stopped. Total elapsed time: {stopwatch.ElapsedMilliseconds} ms.");
            ReportPerformance();
            // Reset the stopwatch for next use
            stopwatch.Reset();
        }

        // Report collected performance data
        private static void ReportPerformance()
        {
            Console.WriteLine("Performance Report:");
            Console.WriteLine($"Total Memory Usage: {memoryUsage} bytes");
            Console.WriteLine($"Total CPU Time: {cpuUsage} ms");
        }

        // Call this method at critical points in your mod where performance may be impacted
        public static void LogPerformance(string activityName)
        {
            Console.WriteLine($"Activity '{activityName}' completed in {stopwatch.ElapsedMilliseconds} ms.");
            stopwatch.Restart(); // Restart the stopwatch to begin timing the next activity
        }
    }
}
