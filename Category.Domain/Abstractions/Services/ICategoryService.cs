using HelpDesk.Contracts.Requests.Category;
using HelpDesk.Contracts.Responses;

namespace Category.Domain.Abstractions.Services;

/// <summary>
///     Сервис для работы с категориями
/// </summary>
public interface ICategoryService
{
    /// <summary>
    ///     Получение кактегории(ий)
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    /// <param name="page">Страница для пагинации</param>
    /// <param name="limit">Лимит пагинации</param>
    /// <returns></returns>
    Task<List<CategoryResponse>> Get(int? id, int page = 0, int limit = 20);
    
    /// <summary>
    ///     Создание категории
    /// </summary>
    /// <param name="request">Данные для создания категории</param>
    /// <returns></returns>
    Task<CategoryResponse> Create(PostCategoryRequest request);
    
    /// <summary>
    ///     Изменение категории
    /// </summary>
    /// <param name="request">Данные для изменения категории</param>
    /// <returns></returns>
    Task<CategoryResponse> Update(PatchCategoryRequest request);
    
    /// <summary>
    ///     Удаление категории
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    /// <returns></returns>
    Task<CategoryResponse> Delete(int id);
}