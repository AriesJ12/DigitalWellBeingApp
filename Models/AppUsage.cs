using System;

namespace DigitalWellBeingApp.Models
{
    public class AppUsage
    {
        public int Id { get; set; }
        public string ProcessName { get; set; }
        public DateTime UsageDate { get; set; }
        public TimeSpan DurationSeconds { get; set; }
    }
}
