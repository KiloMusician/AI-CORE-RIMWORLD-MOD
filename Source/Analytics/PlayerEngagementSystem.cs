using System;
using System.Collections.Generic;
using System.Linq;

namespace Source.Analytics
{
    public class PlayerEngagementSystem
    {
        private List<SessionData> sessionRecords = new List<SessionData>();

        public void LogSessionStart(DateTime startTime)
        {
            Console.WriteLine("Session started...");
            sessionRecords.Add(new SessionData { Start = startTime });
        }

        public void LogSessionEnd(DateTime endTime)
        {
            Console.WriteLine("Session ended...");
            var session = sessionRecords.LastOrDefault();
            if (session != null)
            {
                session.End = endTime;
                session.Duration = endTime.Subtract(session.Start);
            }
        }

        public void AnalyzeEngagement()
        {
            Console.WriteLine("Analyzing player engagement...");
            // Example: Calculate average session length
            var averageSessionTime = sessionRecords.Select(s => s.Duration).Average(d => d.TotalMinutes);
            Console.WriteLine($"Average Session Duration: {averageSessionTime} minutes");
        }

        public void ReportEngagementMetrics()
        {
            Console.WriteLine("Reporting engagement metrics...");
            // Implement logic to format and report the engagement findings
        }
    }

    public class SessionData
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
