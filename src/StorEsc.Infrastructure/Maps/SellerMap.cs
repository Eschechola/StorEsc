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
        
        builder.Property(x=>x.WalletId)
            .IsRequired()
            .HasColumnName("WalletId")
            .HasColumnType("VARCHAR(36)");

        builder.Property(x=>x.FirstName)
            .IsRequired()
            .HasColumnName("FirstName")
            .HasColumnType("VARCHAR(100)");
        
        builder.Property(x=>x.LastName)
            .IsRequired()
            .HasColumnName("LastName")
            .HasColumnType("VARCHAR(100)");
        
        builder.Property(x=>x.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("VARCHAR(200)");
        
        builder.Property(x=>x.Password)
            .IsRequired()
            .HasColumnName("Password")
            .HasColumnType("VARCHAR(80)");
        
        builder.HasOne(x => x.Wallet)
            .WithMany(x => x.Sellers)
            .HasForeignKey(x => x.WalletId);
    }
}