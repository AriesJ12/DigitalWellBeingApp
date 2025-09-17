using System;

namespace DigitalWellBeingApp.Models
{
    /// <summary>
    /// Represents how long a process (app) has been used.
    /// </summary>
    public class UsageRecord
    {
        public string ProcessName { get; set; }   // e.g. "chrome"
        public TimeSpan Duration { get; set; }    // how long it's been active
        public DateTime LastUpdated { get; set; } // last time we tracked it

        public UsageRecord(string processName)
        {
            ProcessName = processName;
            Duration = TimeSpan.Zero;
            LastUpdated = DateTime.Now;
        }

        /// <summary>
        /// Add time to the record (used when the app is active).
        /// </summary>
        public void AddTime(TimeSpan timeSpent)
        {
            Duration += timeSpent;
            LastUpdated = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{ProcessName} - {Duration}";
        }
    }
}
