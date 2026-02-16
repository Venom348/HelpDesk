namespace HelpDesk.Contracts.Responses;

/// <summary>
///     Класс для предоставления информации о категории
/// </summary>
public class CategoryResponse
{
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    ///     Название категории
    /// </summary>
    public string NameCategory { get; set; }
}