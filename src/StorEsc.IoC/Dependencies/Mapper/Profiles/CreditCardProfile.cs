using AutoMapper;
using StorEsc.Application.DTOs;
using StorEsc.Domain.Entities;

namespace StorEsc.IoC.Dependencies.Mapper.Profiles;

public class CreditCardProfile : Profile
{
    public CreditCardProfile()
    {
        CreateMap<CreditCard, CreditCardDTO>()
            .ReverseMap();
    }
}