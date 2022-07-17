using AutoMapper;
using StorEsc.Application.DTOs;
using StorEsc.Domain.Entities;

namespace StorEsc.IoC.Dependencies.Mapper.Profiles;

public class RechargeProfile : Profile
{
    public RechargeProfile()
    {
        CreateMap<Recharge, RechargeDTO>()
            .ReverseMap();
    }
}