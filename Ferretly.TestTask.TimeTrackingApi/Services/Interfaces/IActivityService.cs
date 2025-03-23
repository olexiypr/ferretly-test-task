using Ferretly.TestTask.TimeTrackingApi.RequestModels;
using Ferretly.TestTask.TimeTrackingApi.ResponseModels;

namespace Ferretly.TestTask.TimeTrackingApi.Services.Interfaces;

public interface IActivityService
{
    Task<StartedActivityResponseModel> TrackNewActivity(int userId, NewActivityRequestModel activityRequestModel);
    Task<DateTimeOffset> StopTrackingActivity(int id);
    Task<IEnumerable<ActivityResponseModel>> GetActivitiesByDate(DateOnly date, int userId);
    Task<IEnumerable<ActivityResponseModel>> GetActivitiesByWeek(int userId, int weekNumber, int year);
}