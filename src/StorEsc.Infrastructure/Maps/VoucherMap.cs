using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Maps;

public class VoucherMap : BaseMap<Voucher>
{
    public override void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.ToTable("Voucher", "ste");
        
        base.Configure(builder);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasColumnName("Code")
            .HasColumnType("VARCHAR(80)");
        
        builder.Property(x => x.IsPercentageDiscount)
            .IsRequired()
            .HasDefaultValue(0)
            .HasColumnName("IsPercentageDiscount")
            .HasColumnType("BIT");
        
        
        builder.Property(x => x.ValueDiscount)
            .HasColumnName("ValueDiscount")
            .HasColumnType("DECIMAL(14,9)");
        
        builder.Property(x => x.PercentageDiscount)
            .HasColumnName("ValueDiscount")
            .HasColumnType("DECIMAL(14,9)");
    }
}