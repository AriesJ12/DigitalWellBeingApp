using DigitalWellBeingApp.Models.Enums;

namespace DigitalWellBeingApp.Models
{
    public class CategoryDebt
    {
        public int Id { get; set; }

        // e.g., "Entertainment" is the category being limited
        public AppCategory SourceCategory { get; set; }

        // e.g., "Productive" is the category you need to balance against
        public AppCategory TargetCategory { get; set; }

        // e.g., 2 means you can do 2h Entertainment for every 1h Productive
        public double Ratio { get; set; }

        // e.g., after how many hours of SourceCategory before reminder triggers
        public double TriggerHours { get; set; }


        // Computed properties for UI display
        public string SourceCategoryName => SourceCategory.ToString();
        public string TargetCategoryName => TargetCategory.ToString();
    }

}
