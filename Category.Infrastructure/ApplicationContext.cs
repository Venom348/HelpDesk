using Microsoft.EntityFrameworkCore;

namespace Category.Infrastructure;

/// <summary>
///     Модель подключения к БД
/// </summary>
public class ApplicationContext : DbContext
{
    // Определение сущности
    public DbSet<Domain.Entities.Category>  Categories => Set<Domain.Entities.Category>();

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