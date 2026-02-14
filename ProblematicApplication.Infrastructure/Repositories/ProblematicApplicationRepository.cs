using Microsoft.EntityFrameworkCore;
using ProblematicApplication.Domain.Abstractions.Repositories;

namespace ProblematicApplication.Infrastructure.Repositories;

/// <inheritdoc cref="IBaseRepository{ProblematicApplication}" />
public class ProblematicApplicationRepository : IBaseRepository<Domain.Entities.ProblematicApplication>
{
    private readonly ApplicationContext _dbContext;

    public ProblematicApplicationRepository(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // Получение всех записей
    public IQueryable<Domain.Entities.ProblematicApplication> GetAll() => _dbContext.ProblematicApplications;

    // Получение по ID
    public async Task<Domain.Entities.ProblematicApplication> GetById(int id) => await _dbContext.ProblematicApplications.FirstOrDefaultAsync(s => s.Id == id);
    
    // Создание записи
    public async Task<Domain.Entities.ProblematicApplication> Create(Domain.Entities.ProblematicApplication entity)
    {
        _dbContext.ProblematicApplications.Add(entity);
        await SaveChangesAsync();
        return entity;
    }
    
    // Изменение записи
    public async Task<Domain.Entities.ProblematicApplication> Update(Domain.Entities.ProblematicApplication entity)
    {
        _dbContext.ProblematicApplications.Update(entity);
        await SaveChangesAsync();
        return entity;
    }
    
    // Удаление записи
    public async Task<Domain.Entities.ProblematicApplication> Delete(Domain.Entities.ProblematicApplication entity)
    {
        _dbContext.ProblematicApplications.Remove(entity);
        await SaveChangesAsync();
        return entity;
    }
    
    // Метод сохранения
    public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}