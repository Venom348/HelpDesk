using System.Text;
using System.Text.Json;
using Category.Application.Abstractions;
using Category.Application.Exceptions;
using HelpDesk.Contracts.Messages.ProblematicApplication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Category.Infrastructure.Messaging.Consumers;

/// <summary>
///     Сервис, который слушает очередь RabbitMQ и обрабатывает событие о создании новой заявки.
/// </summary>
public class ApplicationCreatedConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ApplicationCreatedConsumer> _logger;
    private readonly IConfiguration _configuration;
    
    private IConnection _connection;
    private IChannel _channel;
    
    // Название Exchange
    private const string ExchangeName = "application.created";
    
    // Название очереди
    private const string QueueName = "category.application.created";

    public ApplicationCreatedConsumer(IServiceScopeFactory scopeFactory, ILogger<ApplicationCreatedConsumer> logger, IConfiguration configuration)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQ:HostName"],
        };
        
        _connection = await factory.CreateConnectionAsync(stoppingToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);
        
        // Объявление Exchange
        await _channel.ExchangeDeclareAsync(
            exchange: ExchangeName,
            type: ExchangeType.Fanout,
            durable: true,
            cancellationToken: stoppingToken);
        
        // Объявление очереди
        await _channel.QueueDeclareAsync(
            queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: stoppingToken);
        
        // Привязка очереди к Exchange
        await _channel.QueueBindAsync(
            queue: QueueName,
            exchange: ExchangeName,
            routingKey: string.Empty,
            cancellationToken: stoppingToken);
        
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += HandleMessageAsync;

        // Запуск прослушивание очереди
        await _channel.BasicConsumeAsync(
            queue: QueueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);
        
        // Сервис жив до остановки приложения
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    /// <summary>
    ///     Метод обработки полученного сообщения из очереди для сохранения заявки
    /// </summary>
    /// <param name="sender">Источник события (AsyncEventingBasicConsumer)</param>
    /// <param name="args">Данные полученного сообщения</param>
    private async Task HandleMessageAsync(object sender, BasicDeliverEventArgs args)
    {
        try
        {
            var body = Encoding.UTF8.GetString(args.Body.ToArray());
            var message = JsonSerializer.Deserialize<ApplicationCreatedMessage>(body);

            if (message is null)
            {
                _logger.LogWarning("Получено пустое сообщение ApplicationCreatedConsumer");
                await _channel.BasicNackAsync(args.DeliveryTag, false, false);
                return;
            }
            
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IApplicationEventRepository>();
            
            await repository.AddAsync(message.ApplicationId, message.CategoryId, CancellationToken.None);
            
            await _channel.BasicAckAsync(args.DeliveryTag, false);
            
            _logger.LogInformation("Заявка {ApplicationId} для категории {CategoryId} успешно сохранена.", message.ApplicationId, message.CategoryId);
        }
        catch (CategoryException ex)
        {
            _logger.LogError(ex, "Ошибка при обработке ApplicationCreatedConsumer.");
            
            await _channel.BasicNackAsync(args.DeliveryTag, false, true);
        }
    }
    
    /// <summary>
    ///     Метод освобождение ресурсов подключения к RabbitMQ при остановке сервиса.
    /// </summary>
    public override void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
        base.Dispose();
    }
}