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

        public ActiveWindowTracker()
        {
            _lastProcess = "";
            _lastSwitchTime = DateTime.Now;
        }

        public void NotifyActiveProcess(string processName)
        {
            var now = DateTime.Now;

            // Add time to the last process
            if (!string.IsNullOrEmpty(_lastProcess))
            {
                var timeSpent = now - _lastSwitchTime;

                if (!_usage.ContainsKey(_lastProcess))
                    _usage[_lastProcess] = new UsageRecord(_lastProcess);

                _usage[_lastProcess].AddTime((int)timeSpent.TotalSeconds);
            }

            // Switch to new process
            _lastProcess = processName;
            _lastSwitchTime = now;
        }

        public IReadOnlyDictionary<string, UsageRecord> GetUsage() => _usage;

        public void StoreUsage(string processName)
        {
            
        }
    }
}
