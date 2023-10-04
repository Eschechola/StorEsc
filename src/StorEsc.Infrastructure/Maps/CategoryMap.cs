using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Maps;

public class CategoryMap : BaseMap<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category", "ste");

        base.Configure(builder);
        
        builder.Property(category => category.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("VARCHAR(120)");

        builder.HasMany(category => category.Products)
            .WithOne(product => product.Category)
            .HasForeignKey(product => product.CategoryId);
    }
}