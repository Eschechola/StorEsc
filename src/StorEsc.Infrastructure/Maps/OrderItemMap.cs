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

        builder.Property(x => x.ItemCount)
            .IsRequired()
            .HasColumnName("Count")
            .HasColumnType("INT");
        
        builder.Property(x=>x.ProductId)
            .IsRequired()
            .HasColumnName("ProductId")
            .HasColumnType("VARCHAR(36)");
        
        builder.Property(x=>x.OrderId)
            .IsRequired()
            .HasColumnName("OrderId")
            .HasColumnType("VARCHAR(36)");

        builder.HasOne(x => x.Order)
            .WithMany(x => x.OrderItens)
            .HasForeignKey(x => x.OrderId);
        
        builder.HasOne(x => x.Product)
            .WithMany(x => x.OrderItens)
            .HasForeignKey(x => x.ProductId);

    }
}