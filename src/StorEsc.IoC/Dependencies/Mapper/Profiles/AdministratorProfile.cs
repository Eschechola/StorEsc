using AutoMapper;
using StorEsc.Application.DTOs;
using StorEsc.Domain.Entities;

namespace StorEsc.IoC.Dependencies.Mapper.Profiles;

public class AdministratorProfile : Profile
{
    public AdministratorProfile()
    {
        CreateMap<Administrator, AdministratorDTO>()
            .ReverseMap();
    }
}