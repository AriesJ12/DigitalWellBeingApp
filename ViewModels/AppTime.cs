using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using DigitalWellBeingApp.Models;

namespace DigitalWellBeingApp.ViewModels
{
    public partial class AppTimeViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<AppUsage> appUsages;

        public AppTimeViewModel()
        {
            AppUsages = new ObservableCollection<AppUsage>
            {
                new AppUsage { ProcessName = "Chrome", DurationSeconds = TimeSpan.FromSeconds(120) },
                new AppUsage { ProcessName = "VS Code", DurationSeconds = TimeSpan.FromSeconds(90) },
                new AppUsage { ProcessName = "Spotify", DurationSeconds = TimeSpan.FromSeconds(45) }
            };
        }
    }
}
