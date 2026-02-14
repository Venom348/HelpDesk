using HelpDesk.Contracts.Common;
using HelpDesk.Contracts.Common.Enums;

namespace ProblematicApplication.Domain.Entities;

/// <summary>
///     Модель заявки
/// </summary>
public class ProblematicApplication : Entity
{
    
    /// <summary>
    ///     Название
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    ///     Описание
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    ///     Заявитель
    /// </summary>
    public int Applicant { get; set; }
    
    /// <summary>
    ///     Ответственный инженер
    /// </summary>
    public int? ResponsibleEngineer { get; set; } 
    
    /// <summary>
    ///     Категория
    /// </summary>
    public int CategoryId { get; set; }
    
    /// <summary>
    ///     Крайний срок решения
    /// </summary>
    public DateOnly Deadline  { get; set; }
    
    /// <summary>
    ///     Дата и время создания
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    ///     Статус
    /// </summary>
    public Status Status { get; set; }
    
    /// <summary>
    ///     Приоритет
    /// </summary>
    public Priority Priority { get; set; }
    
    /// <summary>
    ///     Список наблюдателей
    /// </summary>
    public ICollection<int> Watchers { get; set; } = new List<int>();
}