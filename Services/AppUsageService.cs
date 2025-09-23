using System;
using System.Linq;
using DigitalWellBeingApp.Data;
using DigitalWellBeingApp.Models;

namespace DigitalWellBeingApp.Services
{
    public class AppUsageService
    {
        // records app usage
        public void TrackAppUsage(string processName, int durationSeconds)
        {
            using (var db = new AppUsageContext())
            {
                var today = DateTime.Today;

                var existing = db.AppUsages
                                 .FirstOrDefault(a => a.ProcessName == processName &&
                                                      a.UsageDate == today);

                if (existing != null)
                {
                    existing.DurationSeconds += TimeSpan.FromSeconds(durationSeconds);
                }
                else
                {
                    db.AppUsages.Add(new AppUsage
                    {
                        ProcessName = processName,
                        UsageDate = today,
                        DurationSeconds = TimeSpan.FromSeconds(durationSeconds)
                    });
                }

                db.SaveChanges();
            }
        }
        // get app usage by date
        public List<AppUsage> GetAppUsagesByDate(DateTime date)
        {
            using (var db = new AppUsageContext())
            {
                return db.AppUsages
                         .Where(a => a.UsageDate == date.Date)
                         .OrderByDescending(a => a.DurationSeconds) // optional: sort longest first
                         .ToList();
            }
        }
    }
}
