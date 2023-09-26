using StorEsc.Application.Dtos;
using StorEsc.Domain.Entities;

namespace StorEsc.Application.Extensions;

public static class OrderItemItensExtension
{
    public static OrderItemDto AsDto(this OrderItem orderItem)
        => new OrderItemDto
        {
            Id = orderItem.Id,
            ItemCount = orderItem.ItemCount,
            OrderId = orderItem.OrderId,
            ProductId = orderItem.ProductId,
            CreatedAt = orderItem.CreatedAt,
            UpdatedAt = orderItem.UpdatedAt,
        };
    
    public static OrderItem AsEntity(this OrderItemDto orderItemDto)
        => new OrderItem(
            id: orderItemDto.Id,
            itemCount: orderItemDto.ItemCount,
            createdAt: orderItemDto.CreatedAt,
            updatedAt: orderItemDto.UpdatedAt,
            product: null,
            order: null);

    public static IList<OrderItem> AsEntityList(this IList<OrderItemDto> orderItemDtos)
        => orderItemDtos.Select(orderItem => orderItem.AsEntity()).ToList();
    
    public static IList<OrderItemDto> AsDtoList(this IList<OrderItem> orderItemDtos)
        => orderItemDtos.Select(orderItem => orderItem.AsDto()).ToList();
}