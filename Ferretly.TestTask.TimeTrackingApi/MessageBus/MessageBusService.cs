using System.Text.Json;
using RabbitMQ.Client;

namespace Ferretly.TestTask.TimeTrackingApi.MessageBus;

public class MessageBusService(IConfiguration configuration) : IMessageBusService
{
    private const string DefaultExchangeName = "time-tracking-exchange";
    
    public async Task PublishAsync<TMessage>(TMessage message)
    {
        var exchangeName = configuration["RabbitMq:DefaultExchangeName"] ?? DefaultExchangeName;
        var factory = new ConnectionFactory { HostName = configuration["RabbitMq:Host"] ?? "localhost" };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();
        await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);
        
        var body = JsonSerializer.SerializeToUtf8Bytes(message, message.GetType());
        await channel.BasicPublishAsync(
            exchange: exchangeName,
            routingKey: message.GetType().Name,
            mandatory: true,
            body: body);
    }
}