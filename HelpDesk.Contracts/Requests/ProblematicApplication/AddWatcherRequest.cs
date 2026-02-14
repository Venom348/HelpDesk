namespace HelpDesk.Contracts.Requests.ProblematicApplication;

/// <summary>
///     Модель добавления наблюдателя к заявке
/// </summary>
public class AddWatcherRequest
{
    /// <summary>
    ///     Идентификатор заявки
    /// </summary>
    public int ProblematicApplicationId { get; set; }
    
    /// <summary>
    ///     Идентификатор пользователя
    /// </summary>
    public int UserId { get; set; }
}