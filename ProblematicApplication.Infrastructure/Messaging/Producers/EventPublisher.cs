using System.Text;
using System.Text.Json;
using HelpDesk.Contracts.Messages.ProblematicApplication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace ProblematicApplication.Infrastructure.Messaging.Producers;

/// <summary>
///     Сервис, который публикует события о заявках в RabbitMQ
/// </summary>
public class EventPublisher : IDisposable
{
    private readonly ILogger<EventPublisher> _logger;
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public EventPublisher(IConfiguration configuration,ILogger<EventPublisher> logger)
    {
        _logger = logger;

        var factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQ:HostName"],
        };
        
        _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
    }

    public async Task PublishApplicationCreatedAsync(int applicationId, int categoryId, CancellationToken cancellationToken)
    {
        // Название Exchange
        const string exchangeName = "application.created";
        
        // Объявление Exchange
        await _channel.ExchangeDeclareAsync(
            exchange: exchangeName,
            type: ExchangeType.Fanout,
            durable: true,
            cancellationToken: cancellationToken);

        var message = new ApplicationCreatedMessage
        {
            ApplicationId = applicationId,
            CategoryId = categoryId
        };
        
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        // Публикация сообщения в Exchange
        await _channel.BasicPublishAsync(
            exchange: exchangeName,
            routingKey: string.Empty,
            body: body,
            cancellationToken: cancellationToken);
        
        _logger.LogInformation(
            "Опубликовано событие ApplicationCreated: ApplicationId={ApplicationId}, CategoryId={CategoryId}",
            applicationId,
            categoryId);
    }

    public async Task PublishApplicationStatusChangedAsync(int applicationId, int categoryId, bool isActive,
        CancellationToken cancellationToken)
    {
        // Название Exchange
        const string exchangeName = "application.status.changed";
        
        // Объявление Exchange
        await _channel.ExchangeDeclareAsync(
            exchange: exchangeName,
            type: ExchangeType.Fanout,
            durable: true,
            cancellationToken: cancellationToken);

        var message = new ApplicationStatusChangedMessage
        {
            ApplicationId = applicationId,
            CategoryId = categoryId,
            IsActive = isActive
        };
        
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        
        // Публикация сообщения в Exchange
        await _channel.BasicPublishAsync(
            exchange: exchangeName,
            routingKey: string.Empty,
            body: body,
            cancellationToken: cancellationToken);
        
        _logger.LogInformation(
            "Опубликовано событие ApplicationStatusChanged: ApplicationId={ApplicationId}, IsActive={IsActive}",
            applicationId,
            isActive);
    }

    // Освобождение ресурсов подключения к RabbitMQ
    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}