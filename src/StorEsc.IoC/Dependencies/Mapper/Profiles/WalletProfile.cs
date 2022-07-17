using AutoMapper;
using StorEsc.Application.DTOs;
using StorEsc.Domain.Entities;

namespace StorEsc.IoC.Dependencies.Mapper.Profiles;

public class WalletProfile : Profile
{
    public WalletProfile()
    {
        CreateMap<Wallet, WalletDTO>()
            .ReverseMap();
    }
}