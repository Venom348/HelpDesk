using HelpDesk.Contracts.Requests.ProblematicApplication;
using HelpDesk.Contracts.Requests.User;
using HelpDesk.Contracts.Responses;
using HelpDesk.Contracts.Responses.ProblematicApplication;

namespace ProblematicApplication.Domain.Abstractions.Services;

/// <summary>
///     Сервис для работы с заявками
/// </summary>
public interface IProblematicApplicationService
{
    /// <summary>
    ///     Получение всех заявок с заданными данными
    /// </summary>
    /// <param name="applicantId">Идентификатор заявителя</param>
    /// <param name="categoryId">Идентификатор категории</param>
    /// <param name="page">Страница для пагинации</param>
    /// <param name="limit">Лимит пагинации</param>
    /// <returns></returns>
    Task<List<ProblematicApplicationDescriptionResponse>> GetAll(int applicantId, int categoryId, int page = 0, int limit = 20);
    
    /// <summary>
    ///     Получение заявки по ID
    /// </summary>
    /// <param name="id">Идентификатор заявки</param>
    /// <returns></returns>
    Task<ProblematicApplicationDescriptionResponse> GetId(int id);
    
    /// <summary>
    ///     Создание заявки
    /// </summary>
    /// <param name="request">Данные для создания заявки</param>
    /// <returns></returns>
    Task<ProblematicApplicationDescriptionResponse> Create(PostProblematicApplicationRequest request);
    
    /// <summary>
    ///     Добавление наблюдателя к заявке
    /// </summary>
    /// <param name="request">Данные для добавления наблюдателя</param>
    /// <returns></returns>
    Task AddWatcher(AddWatcherRequest request);
    
    /// <summary>
    ///     Изменение заявки
    /// </summary>
    /// <param name="request">Данные для изменения заявки</param>
    /// <returns></returns>
    Task<ProblematicApplicationDescriptionResponse> Update(PatchProblematicApplicationRequest request);

    /// <summary>
    ///     Изменения статуса заявки
    /// </summary>
    /// <param name="request">Данные для изменения статуса</param>
    /// <returns></returns>
    Task<ProblematicApplicationDescriptionResponse> UpdateStatus(PatchProblematicApplicationStatusRequest request);
    
    /// <summary>
    ///     Удаление заявки
    /// </summary>
    /// <param name="id">Идентификатор заявки</param>
    /// <returns></returns>
    Task<ProblematicApplicationResponse> Delete(int id);
}