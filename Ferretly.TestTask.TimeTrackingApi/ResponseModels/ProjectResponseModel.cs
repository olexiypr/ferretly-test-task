namespace Ferretly.TestTask.TimeTrackingApi.ResponseModels;

public class ProjectResponseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
}