using Ferretly.TestTask.TimeTrackingApi.Entities;
using Ferretly.TestTask.TimeTrackingApi.MessageBus.Events;
using Ferretly.TestTask.TimeTrackingApi.RequestModels;
using Ferretly.TestTask.TimeTrackingApi.ResponseModels;

namespace Ferretly.TestTask.TimeTrackingApi.Mappers;

public interface IActivityMapper
{
    ActivityResponseModel MapToResponseModel(Activity activity);
    Activity Map(int userId, NewActivityRequestModel activityRequestModel);
    TrackNewActivityEvent Map(Activity activity);
    StartedActivityResponseModel MapToStartedActivityResponseModel(Activity activity);
}