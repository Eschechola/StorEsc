using AutoMapper;
using StorEsc.Application.DTOs;
using StorEsc.Domain.Entities;

namespace StorEsc.IoC.Dependencies.Mapper.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerDTO>()
            .ReverseMap();
    }
}