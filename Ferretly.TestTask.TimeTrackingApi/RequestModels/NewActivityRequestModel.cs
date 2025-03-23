namespace Ferretly.TestTask.TimeTrackingApi.RequestModels;

public class NewActivityRequestModel
{
    public int RoleId { get; set; }
    public int ProjectId { get; set; }
    public int ActivityTypeId { get; set; }
}