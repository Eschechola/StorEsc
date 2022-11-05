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
        
        builder.Property(recharge => recharge.WalletId)
            .IsRequired()
            .HasColumnType("VARCHAR(36)")
            .HasColumnName("WalletId");

        builder.Property(recharge => recharge.Amount)
            .IsRequired()
            .HasColumnType("DECIMAL(19,4)")
            .HasColumnName("Amount");

        builder.HasOne(recharge => recharge.Wallet)
            .WithMany(wallet => wallet.Recharges)
            .HasForeignKey(recharge => recharge.WalletId);

        builder.HasOne(recharge => recharge.Payment)
            .WithMany(wallet => wallet.Recharges)
            .HasForeignKey(recharge => recharge.PaymentId);
    }
}