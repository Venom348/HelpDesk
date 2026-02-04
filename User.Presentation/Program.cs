using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using User.Application.Implementations.Services;
using User.Domain.Abstractions.Repositories;
using User.Domain.Abstractions.Services;
using User.Domain.Mapping;
using User.Infrastructure;
using User.Infrastructure.Repositories;

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

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("Psql")));
builder.Services.AddTransient<IBaseRepository<User.Domain.Entities.User>, UserRepository>();
builder.Services.AddAutoMapper(opt => opt.AddProfile<UserProfile>());

var app = builder.Build();

app.MapControllers(); // Маршрутизация контроллеров
app.UseSwagger(); // Включает middleware для генерации Swagger JSON
app.UseSwaggerUI(); // Включает веб-интерфейс для документации API
app.UseHttpsRedirection(); // Перенаправляет HTTP запросы на HTTPS

app.Run();