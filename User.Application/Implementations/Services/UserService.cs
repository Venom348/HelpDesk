using AutoMapper;
using HelpDesk.Contracts.Requests.User;
using HelpDesk.Contracts.Responses;
using HelpDesk.Contracts.Responses.User;
using Microsoft.EntityFrameworkCore;
using User.Application.Exceptions;
using User.Domain.Abstractions.Repositories;
using User.Domain.Abstractions.Services;

namespace User.Application.Implementations.Services;

/// <inheritdoc cref="IUserService" />
public class UserService : IUserService
{
    private readonly IBaseRepository<Domain.Entities.User> _userRepository;
    private readonly IMapper _mapper;

    public UserService(IBaseRepository<Domain.Entities.User> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<UserDescriptionResponse>> Get(int? id, int page = 0, int limit = 20)
    {
        //  Валидация параметров
        if (page < 0 || limit < 0 || limit >= 20)
        {
            throw new UserException("Некорректные параметры пагинации");
        }
    
        // Фильтрация по ID пользователя
        if (id != null)
        {
            // Поиск пользователя по ID
            var result = await _userRepository.GetAll()
                .Where(x => x.Id == id)
                .Skip(page * limit)
                .Take(limit)
                .ToListAsync();
            
            // Если пользователь с таким ID не найден - выбрасывает исключение
            if (result.Count == 0)
            {
                throw new UserException("Пользователь с таким ID не найден. Повторите попытку.");
            }
            
            // Возвращает данные о пользователе
            return _mapper.Map<List<UserDescriptionResponse>>(result);
        }
        
        // Если ID не был передан, то возвращает список всех пользователей
        var queryResult = await _userRepository.GetAll()
            .Skip(page * limit)
            .Take(limit)
            .ToListAsync();
        
        // Если результат не найден - выбрасывает исключение
        if (queryResult.Count == 0)
        {
            throw new UserException("Результат не найден. Потворите попытку.");
        }
        
        // Возвращает список всех пользователей
        return _mapper.Map<List<UserDescriptionResponse>>(queryResult);
    }

    public async Task<UserDescriptionResponse> Create(PostUserRequest request)
    {
        // Создание пользователя с переданными данными
        var result = await _userRepository.Create(new Domain.Entities.User
        {
            LastName =  request.LastName,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            Username =  request.Username,
            Role = request.Role,
            Email = request.Email,
        });
        
        // Возвращает информацию о созданном пользователе
        return _mapper.Map<UserDescriptionResponse>(result);
    }

    public async Task<UserDescriptionResponse> Update(PatchUserRequest request)
    {
        // Проверка существования пользователя по ID, если такого нет - выбрасывает исключение
        var result = await _userRepository.GetById(request.Id);
        
        if (result is null)
        {
            throw new UserException("Пользователь с таким ID не найден. Повторите попытку.");
        }
        
        // Данные для изменения
        result.LastName = request.LastName;
        result.FirstName = request.FirstName;
        result.MiddleName = request.MiddleName;
        result.Username = request.Username;
        result.Email = request.Email;
        
        // Изменяет данные пользователя
        _userRepository.Update(result);
        
        // Возвращает изменённые данные пользователя
        return _mapper.Map<UserDescriptionResponse>(result);
    }

    public async Task<UserResponse> Delete(int id)
    {
        // Проверка существования пользователя по ID, если такого нет - выбрасывает исключение
        var result = await _userRepository.GetById(id);
        
        if (result is null)
        {
            throw new UserException("Пользователь с таким ID не найден. Повторите попытку.");
        }
        
        // Удаление пользователя
        _userRepository.Delete(result);
        
        // Возвращает ID удалённого пользователя
        return _mapper.Map<UserResponse>(result);
    }
}