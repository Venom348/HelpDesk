using AutoMapper;
using HelpDesk.Contracts.Responses;

namespace Category.Domain.Mapping;

/// <summary>
///     Профиль автомаппера для категории
/// </summary>
public class CategoryProfile : Profile
{
    //  Создание маппинга
    public CategoryProfile()
    {
        CreateMap<Entities.Category, CategoryResponse>();
        CreateMap<CategoryResponse, Entities.Category>();
    }
}