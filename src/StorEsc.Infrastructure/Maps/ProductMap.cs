using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Maps;

public class ProductMap : BaseMap<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product", "ste");
        
        base.Configure(builder);
        
        builder.Property(x => x.SellerId)
            .IsRequired()
            .HasColumnType("VARCHAR(36)")
            .HasColumnName("SellerId");
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("VARCHAR(200)")
            .HasColumnName("Name");
        
        builder.Property(x => x.Description)
            .IsRequired()
            .HasColumnType("VARCHAR(2000)")
            .HasColumnName("Description");
        
        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("DECIMAL(14,9)")
            .HasColumnName("Price");
        
        builder.Property(x => x.Stock)
            .IsRequired()
            .HasColumnType("INT")
            .HasColumnName("Stock");
        
        builder.HasOne(x => x.Seller)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.SellerId);
    }
}