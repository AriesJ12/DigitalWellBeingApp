using System;

namespace DigitalWellBeingApp.Models
{
    public class AppCategoryMapping
    {
        public int Id { get; set; }
        public string ProcessName { get; set; } = string.Empty;

        public string WindowTitle { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}
