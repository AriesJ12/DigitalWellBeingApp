using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using DigitalWellBeingApp.FakeServices;

using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace DigitalWellBeingApp.ViewModels
{
    public partial class WeeklyUsageChartViewModel : ObservableObject
    {
        private readonly AppUsageService _service = new AppUsageService();

        [ObservableProperty]
        private ObservableCollection<WeeklyRecord> appUsages = new();

        [ObservableProperty]
        private ISeries[] weeklySeries = [];

        [ObservableProperty]
        private List<Axis> xAxes = new();

        public WeeklyUsageChartViewModel()
        {
            LoadData(1,2,3);
        }

        public void LoadData(int indexWeek, int month, int year)
        {
            var data = _service.GetWeeklyRecord(indexWeek, month, year);
            AppUsages = new ObservableCollection<WeeklyRecord>(data);

            WeeklySeries = new ISeries[]
            {
                new ColumnSeries<double>
                {
                    Name = "App Usage (hrs)",
                    Values = AppUsages.Select(x => x.Hours).ToArray()
                }
            };

            XAxes = new List<Axis>
            {
                new Axis
                {
                    Name = "Days",
                    Labels = AppUsages.Select(x => x.Day).ToArray()
                }
            };
        }
    }
}
