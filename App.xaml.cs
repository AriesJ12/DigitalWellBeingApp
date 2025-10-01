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
    public ActiveWindowTracker Tracker { get; } = new ActiveWindowTracker();
    private readonly ActiveWindowWatcher _watcher;

    public App()
    {
        Tracker = new ActiveWindowTracker();
        _watcher = new ActiveWindowWatcher();
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _watcher.OnActiveWindowChanged += (procName, pid, hwnd) =>
        {
            Tracker.NotifyActiveProcess(procName);
        };

        _watcher.Start();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
        _watcher.Dispose();
        Tracker.FlushToDb();

        // Cleanup logic here
    }
}

