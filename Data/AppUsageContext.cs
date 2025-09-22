using Microsoft.EntityFrameworkCore;
using DigitalWellBeingApp.Models;

namespace DigitalWellBeingApp.Data
{
    public class AppUsageContext : DbContext
    {
        public DbSet<AppUsage> AppUsages { get; set; }

        public string DbPath { get; }

        public AppUsageContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "app_usage.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Creates a SQLite DB file in your project folder
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        

    }
    
}
