using Microsoft.EntityFrameworkCore;
using DigitalWellBeingApp.Models;

namespace DigitalWellBeingApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AppUsage> AppUsages { get; set; }
        public DbSet<AppCategoryMapping> AppCategoryMappings { get; set; }

        public string DbPath { get; }

        public AppDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "digital_wellbeing.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Creates a SQLite DB file in your project folder
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppCategoryMapping>()
            .Property(m => m.Category)
            .HasConversion<string>();
            
            modelBuilder.Entity<AppCategoryMapping>()
                .HasIndex(m => m.ProcessName)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

    }

}
