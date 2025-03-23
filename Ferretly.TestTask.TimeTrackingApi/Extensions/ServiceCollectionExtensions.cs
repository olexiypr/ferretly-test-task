using Ferretly.TestTask.TimeTrackingApi.Mappers;
using Ferretly.TestTask.TimeTrackingApi.MessageBus;
using Ferretly.TestTask.TimeTrackingApi.Services.Implementations;
using Ferretly.TestTask.TimeTrackingApi.Services.Interfaces;

namespace Ferretly.TestTask.TimeTrackingApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        //Can be implemented using reflections or attributes
        services.AddTransient<IActivityService, ActivityService>();
        services.AddTransient<IProjectService, ProjectService>();
        services.AddTransient<IMessageBusService, MessageBusService>();
        services.AddTransient<IActivityMapper, ActivityMapper>();
        return services;
    }
}