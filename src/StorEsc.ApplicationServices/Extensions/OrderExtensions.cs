using StorEsc.Application.Dtos;
using StorEsc.Domain.Entities;

namespace StorEsc.Application.Extensions;

public static class OrderExtensions
{
    public static OrderDto AsDto(this Order order)
        => new OrderDto
        {
            Id = order.Id,
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt,
            IsPaid = order.IsPaid,
            TotalValue = order.TotalValue,
            VoucherId = order.VoucherId,
            CustomerId = order.CustomerId,
            OrderItens = order.OrderItens.AsDtoList(),
            Customer = order?.Customer?.AsDto(),
            Voucher = order?.Voucher?.AsDto(),
        };
    
    public static Order AsEntity(this OrderDto orderDto)
        => new Order(
            id: orderDto.Id,
            customerId: orderDto.CustomerId,
            isPaid: orderDto.IsPaid,
            createdAt: orderDto.CreatedAt,
            updatedAt: orderDto.UpdatedAt,
            customer: orderDto.Customer.AsEntity(),
            voucher: orderDto?.Voucher?.AsEntity(),
            orderItens: orderDto?.OrderItens?.AsEntityList()
        );

    public static IList<Order> AsEntityList(this IList<OrderDto> orderDtos)
        => orderDtos.Select(order => order.AsEntity()).ToList();
    
    public static IList<OrderDto> AsDtoList(this IList<Order> orderDtos)
        => orderDtos.Select(order => order.AsDto()).ToList();
}