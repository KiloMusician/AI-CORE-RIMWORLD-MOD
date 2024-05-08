using System;
using System.Collections.Generic;
using System.Linq;

namespace Source.Analytics
{
    public class AnalyticsSystem
    {
        private List<UserAction> actions = new List<UserAction>();
        private List<SessionData> sessionRecords = new List<SessionData>();

        public void RecordAction(string type)
        {
            var action = new UserAction(type);
            actions.Add(action);
            Log.Message($"Action recorded: {type}");
        }

        public void LogSessionStart(DateTime startTime)
        {
            sessionRecords.Add(new SessionData { Start = startTime });
            Log.Message("Session started...");
        }

        public void LogSessionEnd(DateTime endTime)
        {
            var session = sessionRecords.LastOrDefault();
            if (session != null)
            {
                session.End = endTime;
                session.Duration = endTime.Subtract(session.Start);
                Log.Message("Session ended...");
            }
        }

        public void AnalyzeData()
        {
            AnalyzeActions();
            AnalyzeEngagement();
        }

        private void AnalyzeActions()
        {
            foreach (var action in actions)
            {
                Log.Message($"Analyzing user action: {action.Type}");
            }
        }

        private void AnalyzeEngagement()
        {
            var averageSessionTime = sessionRecords.Select(s => s.Duration).Average(d => d.TotalMinutes);
            Log.Message($"Average Session Duration: {averageSessionTime} minutes");
        }

        public void ReportFindings()
        {
            // Logic to format and report both action and engagement findings
        }
    }

    public class UserAction
    {
        public string Type { get; set; }
        public DateTime Time { get; set; }

        public UserAction(string type)
        {
            Type = type;
            Time = DateTime.Now;
        }
    }

    public class SessionData
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TimeSpan Duration { get; set; }
    }
}