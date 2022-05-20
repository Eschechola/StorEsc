using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Maps;

public class OrderMap : BaseMap<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order", "ste");
        
        base.Configure(builder);

        builder.Property(x => x.CustomerId)
            .IsRequired()
            .HasColumnType("VARCHAR(36)")
            .HasColumnName("CustomerId");
        
        builder.Property(x => x.TotalValue)
            .IsRequired()
            .HasColumnType("DECIMAL(19,4)")
            .HasColumnName("TotalValue");
        
        builder.Property(x => x.IsPaid)
            .IsRequired()
            .HasDefaultValue(0)
            .HasColumnType("BIT")
            .HasColumnName("IsPaid");

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.CustomerId);

    }
}