namespace HelpDesk.Contracts.Requests.Category;

/// <summary>
///     Модель удаления категории на сервере
/// </summary>
public class DeleteCategoryRequest
{
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    public int Id { get; set; }
}