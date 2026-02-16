using HelpDesk.Contracts.Common;

namespace Category.Domain.Entities;

/// <summary>
///     Модель категории
/// </summary>
public class Category : Entity
{
    /// <summary>
    ///     Название категории
    /// </summary>
    public string NameCategory { get; set; }
}