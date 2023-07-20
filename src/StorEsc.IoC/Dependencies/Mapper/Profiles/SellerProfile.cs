using AutoMapper;
using StorEsc.Application.Dtos;
using StorEsc.Domain.Entities;

namespace StorEsc.IoC.Dependencies.Mapper.Profiles;

public class SellerProfile : Profile
{
    public SellerProfile()
    {
        CreateMap<Seller, SellerDto>()
            .ReverseMap();
    }
}