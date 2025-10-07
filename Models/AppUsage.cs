using System;

namespace DigitalWellBeingApp.Models
{
    public class AppUsage
    {
        public int Id { get; set; }
        public string ProcessName { get; set; } = string.Empty;
        public DateTime UsageDate { get; set; }
        public int DurationSeconds { get; set; }

        public string DurationFormatted
        {
            get
            {
                var time = TimeSpan.FromSeconds(DurationSeconds);
                return time.ToString(@"hh\:mm\:ss");
            }
        }
    }
}
