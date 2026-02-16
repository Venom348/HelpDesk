namespace HelpDesk.Contracts.Requests.Category;

/// <summary>
///     Модель отправки категории на сервер
/// </summary>
public class PostCategoryRequest
{
    /// <summary>
    ///     Название категории
    /// </summary>
    public string NameCategory { get; set; }
}