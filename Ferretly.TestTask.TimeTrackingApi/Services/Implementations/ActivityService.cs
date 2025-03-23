using Ferretly.TestTask.TimeTrackingApi.DbContext;
using Ferretly.TestTask.TimeTrackingApi.Entities;
using Ferretly.TestTask.TimeTrackingApi.Exceptions;
using Ferretly.TestTask.TimeTrackingApi.Mappers;
using Ferretly.TestTask.TimeTrackingApi.MessageBus;
using Ferretly.TestTask.TimeTrackingApi.RequestModels;
using Ferretly.TestTask.TimeTrackingApi.ResponseModels;
using Ferretly.TestTask.TimeTrackingApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ferretly.TestTask.TimeTrackingApi.Services.Implementations;

public class ActivityService(TimeTrackerDbContext trackerDbContext, IMessageBusService messageBusService, IActivityMapper activityMapper) : IActivityService
{
    public async Task<StartedActivityResponseModel> TrackNewActivity(int userId, NewActivityRequestModel activityRequestModel)
    {
        var unfinishedActivityId = await GetUnfinishedActivityIdIfExistsForUser(userId);
        if (unfinishedActivityId.HasValue)
        {
            await StopTrackingActivity(unfinishedActivityId.Value);
        }
        var activity = activityMapper.Map(userId, activityRequestModel);
        
        await trackerDbContext.Activities.AddAsync(activity);
        await trackerDbContext.SaveChangesAsync();
        await messageBusService.PublishAsync(activityMapper.Map(activity));
        
        return activityMapper.MapToStartedActivityResponseModel(activity);
    }

    public async Task<DateTimeOffset> StopTrackingActivity(int id)
    {
        var activity = await trackerDbContext.Activities.FindAsync(id);
        if (activity is null)
        {
            throw new EntityNotFoundException(nameof(Activity), id);
        }
        activity.EndDate = DateTimeOffset.UtcNow;
        await trackerDbContext.SaveChangesAsync();
        return activity.EndDate.Value;
    }

    public async Task<IEnumerable<ActivityResponseModel>> GetActivitiesByDate(DateOnly date, int userId)
    {
        var dateTimeOffsetToday = new DateTimeOffset(date.Year, date.Month, date.Day, 0, 0, 0, TimeSpan.Zero);
        var activities = await GetActivitiesForUser(userId)
            //Not sure that it's correct filters, should be tested
            .Where(a => a.StartDate > dateTimeOffsetToday)
            .ToListAsync();
        return activities.Select(activityMapper.MapToResponseModel);
    }

    public async Task<IEnumerable<ActivityResponseModel>> GetActivitiesByWeek(int userId, int weekNumber, int year)
    {
        var (firstDayOfWeek, lastDayOfWeek) = GetFirstAndLastDayOfWeek(year, year);
        var activities = await GetActivitiesForUser(userId)
            //Not sure that it's correct filters, should be tested
            .Where(a => a.StartDate > firstDayOfWeek && a.StartDate < lastDayOfWeek)
            .ToListAsync();
        return activities.Select(activityMapper.MapToResponseModel);
    }

    private async Task<int?> GetUnfinishedActivityIdIfExistsForUser(int userId)
    {
        var activity = await trackerDbContext.Activities.FirstOrDefaultAsync(a => a.EmployeeId == userId && a.EndDate == null);
        return activity?.Id;
    }

    private IQueryable<Activity> GetActivitiesForUser(int userId)
    {
        return trackerDbContext.Activities
            .Include(a => a.Role)
            .Include(a => a.ActivityType)
            .Where(a => a.EmployeeId == userId);
    }

    private (DateTimeOffset, DateTimeOffset) GetFirstAndLastDayOfWeek(int year, int weekOfYear)
    {
        //Should be moved to separate class
        var givenYear = new DateTimeOffset(year, 1, 1, 0, 0, 0, TimeSpan.Zero);
        if (givenYear.DayOfWeek != DayOfWeek.Monday)
        {
            while (givenYear.DayOfWeek != DayOfWeek.Monday)
            {
                givenYear = givenYear.AddDays(1);
            }
        }

        if (weekOfYear == 1)
        {
            return (new DateTimeOffset(year, 1, 1, 0, 0, 0, TimeSpan.Zero), givenYear);
        }
        
        var firstDayOfWeek = givenYear.AddDays(weekOfYear - 1 * 7);
        return (firstDayOfWeek, firstDayOfWeek.AddDays(7));
    }
}