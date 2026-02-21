using AutoMapper;
using HelpDesk.Contracts.Common.Enums;
using HelpDesk.Contracts.Requests.ProblematicApplication;
using HelpDesk.Contracts.Responses;
using HelpDesk.Contracts.Responses.ProblematicApplication;
using Microsoft.EntityFrameworkCore;
using ProblematicApplication.Application.Exceptions;
using ProblematicApplication.Domain.Abstractions.Repositories;
using ProblematicApplication.Domain.Abstractions.Services;
using ProblematicApplication.Infrastructure.Messaging.Producers;

namespace ProblematicApplication.Application.Implementations.Services;

/// <inheritdoc cref="IProblematicApplicationService" />
public class ProblematicApplicationService : IProblematicApplicationService
{
    private readonly IBaseRepository<Domain.Entities.ProblematicApplication> _problematicApplicationRepository;
    private readonly IMapper _mapper;
    private readonly EventPublisher _eventPublisher;

    public ProblematicApplicationService(IBaseRepository<Domain.Entities.ProblematicApplication> problematicApplicationRepository, IMapper mapper, EventPublisher eventPublisher)
    {
        _problematicApplicationRepository = problematicApplicationRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    public async Task<List<ProblematicApplicationDescriptionResponse>> GetAll(int applicantId, int categoryId, int page = 0, int limit = 20)
    {
        // Валидация пагинации
        if (page < 0 || limit < 0 || limit > 20)
        {
            throw new ProblematicApplicationException("Некорректные данные пагинации");
        }
    
        // Получение всех записей
        var query = _problematicApplicationRepository.GetAll();
        
        // Фильтрация по ID заявителя
        query = query
            .Where(u => u.Applicant == applicantId);
        
        // Фильтрация по ID категории
        query = query
            .Where(u => u.CategoryId == categoryId);
        
        // Применение пагинации
        query = query
            .Skip(limit * page)
            .Take(limit);
        
        var result = query.ToListAsync();
        
        // Возвращает список всех заявок
        return _mapper.Map<List<ProblematicApplicationDescriptionResponse>>(result);
    }

    public async Task<ProblematicApplicationDescriptionResponse> GetId(int id)
    {
        // Поиск заявки по ID, если такой нет - выбрасывает исключение
        var result = await _problematicApplicationRepository.GetById(id);

        if (result is null)
        {
            throw new ProblematicApplicationException("Заявка с таким ID не найдена. Повторите попытку.");
        }
        
        // Возвращает заявку
        return _mapper.Map<ProblematicApplicationDescriptionResponse>(result);
    }

    public async Task<ProblematicApplicationDescriptionResponse> Create(PostProblematicApplicationRequest request)
    {
        // Создание заявки
        var result = await _problematicApplicationRepository.Create(new Domain.Entities.ProblematicApplication
        {
            Title = request.Title,
            Description = request.Description,
            Applicant = request.Applicant,
            ResponsibleEngineer =  request.ResponsibleEngineer,
            CategoryId = request.CategoryId,
            Deadline =  request.Deadline,
            CreatedAt = DateTime.UtcNow,
            Status = request.Status,
            Priority = request.Priority
        });
        
        // Публикация события о создании заявки в RabbitMQ
        await _eventPublisher.PublishApplicationCreatedAsync(
            applicationId: result.Id,
            categoryId: result.CategoryId,
            cancellationToken: CancellationToken.None);
        
        // Возвращает информацию о созданной заявке
        return _mapper.Map<ProblematicApplicationDescriptionResponse>(result);
    }

    public async Task AddWatcher(AddWatcherRequest request)
    {
        // Поиск заявки по ID, если такой нет - выбрасывает исключение
        var result = await _problematicApplicationRepository.GetById(request.ProblematicApplicationId);

        if (result is null)
        {
            throw new ProblematicApplicationException("Заявка с таким ID не найдена. Повторите попытку.");
        }
        
        // Проверка, является ли пользователь наблюдателем
        if (result.Watchers.Contains(request.UserId))
        {
            throw new ProblematicApplicationException("Пользователь уже является наблюдателем этой заявки.");
        }
        
        // Добавляет пользователя в список наблюдателей
        result.Watchers.Add(request.UserId);
        
        // Обновление списка наблюдателей заявки
        await _problematicApplicationRepository.Update(result);
        await _problematicApplicationRepository.SaveChangesAsync();
    }

    public async Task<ProblematicApplicationDescriptionResponse> Update(PatchProblematicApplicationRequest request)
    {
        // Поиск заявки по ID, если такой нет - выбрасывает исключение
        var result = await _problematicApplicationRepository.GetById(request.Id);

        if (result is null)
        {
            throw new ProblematicApplicationException("Заявка с таким ID не найдена. Повторите попытку.");
        }
        
        // Изменение полей заявки
        result.Title = request.Title;
        result.Description = request.Description;
        result.ResponsibleEngineer = request.ResponsibleEngineer;
        result.Priority = request.Priority;
        
        // Обновление заявки
        await _problematicApplicationRepository.Update(result);
        
        // Возвращает заявку с изменёнными данными
        return _mapper.Map<ProblematicApplicationDescriptionResponse>(result);
    }

    public async Task<ProblematicApplicationDescriptionResponse> UpdateStatus(PatchProblematicApplicationStatusRequest request)
    {
        // Поиск заявки по ID, если такой нет - выбрасывает исключение
        var result = await _problematicApplicationRepository.GetById(request.Id);

        if (result is null)
        {
            throw new ProblematicApplicationException("Заявка с таким ID не найдена. Повторите попытку.");
        }
        
        // Проверка допустимости изменения статуса
        if (!IsValidStatusTransition(result.Status, request.Status))
        {
            throw new ProblematicApplicationException(
                $"Невозможно изменить статус с «{result.Status}» на «{request.Status}»");
        }
        
        // Изменение статуса
        result.Status = request.Status;
        
        // Обновление заявки
        await _problematicApplicationRepository.Update(result);
        await _problematicApplicationRepository.SaveChangesAsync();
        
        // Публикация события об изменении статуса в RabbitMQ
        bool isActive = request.Status != Status.Closed && request.Status != Status.Canceled;
        
        await _eventPublisher.PublishApplicationStatusChangedAsync(
            applicationId: result.Id,
            categoryId: result.CategoryId,
            isActive: isActive,
            cancellationToken: CancellationToken.None);
        
        // Возвращает изменённые данные заявки
        return _mapper.Map<ProblematicApplicationDescriptionResponse>(result);
    }

    public async Task<ProblematicApplicationResponse> Delete(int id)
    {
        // Поиск заявки по ID, если такой нет - выбрасывает исключение
        var result = await _problematicApplicationRepository.GetById(id);

        if (result is null)
        {
            throw new ProblematicApplicationException("Заявка с таким ID не найдена. Повторите попытку.");
        }
        
        // Если статус заявки New, Closed или Canceled, то удаляет задачу, иначе выбрасывает исключение
        if (result.Status switch { 
                Status.New or Status.Closed or Status.Canceled => true,
                _ => false 
        } == false)
            throw new ProblematicApplicationException($"Нельзя удалить в статусе {result.Status}");
        
        // Удаление заявки
        await _problematicApplicationRepository.Delete(result);
        
        // Возвращает ID удалённой заявки
        return _mapper.Map<ProblematicApplicationResponse>(result);
    }
    
    // Метод валидации изменения статуса заявки
    private bool IsValidStatusTransition(Status currentStatus, Status newStatus)
    {
        if (currentStatus == newStatus)
        {
            return true;
        }

        return currentStatus switch
        {
            // Если заявка New, то новый статус либо AssignedEngineer, либо Canceled
            Status.New => newStatus == Status.AssignedEngineer || newStatus == Status.Canceled,
            
            // Если заявка AssignedEngineer, то новый статус либо InProgress, либо Canceled
            Status.AssignedEngineer => newStatus == Status.InProgress || newStatus == Status.Canceled,
            
            // ЕСли заявка InProgress, то новый статус либо AwaitingInformation, либо Canceled
            Status.InProgress => newStatus == Status.AwaitingInformation || newStatus == Status.Canceled,
            
            // Если заявка AwaitingInformation, то новый статус либо Resolved, либо InProgress, либо Canceled
            Status.AwaitingInformation => newStatus == Status.Resolved ||
                                          newStatus == Status.InProgress ||
                                          newStatus == Status.Canceled,
            
            // Если статус Resolved, то новый статус Closed, либо Canceled
            Status.Resolved => newStatus == Status.Closed || newStatus == Status.Canceled,
            
            // Дефолтный кейс для остальных случаев
            _ => false
        };
    }
}