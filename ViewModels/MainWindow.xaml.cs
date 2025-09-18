using System;
using System.Windows;
using DigitalWellBeingApp.Services;

namespace DigitalWellBeingApp.ViewModels 
{
    public partial class MainWindow : Window
    {
        private readonly ActiveWindowTracker tracker;
        private readonly ActiveWindowWatcher watcher;

        public MainWindow()
        {
            InitializeComponent();

            tracker = new ActiveWindowTracker();
            watcher = new ActiveWindowWatcher();

            watcher.OnActiveWindowChanged += (procName, pid, hwnd) =>
            {
                tracker.NotifyActiveProcess(procName);

                // update the UI safely
                Dispatcher.Invoke(() =>
                {
                    ActiveProcessText.Text = $"Active: {procName}";
                });
            };

            watcher.Start();
        }
    }
}
