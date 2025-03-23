namespace Ferretly.TestTask.TimeTrackingApi.Entities;

public class Activity : BaseEntity
{
    public Project? Project { get; set; }
    public int ProjectId { get; set; }
    public Employee? Employee { get; set; }
    public int EmployeeId { get; set; }
    public ActivityType? ActivityType { get; set; }
    public int ActivityTypeId { get; set; }
    public Role? Role { get; set; }
    public int RoleId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; } = null;
}