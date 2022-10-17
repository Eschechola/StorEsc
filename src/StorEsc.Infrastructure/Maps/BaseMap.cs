using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Maps;

public class BaseMap<T> : IEntityTypeConfiguration<T> where T : Entity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(entity => entity.Id)
            .IsRequired()
            .HasColumnType("VARCHAR(36)");

        builder.HasIndex(entity => entity.Id)
            .IsUnique();

        builder.Ignore(entity => entity.Errors);
        builder.Ignore(entity => entity.IsValid);

        builder.Property(entity => entity.CreatedAt)
            .IsRequired()
            .HasColumnType("DATETIME");

        builder.Property(entity => entity.UpdatedAt)
            .IsRequired()
            .HasColumnType("DATETIME");
    }
}