using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Maps;

public class SellerMap : BaseMap<Seller>
{
    public override void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.ToTable("Seller", "ste");
        
        base.Configure(builder);
        
        builder.Property(seller => seller.WalletId)
            .IsRequired()
            .HasColumnName("WalletId")
            .HasColumnType("VARCHAR(36)");

        builder.Property(seller => seller.FirstName)
            .IsRequired()
            .HasColumnName("FirstName")
            .HasColumnType("VARCHAR(100)");
        
        builder.Property(seller => seller.LastName)
            .IsRequired()
            .HasColumnName("LastName")
            .HasColumnType("VARCHAR(100)");
        
        builder.Property(seller => seller.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("VARCHAR(200)");
        
        builder.Property(seller => seller.Password)
            .IsRequired()
            .HasColumnName("Password")
            .HasColumnType("VARCHAR(120)");
        
        builder.HasOne(seller => seller.Wallet)
            .WithMany(wallet => wallet.Sellers)
            .HasForeignKey(seller => seller.WalletId);
    }
}