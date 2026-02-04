using AutoMapper;
using HelpDesk.Contracts.Responses;
using HelpDesk.Contracts.Responses.User;

namespace User.Domain.Mapping;

/// <summary>
///     Профиль автомаппера
/// </summary>
public class UserProfile : Profile
{
    // Создание маппинга
    public UserProfile()
    {
        CreateMap<Entities.User, UserResponse>();
        CreateMap<UserResponse, Entities.User>();
        CreateMap<Entities.User, UserDescriptionResponse>();
        CreateMap<UserDescriptionResponse, Entities.User>();
    }
}