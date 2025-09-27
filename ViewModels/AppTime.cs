using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using DigitalWellBeingApp.Models;
using DigitalWellBeingApp.Services;
using System;

namespace DigitalWellBeingApp.ViewModels
{
    public partial class AppTimeViewModel : ObservableObject
    {
        private readonly AppUsageService _service = new AppUsageService();

        [ObservableProperty]
        private ObservableCollection<AppUsage> appUsages = new();

        public AppTimeViewModel()
        {
            LoadData(DateTime.Today);
        }

        public void LoadData(DateTime date)
        {
            var data = _service.GetAppUsagesByDate(date);
            AppUsages = new ObservableCollection<AppUsage>(data);
        }
    }
}
