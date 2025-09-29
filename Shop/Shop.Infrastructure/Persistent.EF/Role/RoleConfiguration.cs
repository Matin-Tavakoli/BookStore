using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shop.Infrastructure.Persistent.EF.Role;

internal class RoleConfiguration : IEntityTypeConfiguration<Domain.Role.Entities.Role>
{
    public void Configure(EntityTypeBuilder<Domain.Role.Entities.Role> builder)
    {
        builder.ToTable("Roles", "role");
        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(60);

        builder.OwnsMany(b => b.Permissions, option =>
        {
            option.ToTable("Permissions", "role");
        });
    }
}