using HelpDesk.Contracts.Common.Enums;

namespace HelpDesk.Contracts.Requests.User;

/// <summary>
///     Модель для изменения пользователя на сервере
/// </summary>
public class PatchUserRequest
{
    /// <summary>
    ///     Идентификатор пользователя
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    ///     Фамилия
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    ///     Имя
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    ///     Отчество
    /// </summary>
    public string MiddleName { get; set; }
    
    /// <summary>
    ///     Имя пользователя(никнейм)
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    ///     Почта
    /// </summary>
    public string Email { get; set; }
}