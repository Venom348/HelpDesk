using HelpDesk.Contracts.Common.Enums;

namespace HelpDesk.Contracts.Requests.ProblematicApplication;

/// <summary>
///     Модель изменения статуса заявки на сервере
/// </summary>
public class PatchProblematicApplicationStatusRequest
{
    /// <summary>
    ///     Идентификатор заявки
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    ///     Статус
    /// </summary>
    public Status Status { get; set; }
}