using Ferretly.TestTask.TimeTrackingApi.RequestModels;
using Ferretly.TestTask.TimeTrackingApi.ResponseModels;
using Ferretly.TestTask.TimeTrackingApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ferretly.TestTask.TimeTrackingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivitiesController(IActivityService activityService) : ControllerBase
{
    [HttpPost("start-tracking")]
    public async Task<StartedActivityResponseModel> TrackNewActivity([FromBody] NewActivityRequestModel activityRequestModel)
    {
        return await activityService.TrackNewActivity(0, activityRequestModel);
    }

    [HttpPost("{id:int}/stop-tracking")]
    public async Task<DateTimeOffset> StopTrackingActivity(int id)
    {
        return await activityService.StopTrackingActivity(id);
    }

    [HttpGet("by-date")]
    public async Task<IEnumerable<ActivityResponseModel>> GetActivitiesByDate([FromQuery] DateOnly date)
    {
        return await activityService.GetActivitiesByDate(date, 0);
    }

    [HttpGet("by-week")]
    public async Task<IEnumerable<ActivityResponseModel>> GetActivitiesByWeek([FromQuery] int weekNumber, [FromQuery] int yearNumber)
    {
        return await activityService.GetActivitiesByWeek(0, weekNumber, yearNumber);
    }
}