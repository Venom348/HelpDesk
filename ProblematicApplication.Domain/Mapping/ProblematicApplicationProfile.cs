using AutoMapper;
using HelpDesk.Contracts.Responses;
using HelpDesk.Contracts.Responses.ProblematicApplication;

namespace ProblematicApplication.Domain.Mapping;

/// <summary>
///     Профиль автомаппера
/// </summary>
public class ProblematicApplicationProfile : Profile
{
    // Создание маппинга
    public ProblematicApplicationProfile()
    {
        CreateMap<Entities.ProblematicApplication, ProblematicApplicationResponse>();
        CreateMap<ProblematicApplicationResponse, Entities.ProblematicApplication>();
        CreateMap<Entities.ProblematicApplication, ProblematicApplicationDescriptionResponse>();
        CreateMap<ProblematicApplicationDescriptionResponse, Entities.ProblematicApplication>();
    }
}