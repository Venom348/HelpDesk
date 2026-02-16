namespace HelpDesk.Contracts.Requests.Category;

/// <summary>
///     Модель изменения категории на сервере
/// </summary>
public class PatchCategoryRequest : PostCategoryRequest
{
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    public int Id { get; set; }
}