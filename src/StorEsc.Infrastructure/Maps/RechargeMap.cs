using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Maps;

public class RechargeMap : BaseMap<Recharge>
{
    public override void Configure(EntityTypeBuilder<Recharge> builder)
    {
        builder.ToTable("Recharge", "ste");
        
        base.Configure(builder);
        
        builder.Property(x => x.WalletId)
            .IsRequired()
            .HasColumnType("VARCHAR(36)")
            .HasColumnName("WalletId");

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("DECIMAL(14,9)")
            .HasColumnName("Amount");
        
        builder.Property(x => x.PaymentHash)
            .IsRequired()
            .HasColumnType("VARCHAR(36)")
            .HasColumnName("PaymentHash");

        builder.HasOne(x => x.Wallet)
            .WithMany(x => x.Recharges)
            .HasForeignKey(x => x.WalletId);
    }
}