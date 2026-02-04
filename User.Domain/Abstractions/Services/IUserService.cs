using HelpDesk.Contracts.Requests.User;
using HelpDesk.Contracts.Responses;
using HelpDesk.Contracts.Responses.User;

namespace User.Domain.Abstractions.Services;

/// <summary>
///     Сервис для работы с пользователями
/// </summary>
public interface IUserService
{
    /// <summary>
    ///     Получение пользователя(ей)
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="page">Страница для пагинации</param>
    /// <param name="limit">Лимит пагинации</param>
    /// <returns></returns>
    Task<List<UserDescriptionResponse>> Get(int? id, int page = 0, int limit = 20);
    
    /// <summary>
    ///     Создание пользователя
    /// </summary>
    /// <param name="request">Данные для создания пользователя</param>
    /// <returns></returns>
    Task<UserDescriptionResponse> Create(PostUserRequest request);
    
    /// <summary>
    ///     Изменение пользователя
    /// </summary>
    /// <param name="request">Данные для изменения пользователя</param>
    /// <returns></returns>
    Task<UserDescriptionResponse> Update(PatchUserRequest request);
    
    /// <summary>
    ///     Удаление пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <returns></returns>
    Task<UserResponse> Delete(int id);
}