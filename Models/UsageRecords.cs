using System;

namespace DigitalWellBeingApp.Models
{
    /// <summary>
    /// Represents how long a process (app) has been used.
    /// </summary>
    public class UsageRecord : AppUsage
    {
        public UsageRecord(string processName)
        {
            ProcessName = processName;
            DurationSeconds = TimeSpan.Zero;
        }

        /// <summary>
        /// Add time to the record (used when the app is active).
        /// </summary>
        public void AddTime(TimeSpan timeSpent)
        {
            DurationSeconds += timeSpent;
        }
    }
}
