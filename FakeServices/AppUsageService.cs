using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/* HAS THE FOLLOWING FUNCTIONS
 * FUNCTION - DAILY APPS RECORD
 * ARGUMENTS:
 * date to show
 * 
 * RETURNS: array of objects
 * [
 * {IconPath, AppName, Duration},
 * {IconPath, AppName, Duration in hours},
 * {IconPath, AppName, Duration in hours}
 * ]
 * 
 * FUNCTION - WEEKLY TIME RECORD
 * ARGUMENTS:
 * (int indexWeek, int month, int year)
 * 
 * RETURNS:
 * [n,m,x,z] array of ints 
*/
namespace DigitalWellBeingApp.FakeServices
{
    interface DailyRecord
    {
        string IconPath { get; }
        string AppName { get; }

        string Duration { get; }

    }
    class AppDailyRecord : DailyRecord
    {
        public string IconPath { get; set; } = string.Empty;
        public string AppName { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
    }

    class AppUsageService
    {
        public List<DailyRecord> GetDailyRecord(DateOnly date)
        {
            return new List<DailyRecord>
            {
                new AppDailyRecord { IconPath = "chrome.png", AppName = "Chrome", Duration = "1hr, 30mins" },
                new AppDailyRecord { IconPath = "vscode.png", AppName = "VSCode", Duration = "12mins" }
            };
        }

        public List<double> GetWeeklyRecord(int indexWeek, int month, int year)
        {
            return new List<double>
            {
                1,
                2,
                1.5,
                12
            };
        }
    }
}
