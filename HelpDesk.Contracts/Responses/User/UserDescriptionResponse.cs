using HelpDesk.Contracts.Common.Enums;

namespace HelpDesk.Contracts.Responses.User;

/// <summary>
///     Класс для предоставления полной информации о пользователе
/// </summary>
public class UserDescriptionResponse : UserResponse
{
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
    ///     Роль
    /// </summary>
    public Role Role { get; set; }
    
    /// <summary>
    ///     Почта
    /// </summary>
    public string Email { get; set; }
}