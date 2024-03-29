﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Maps;

public class ProductMap : BaseMap<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product", "ste");
        
        base.Configure(builder);
        
        builder.Property(product => product.CategoryId)
            .IsRequired()
            .HasColumnType("VARCHAR(36)");
        
        builder.Property(product => product.Name)
            .IsRequired()
            .HasColumnType("VARCHAR(200)")
            .HasColumnName("Name");
        
        builder.Property(product => product.Description)
            .IsRequired()
            .HasColumnType("VARCHAR(2000)")
            .HasColumnName("Description");
        
        builder.Property(product => product.Price)
            .IsRequired()
            .HasColumnType("DECIMAL(19,4)")
            .HasColumnName("Price");
        
        builder.Property(product => product.Stock)
            .IsRequired()
            .HasColumnType("INT")
            .HasColumnName("Stock");

        builder.Property(product => product.Enabled)
            .IsRequired()
            .HasDefaultValue(0)
            .HasColumnType("BIT")
            .HasColumnName("Enabled");
    }
}