using Category.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Category.Infrastructure.Repositories;

/// <inheritdoc cref="IBaseRepository{Category}" />
public class CategoryRepository : IBaseRepository<Domain.Entities.Category>
{
    private readonly ApplicationContext _dbContext;

    public CategoryRepository(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Получение всех записей
    public IQueryable<Domain.Entities.Category> GetAll() => _dbContext.Categories;

    // Получение по ID
    public async Task<Domain.Entities.Category> GetById(int id) => await _dbContext.Categories.FirstOrDefaultAsync(s => s.Id == id);

    //  Создание записи
    public async Task<Domain.Entities.Category> Create(Domain.Entities.Category entity)
    {
        _dbContext.Categories.Add(entity);
        await SaveChangesAsync();
        return entity;
    }
    
    // Изменение записи
    public async Task<Domain.Entities.Category> Update(Domain.Entities.Category entity)
    {
        _dbContext.Categories.Update(entity);
        await SaveChangesAsync();
        return entity;
    }
    
    // Удаление записи
    public async Task<Domain.Entities.Category> Delete(Domain.Entities.Category entity)
    {
        _dbContext.Categories.Remove(entity);
        await SaveChangesAsync();
        return entity;
    }

    // Метод сохранения
    public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}