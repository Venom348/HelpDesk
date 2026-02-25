using HelpDesk.Contracts.Common;

namespace Category.Domain.Entities;

/// <summary>
///     Модель заявки для RabbitMQ
/// </summary>
public class ApplicationEvent : Entity
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