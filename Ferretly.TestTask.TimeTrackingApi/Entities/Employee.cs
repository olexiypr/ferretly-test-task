namespace Ferretly.TestTask.TimeTrackingApi.Entities;

public class Employee : BaseEntity
{
    public string Name { get; set; }
    public DateOnly Birthday { get; set; }
    public Sexes Sex { get; set; }
    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
}