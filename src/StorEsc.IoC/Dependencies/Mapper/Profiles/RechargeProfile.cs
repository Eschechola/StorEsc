using AutoMapper;
using StorEsc.Application.Dtos;
using StorEsc.Domain.Entities;

namespace StorEsc.IoC.Dependencies.Mapper.Profiles;

public class RechargeProfile : Profile
{
    public RechargeProfile()
    {
        CreateMap<Recharge, RechargeDto>()
            .ReverseMap();
    }
}