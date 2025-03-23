

using System.Text;
using Ferretly.TestTask.NotificationsApp.MessageBus.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory() { HostName = Environment.GetEnvironmentVariable("RabbitMQ_HostName") ?? "localhost" };
await using var connection = await factory.CreateConnectionAsync();
await using var channel = await connection.CreateChannelAsync();

var exchangeName = Environment.GetEnvironmentVariable("RabbitMQ_ExchangeName") ?? "time-tracking-exchange";
var queueName = Environment.GetEnvironmentVariable("RabbitMQ_QueueName") ?? "time-tracking-notifications-queue";

await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);

await channel.QueueDeclareAsync(queue: queueName,
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null);

await channel.QueueBindAsync(queue: queueName, exchange: exchangeName, routingKey: nameof(TrackNewActivityEvent));


var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += (_, eventArgs) =>
{
    var eventMessage = Encoding.UTF8.GetString(eventArgs.Body.Span);
    Console.WriteLine(eventMessage);
    return Task.CompletedTask;
};
await channel.BasicConsumeAsync(queue: queueName, autoAck: true, consumer: consumer);

Console.ReadLine();