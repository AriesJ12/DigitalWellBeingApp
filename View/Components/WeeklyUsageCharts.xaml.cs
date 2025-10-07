using System;
using System.Windows.Controls;
using DigitalWellBeingApp.ViewModels.Components;


namespace DigitalWellBeingApp.View.Components
{
    public partial class WeeklyUsageChart : UserControl
    {
        public WeeklyUsageChart()
        {
            DataContext = new WeeklyUsageChartViewModel();
        }
    }
}
