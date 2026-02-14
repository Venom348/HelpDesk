namespace HelpDesk.Contracts.Common.Enums;

/// <summary>
///     Статусы
/// </summary>
public enum Status
{
    /// <summary>
    ///     Новая
    /// </summary>
    New = 1,
    
    /// <summary>
    ///     Назначена инженеру
    /// </summary>
    AssignedEngineer = 2,
    
    /// <summary>
    ///     В работе
    /// </summary>
    InProgress = 3,
    
    /// <summary>
    ///     Ожидает информации
    /// </summary>
    AwaitingInformation = 4,
    
    /// <summary>
    ///     Решена
    /// </summary>
    Resolved = 5,
    
    /// <summary>
    ///     Закрыта
    /// </summary>
    Closed = 6,
    
    /// <summary>
    ///     Отменена
    /// </summary>
    Canceled = 7,
}