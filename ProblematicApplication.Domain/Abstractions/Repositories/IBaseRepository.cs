using HelpDesk.Contracts.Common;

namespace ProblematicApplication.Domain.Abstractions.Repositories;

/// <summary>
///     Базовый интерфейс для взаимодействия с сущностями в БД
/// </summary>
public interface IBaseRepository<TEntity> where TEntity : Entity
{
    /// <summary>
    ///     Получение всех записей
    /// </summary>
    /// <returns></returns>
    IQueryable<TEntity> GetAll();
    
    /// <summary>
    ///     Получение по ID
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns></returns>
    Task<TEntity> GetById(int id);
    
    /// <summary>
    ///     Создание сущности
    /// </summary>
    /// <param name="entity">Сущность</param>
    /// <returns></returns>
    Task<TEntity> Create(TEntity entity);
    
    /// <summary>
    ///     Изменение сущности
    /// </summary>
    /// <param name="entity">Сущность</param>
    /// <returns></returns>
    Task<TEntity> Update(TEntity entity);
    
    /// <summary>
    ///     Удаление сущности
    /// </summary>
    /// <param name="entity">Сущность</param>
    /// <returns></returns>
    Task<TEntity> Delete(TEntity entity);
    
    /// <summary>
    ///     Асинхронный вспомогательный метод сохранения
    /// </summary>
    /// <returns></returns>
    Task<int> SaveChangesAsync();
}