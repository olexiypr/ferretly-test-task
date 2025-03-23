namespace Ferretly.TestTask.TimeTrackingApi.ResponseModels;

public class ActivityResponseModel
{
    public string RoleName { get; set; }
    public string ActivityName { get; set; }
    public int ProjectId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset? EndTime { get; set; }
}