namespace Ferretly.TestTask.TimeTrackingApi.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    //We need it to be able to get activities related to this project even if it was deleted.
    //Otherwise we won't be able to get project's name
    public bool IsDeleted { get; set; } 
}