using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Maps;

public class AdministratorMap : BaseMap<Administrator>
{
    public override void Configure(EntityTypeBuilder<Administrator> builder)
    {
        builder.ToTable("Administrator", "ste");

        base.Configure(builder);
        
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
            .HasColumnType("VARCHAR(120)");
    }
}