using AutoMapper;
using StorEsc.Application.DTOs;
using StorEsc.Domain.Entities;

namespace StorEsc.IoC.Dependencies.Mapper.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDTO>()
            .ReverseMap();

        CreateMap<OrderItem, OrderItemDTO>()
            .ReverseMap();
    }
}