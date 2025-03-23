using Ferretly.TestTask.TimeTrackingApi.Entities;
using Ferretly.TestTask.TimeTrackingApi.MessageBus.Events;
using Ferretly.TestTask.TimeTrackingApi.RequestModels;
using Ferretly.TestTask.TimeTrackingApi.ResponseModels;

namespace Ferretly.TestTask.TimeTrackingApi.Mappers;

public class ActivityMapper : IActivityMapper
{
    public ActivityResponseModel MapToResponseModel(Activity activity)
    {
        return new ActivityResponseModel
        {
            ActivityName = activity.ActivityType?.Name ?? string.Empty,
            ProjectId = activity.ProjectId,
            EndTime = activity.EndDate,
            RoleName = activity.Role?.Name ?? string.Empty,
            StartDate = activity.StartDate,
        };
    }

    public Activity Map(int userId, NewActivityRequestModel activityRequestModel)
    {
        return new Activity
        {
            ProjectId = activityRequestModel.ProjectId,
            RoleId = activityRequestModel.RoleId,
            ActivityTypeId = activityRequestModel.ActivityTypeId,
            StartDate = DateTimeOffset.UtcNow,
            EmployeeId = userId
        };
    }

    public TrackNewActivityEvent Map(Activity activity)
    {
        return new TrackNewActivityEvent
        {
            ProjectId = activity.ProjectId,
            EmployeeId = activity.EmployeeId,
            RoleId = activity.RoleId,
            ActivityTypeId = activity.ActivityTypeId,
            StartDate = activity.StartDate,
        };
    }

    public StartedActivityResponseModel MapToStartedActivityResponseModel(Activity activity)
    {
        return new StartedActivityResponseModel
        {
            Id = activity.Id,
            StartDate = activity.StartDate,
        };
    }
}