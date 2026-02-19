using Category.Application.Abstractions;
using Category.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Category.Infrastructure.Repositories;

/// <inheritdoc cref="IApplicationEventRepository" />
public class ApplicationEventRepository : IApplicationEventRepository
{
    private readonly ApplicationContext _dbContext;

    public ApplicationEventRepository(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Добавление записи о новой заявке
    public async Task AddAsync(int applicationId, int categoryId, CancellationToken cancellationToken)
    {
        var applicationEvent = new ApplicationEvent
        {
            ApplicationId = applicationId,
            CategoryId = categoryId,
            IsActive = true
        };
        
        await _dbContext.ApplicationEvents.AddAsync(applicationEvent, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    // Изменение статуса активной заявки
    public async Task UpdateStatusAsync(int applicationId, bool isActive, CancellationToken cancellationToken)
    {
        var applicationEvent = await _dbContext.ApplicationEvents.FirstOrDefaultAsync(s => s.ApplicationId == applicationId, cancellationToken);

        // Если записи нет - Consumer пропустил событие создания и создаём новую запись
        if (applicationEvent is null)
        {
            applicationEvent = new ApplicationEvent
            {
                ApplicationId = applicationId,
                CategoryId = 0,
                IsActive = isActive
            };

            await _dbContext.ApplicationEvents.AddAsync(applicationEvent, cancellationToken);
        }
        else
        {
            applicationEvent.IsActive = isActive;
            _dbContext.ApplicationEvents.Update(applicationEvent);
        }
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    // Проверка активной заявки у категории
    public async Task<bool> HasActiveApplicationAsync(int categoryId, CancellationToken cancellationToken)
    {
        return await _dbContext.ApplicationEvents
            .AnyAsync(s => s.CategoryId == categoryId && s.IsActive, cancellationToken);
    }
}