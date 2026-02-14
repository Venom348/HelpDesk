using Microsoft.EntityFrameworkCore;

namespace ProblematicApplication.Infrastructure;

/// <summary>
///     Модель подключения к БД
/// </summary>
public class ApplicationContext : DbContext
{
    // Определение сущности
    public DbSet<Domain.Entities.ProblematicApplication> ProblematicApplications => Set<Domain.Entities.ProblematicApplication>();

    public ApplicationContext(DbContextOptions options) : base(options)
    {
        // Проверка существования и создание БД
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