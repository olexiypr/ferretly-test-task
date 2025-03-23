namespace Ferretly.TestTask.TimeTrackingApi.RequestModels;

public class ProjectRequestModel
{
    public string Name { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
}