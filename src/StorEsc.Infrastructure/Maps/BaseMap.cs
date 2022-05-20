using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Maps;

public class BaseMap<T> : IEntityTypeConfiguration<T> where T : Entity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnType("VARCHAR(36)");

        builder.HasIndex(x => x.Id)
            .IsUnique();

        builder.Ignore(x => x.Errors);
        builder.Ignore(x => x.IsValid);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("DATETIME");

        builder.Property(x => x.UpdatedAt)
            .IsRequired()
            .HasColumnType("DATETIME");
    }
}