namespace HelpDesk.Contracts.Messages.ProblematicApplication;

/// <summary>
///     Сообщение о создании новой заявки
/// </summary>
public class ApplicationCreatedMessage
{
    /// <summary>
    ///     Идентификатор заявки
    /// </summary>
    public int ApplicationId { get; set; }
    
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    public int CategoryId { get; set; }
}