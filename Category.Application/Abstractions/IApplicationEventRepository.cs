namespace Category.Application.Abstractions;

/// <summary>
///     Интерфейс для хранения локальной проекции заявок внутри Category-сервиса
/// </summary>
public interface IApplicationEventRepository
{
    /// <summary>
    ///     Добавления записи о новой заявке
    /// </summary>
    /// <param name="applicationId">Идентификатор заявки</param>
    /// <param name="categoryId">Идентификатор категории</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns></returns>
    Task AddAsync(int applicationId, int categoryId, CancellationToken cancellationToken);
    
    /// <summary>
    ///     Изменение статуса активной заявки
    /// </summary>
    /// <param name="applicationId">Идентификатор заявки</param>
    /// <param name="isActive">Флаг активности</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns></returns>
    Task UpdateStatusAsync(int applicationId, bool isActive, CancellationToken cancellationToken);
    
    /// <summary>
    ///     Проверка активной заявки у категории
    /// </summary>
    /// <param name="categoryId">Идентификатор категории</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns></returns>
    Task<bool> HasActiveApplicationAsync(int categoryId, CancellationToken cancellationToken);
}