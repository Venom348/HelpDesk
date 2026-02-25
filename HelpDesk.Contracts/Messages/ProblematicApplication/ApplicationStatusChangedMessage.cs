namespace HelpDesk.Contracts.Messages.ProblematicApplication;

/// <summary>
///     Сообщение об изменении статуса заявки
/// </summary>
public class ApplicationStatusChangedMessage
{
    /// <summary>
    ///     Идентификатор заявки
    /// </summary>
    public int ApplicationId { get; set; }
    
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    public int CategoryId { get; set; }
    
    /// <summary>
    ///     Флаг активности заявки
    /// </summary>
    public bool IsActive { get; set; }
}