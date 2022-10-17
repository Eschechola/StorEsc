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

        builder.Property(order => order.CustomerId)
            .IsRequired()
            .HasColumnType("VARCHAR(36)")
            .HasColumnName("CustomerId");
        
        builder.Property(order => order.TotalValue)
            .IsRequired()
            .HasColumnType("DECIMAL(19,4)")
            .HasColumnName("TotalValue");
        
        builder.Property(order => order.IsPaid)
            .IsRequired()
            .HasDefaultValue(0)
            .HasColumnType("BIT")
            .HasColumnName("IsPaid");

        builder.HasOne(order => order.Customer)
            .WithMany(customer => customer.Orders)
            .HasForeignKey(order => order.CustomerId);

        builder.HasOne(order => order.Voucher)
            .WithMany(voucher => voucher.Orders)
            .HasForeignKey(order => order.VoucherId);
    }
}