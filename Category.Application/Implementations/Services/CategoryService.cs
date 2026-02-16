using AutoMapper;
using Category.Application.Exceptions;
using Category.Domain.Abstractions.Repositories;
using Category.Domain.Abstractions.Services;
using HelpDesk.Contracts.Requests.Category;
using HelpDesk.Contracts.Responses;
using Microsoft.EntityFrameworkCore;

namespace Category.Application.Implementations.Services;

///  <inheritdoc cref="ICategoryService" />
public class CategoryService : ICategoryService
{
    private readonly IBaseRepository<Domain.Entities.Category> _categoryService;
    private readonly IMapper _mapper;

    public CategoryService(IBaseRepository<Domain.Entities.Category> categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    public async Task<List<CategoryResponse>> Get(int? id, int page = 0, int limit = 20)
    {
        // Валидация параметров
        if (page < 0 || limit < 0 || limit >= 20)
        {
            throw new CategoryException("Некорректные данные пагинации");
        }
        
        // Фильтрация по ID
        if (id != null)
        {
            // Поиск категории по ID
            var result = await _categoryService.GetAll()
                .Where(x => x.Id == id)
                .Skip(page * limit)
                .Take(limit)
                .ToListAsync();
            
            // Если категория с таким ID не найдена - выбрасывает исключение
            if (result.Count == 0)
            {
                throw new CategoryException("Категория с таким ID не найдена. Повторите попытку");
            }
            
            // Возвращает информацию о категории
            return _mapper.Map<List<CategoryResponse>>(result);
        }
        
        // Если ID не был передан, то возвращает список всех категорий
        var queryResult = await  _categoryService.GetAll()
            .Skip(page * limit)
            .Take(limit)
            .ToListAsync();
        
        // Если результат не найден - выбрасывает исключение
        if (queryResult.Count == 0)
        {
            throw new CategoryException("Результат не найден. Повторите попытку.");
        }
        
        // Возвращает список всех категорий
        return _mapper.Map<List<CategoryResponse>>(queryResult);
    }

    public async Task<CategoryResponse> Create(PostCategoryRequest request)
    {
        // Создание категории с переданными данными
        var result = await _categoryService.Create(new Domain.Entities.Category
        {
            NameCategory =  request.NameCategory
        });
        
        // Возвращает информацию о категории
        return _mapper.Map<CategoryResponse>(result);
    }

    public async Task<CategoryResponse> Update(PatchCategoryRequest request)
    {
        // Проверка существования категории по ID, если такой нет - выбрасывает исключение
        var result = await _categoryService.GetById(request.Id);

        if (result is null)
        {
            throw new CategoryException("Категория с таким ID не найдена. Повторите попытку");
        }
        
        // Изменение полей категории
        result.NameCategory = request.NameCategory;
        
        await _categoryService.Update(result);
        
        // Возвращает информацию об изменённой категории
        return _mapper.Map<CategoryResponse>(result);
    }

    public async Task<CategoryResponse> Delete(int id)
    {
        // Проверка существования категории по ID, если такой нет - выбрасывает исключение
        var result = await _categoryService.GetById(id);

        if (result is null)
        {
            throw new CategoryException("Категория с таким ID не найдена. Повторите попытку");
        }
        
        // Удаление категории
        await _categoryService.Delete(result);
        
        // Возвращает данные об удалённой категории
        return _mapper.Map<CategoryResponse>(result);
    }
}