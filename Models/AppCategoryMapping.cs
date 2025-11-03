using System;
using DigitalWellBeingApp.Models.Enums;

namespace DigitalWellBeingApp.Models
{
    public class AppCategoryMapping
    {
        public int Id { get; set; }
        public string ProcessName { get; set; } = string.Empty;
        public AppCategory Category { get; set; } 
    }
}
