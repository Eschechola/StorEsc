using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Maps;

public class PaymentMap : BaseMap<Payment>
{
    public override void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payment", "ste");
        
        base.Configure(builder);

        builder.Property(payment => payment.Hash)
            .HasColumnType("VARCHAR(72)")
            .HasColumnName("Hash");
        
        builder.Property(payment => payment.IsPaid)
            .IsRequired()
            .HasColumnType("BIT")
            .HasDefaultValue(0)
            .HasColumnName("IsPaid");
    }
}