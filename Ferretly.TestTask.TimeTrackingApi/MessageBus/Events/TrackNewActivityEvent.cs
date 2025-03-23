namespace Ferretly.TestTask.TimeTrackingApi.MessageBus.Events;

public class TrackNewActivityEvent
{
    public int ProjectId { get; set; }
    public int RoleId { get; set; }
    public int EmployeeId { get; set; }
    public int ActivityTypeId { get; set; }
    public DateTimeOffset StartDate { get; set; }
}