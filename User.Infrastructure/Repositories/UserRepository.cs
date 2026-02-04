using Microsoft.EntityFrameworkCore;
using User.Domain.Abstractions.Repositories;

namespace User.Infrastructure.Repositories;

/// <inheritdoc cref="IBaseRepository{User}" />
public class UserRepository : IBaseRepository<Domain.Entities.User>
{
    private readonly ApplicationContext _dbContext;

    public UserRepository(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // Получение всех записей
    public IQueryable<Domain.Entities.User> GetAll() => _dbContext.Users;
    
    // Получение по ID
    public async Task<Domain.Entities.User> GetById(int id) => await _dbContext.Users.FirstOrDefaultAsync(s => s.Id == id);
    
    //  Создание записи
    public async Task<Domain.Entities.User> Create(Domain.Entities.User entity)
    {
        _dbContext.Users.Add(entity);
        await SaveChangesAsync();
        return entity;
    }
    
    // Изменение записи
    public async Task<Domain.Entities.User> Update(Domain.Entities.User entity)
    {
        _dbContext.Users.Update(entity);
        await SaveChangesAsync();
        return entity;
    }
    
    // Удаление записи
    public async Task<Domain.Entities.User> Delete(Domain.Entities.User entity)
    {
        _dbContext.Users.Remove(entity);
        await SaveChangesAsync();
        return entity;
    }
    
    // Метод сохранения
    public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}