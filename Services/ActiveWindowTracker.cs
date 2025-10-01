using System;
using System.Collections.Generic;
using DigitalWellBeingApp.Models;

namespace DigitalWellBeingApp.Services
{
    public class ActiveWindowTracker
    {
        private readonly Dictionary<string, UsageRecord> _usage = new();
        private string _lastProcess;
        private DateTime _lastSwitchTime;
        private readonly AppUsageService _service = new AppUsageService();

        public ActiveWindowTracker()
        {
            _lastProcess = "";
            _lastSwitchTime = DateTime.Now;

            // flush every 10 minutes
            var timer = new System.Timers.Timer(10 * 60 * 1000);
            timer.Elapsed += (s, e) => FlushToDb();
            timer.Start();
        }

        public void NotifyActiveProcess(string processName)
        {
            var now = DateTime.Now;

            if (!string.IsNullOrEmpty(_lastProcess))
            {
                var timeSpent = now - _lastSwitchTime;

                if (!_usage.ContainsKey(_lastProcess))
                    _usage[_lastProcess] = new UsageRecord(_lastProcess);

                _usage[_lastProcess].AddTime((int)timeSpent.TotalSeconds);
            }

            _lastProcess = processName;
            _lastSwitchTime = now;
        }

        public void FlushToDb()
        {
            foreach (var record in _usage.Values)
            {
                _service.TrackAppUsage(record.ProcessName, record.DurationSeconds);
                record.Reset(); // optional, so you don’t double count
            }
        }
    }
}
