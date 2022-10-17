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
        
        builder.Property(administrator => administrator.FirstName)
            .IsRequired()
            .HasColumnName("FirstName")
            .HasColumnType("VARCHAR(100)");
        
        builder.Property(administrator => administrator.LastName)
            .IsRequired()
            .HasColumnName("LastName")
            .HasColumnType("VARCHAR(100)");
        
        builder.Property(administrator => administrator.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("VARCHAR(200)");
        
        builder.Property(administrator => administrator.Password)
            .IsRequired()
            .HasColumnName("Password")
            .HasColumnType("VARCHAR(120)");
    }
}