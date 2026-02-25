using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ProblematicApplication.Application.Implementations.Services;
using ProblematicApplication.Domain.Abstractions.Repositories;
using ProblematicApplication.Domain.Abstractions.Services;
using ProblematicApplication.Domain.Mapping;
using ProblematicApplication.Infrastructure;
using ProblematicApplication.Infrastructure.Messaging.Producers;
using ProblematicApplication.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration; // Получение доступа к конфигурации

// Регистрирует сервисы для работы с контроллерами и
// включает сериализацию enum в строковом виде для JSON
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IProblematicApplicationService, ProblematicApplicationService>();
builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("Psql")));
builder.Services
    .AddTransient<IBaseRepository<ProblematicApplication.Domain.Entities.ProblematicApplication>,
        ProblematicApplicationRepository>();
builder.Services.AddSingleton<EventPublisher>();
builder.Services.AddAutoMapper(opt => opt.AddProfile<ProblematicApplicationProfile>());

var app = builder.Build();

app.MapControllers(); // Маршрутизация контроллеров
app.UseSwagger(); // Включает middleware для генерации Swagger JSON
app.UseSwaggerUI(); // Включает веб-интерфейс для документации API
app.UseHttpsRedirection(); // Перенаправляет HTTP запросы на HTTPS

app.Run();