using AutoMapper;
using StorEsc.Application.DTOs;
using StorEsc.Domain.Entities;

namespace StorEsc.IoC.Dependencies.Mapper.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDTO>()
            .ReverseMap();
    }
}