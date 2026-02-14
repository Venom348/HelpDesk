using HelpDesk.Contracts.Common.Enums;

namespace HelpDesk.Contracts.Requests.ProblematicApplication;

/// <summary>
///     Модель изменения заявки на сервере
/// </summary>
public class PatchProblematicApplicationRequest
{
    /// <summary>
    ///     Идентификатор заявки
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    ///     Название
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    ///     Описание
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    ///     Ответственный инженер
    /// </summary>
    public int? ResponsibleEngineer { get; set; }
    
    /// <summary>
    ///     Приоритет
    /// </summary>
    public Priority Priority { get; set; }
}