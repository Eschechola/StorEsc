using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Maps;

public class CustomerMap : BaseMap<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer", "ste");
        
        base.Configure(builder);
        
        builder.Property(customer => customer.WalletId)
            .IsRequired()
            .HasColumnName("WalletId")
            .HasColumnType("VARCHAR(36)");

        builder.Property(customer => customer.FirstName)
            .IsRequired()
            .HasColumnName("FirstName")
            .HasColumnType("VARCHAR(100)");
        
        builder.Property(customer => customer.LastName)
            .IsRequired()
            .HasColumnName("LastName")
            .HasColumnType("VARCHAR(100)");
        
        builder.Property(customer => customer.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("VARCHAR(200)");
        
        builder.Property(customer => customer.Password)
            .IsRequired()
            .HasColumnName("Password")
            .HasColumnType("VARCHAR(120)");

        builder.HasOne(customer => customer.Wallet)
            .WithMany(wallet => wallet.Customers)
            .HasForeignKey(customer => customer.WalletId);
    }
}