using AutoMapper;
using StorEsc.Application.DTOs;
using StorEsc.Domain.Entities;

namespace StorEsc.IoC.Dependencies.Mapper.Profiles;

public class SellerProfile : Profile
{
    public SellerProfile()
    {
        CreateMap<Seller, SellerDTO>()
            .ReverseMap();
    }
}