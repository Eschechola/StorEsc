﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Maps;

public class WalletMap : BaseMap<Wallet>
{
    public override void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("Wallet", "ste");
        
        base.Configure(builder);

        builder.Property(wallet => wallet.Amount)
            .IsRequired()
            .HasDefaultValue(0)
            .HasColumnName("Amount")
            .HasColumnType("DECIMAL(19,4)");
    }
}