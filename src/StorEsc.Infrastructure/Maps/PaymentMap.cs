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

        builder.Property(x => x.Hash)
            .HasColumnType("VARCHAR(72)")
            .HasColumnName("Hash");
        
        builder.Property(x => x.IsPaid)
            .IsRequired()
            .HasColumnType("BIT")
            .HasDefaultValue(0)
            .HasColumnName("IsPaid");
    }
}