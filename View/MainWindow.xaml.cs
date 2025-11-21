using System;
using System.Windows;
using DigitalWellBeingApp.Services;

namespace DigitalWellBeingApp.View
{
    public partial class MainWindow : Window
    {
        private ActiveWindowTracker _tracker;
        public MainWindow()
        {
            // get the global tracker from App.xaml.cs
            _tracker = ((App)System.Windows.Application.Current).Tracker;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            // flush the shared tracker data, not a new one
            _tracker.FlushToDb();
        }

        private void NumericOnly(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }

    }
}
