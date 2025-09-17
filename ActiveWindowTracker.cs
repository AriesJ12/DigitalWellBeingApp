using System;
using System.Collections.Generic;

public class ActiveWindowTracker
{
    private readonly Dictionary<string, TimeSpan> usageData;
    private string? currentApp;
    private DateTime lastCheck;

    public ActiveWindowTracker()
    {
        usageData = new Dictionary<string, TimeSpan>();
        lastCheck = DateTime.Now;
    }

    /// <summary>
    /// Call this when you detect the active process has changed.
    /// </summary>
    public void NotifyActiveProcess(string? processName)
    {
        DateTime now = DateTime.Now;

        if (currentApp != null)
        {
            var duration = now - lastCheck;
            if (usageData.ContainsKey(currentApp))
                usageData[currentApp] += duration;
            else
                usageData[currentApp] = duration;
        }

        currentApp = processName;
        lastCheck = now;
    }

    public Dictionary<string, TimeSpan> GetUsage()
    {
        return new Dictionary<string, TimeSpan>(usageData);
    }
}
