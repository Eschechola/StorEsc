using AutoMapper;
using StorEsc.Application.Dtos;
using StorEsc.Domain.Entities;

namespace StorEsc.IoC.Dependencies.Mapper.Profiles;

public class AdministratorProfile : Profile
{
    public AdministratorProfile()
    {
        CreateMap<Administrator, AdministratorDto>()
            .ReverseMap();
    }
}