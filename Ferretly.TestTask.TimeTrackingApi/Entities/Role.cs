namespace Ferretly.TestTask.TimeTrackingApi.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
}