using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Maps;

public class OrderItemMap : BaseMap<OrderItem>
{
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItem", "ste");
        
        base.Configure(builder);

        builder.Property(orderItem => orderItem.ItemCount)
            .IsRequired()
            .HasColumnName("Count")
            .HasColumnType("INT");
        
        builder.Property(orderItem => orderItem.ProductId)
            .IsRequired()
            .HasColumnName("ProductId")
            .HasColumnType("VARCHAR(36)");
        
        builder.Property(orderItem=> orderItem.OrderId)
            .IsRequired()
            .HasColumnName("OrderId")
            .HasColumnType("VARCHAR(36)");

        builder.HasOne(orderItem => orderItem.Order)
            .WithMany(order => order.OrderItens)
            .HasForeignKey(orderItem => orderItem.OrderId);
        
        builder.HasOne(orderItem => orderItem.Product)
            .WithMany(product => product.OrderItens)
            .HasForeignKey(orderItem => orderItem.ProductId);
    }
}