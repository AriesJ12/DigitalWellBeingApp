using System.Configuration;
using System.Data;
using System.Windows;
using DigitalWellBeingApp.Services;

namespace DigitalWellBeingApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ActiveWindowTracker tracker;
    private readonly ActiveWindowWatcher watcher;

    public App()
    {
        tracker = new ActiveWindowTracker();
        watcher = new ActiveWindowWatcher();
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        watcher.OnActiveWindowChanged += (procName, pid, hwnd) =>
        {
            tracker.NotifyActiveProcess(procName);
        };

        watcher.Start();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
        watcher.Dispose();

        // Cleanup logic here
    }
}

