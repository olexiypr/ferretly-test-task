namespace Ferretly.TestTask.TimeTrackingApi.MessageBus;

public interface IMessageBusService
{
    Task PublishAsync<TMessage>(TMessage message);
}