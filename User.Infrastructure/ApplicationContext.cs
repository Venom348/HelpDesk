using Microsoft.EntityFrameworkCore;

namespace User.Infrastructure;

/// <summary>
///     Модель подключения к БД
/// </summary>
public class ApplicationContext : DbContext
{
    // Определение сущности
    public DbSet<Domain.Entities.User> Users => Set<Domain.Entities.User>();
    
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        //  Проверка существования и создание БД
        if (Database.EnsureCreated())
        {
            Init();
        }
    }

    private void Init()
    {
        SaveChanges();
    }
}