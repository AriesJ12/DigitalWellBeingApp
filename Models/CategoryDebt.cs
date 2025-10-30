using DigitalWellBeingApp.Models.Enums;

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
}
