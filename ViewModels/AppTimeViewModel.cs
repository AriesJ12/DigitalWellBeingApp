using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using DigitalWellBeingApp.FakeServices;


namespace DigitalWellBeingApp.ViewModels
{
    public partial class AppTimeViewModel : ObservableObject
    {
        private readonly AppUsageService _service = new AppUsageService();

        [ObservableProperty]
        private ObservableCollection<DailyRecord> appUsages = new();

        public AppTimeViewModel()
        {
            LoadData(DateOnly.FromDateTime(DateTime.Today));
        }

        public void LoadData(DateOnly date)
        {
            var data = _service.GetDailyRecord(date);
            AppUsages = new ObservableCollection<DailyRecord>(data);
        }
    }
}
