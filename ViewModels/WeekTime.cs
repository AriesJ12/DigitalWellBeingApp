using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DigitalWellBeingApp.ViewModels.Components
{
    public partial class WeeklyUsageChartViewModel : ObservableObject
    {
        [ObservableProperty]
        private SeriesCollection weeklySeries;

        [ObservableProperty]
        private List<string> weekDays;

        public WeeklyUsageChartViewModel()
        {
            weekDays = new List<string> { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

            weeklySeries = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "App Usage",
                    Values = new ChartValues<double> { 2.5, 3, 1.5, 4, 2, 3.5, 1 }
                }
            };
        }
    }
}
