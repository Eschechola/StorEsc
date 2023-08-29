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
        
        builder.Property(voucher => voucher.Code)
            .IsRequired()
            .HasColumnName("Code")
            .HasColumnType("VARCHAR(80)");
        
        builder.Property(voucher => voucher.Enabled)
            .IsRequired()
            .HasDefaultValue(0)
            .HasColumnName("Enabled")
            .HasColumnType("BIT");
        
        builder.Property(voucher => voucher.IsPercentageDiscount)
            .IsRequired()
            .HasDefaultValue(0)
            .HasColumnName("IsPercentageDiscount")
            .HasColumnType("BIT");
        
        builder.Property(voucher => voucher.ValueDiscount)
            .HasColumnName("ValueDiscount")
            .HasColumnType("DECIMAL(14,9)");
        
        builder.Property(voucher => voucher.PercentageDiscount)
            .HasColumnName("PercentageDiscount")
            .HasColumnType("DECIMAL(14,9)");
    }
}