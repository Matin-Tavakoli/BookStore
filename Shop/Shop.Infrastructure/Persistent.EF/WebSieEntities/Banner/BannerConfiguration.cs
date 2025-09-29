using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shop.Infrastructure.Persistent.EF.WebSieEntities.Banner;
internal class BannerConfiguration : IEntityTypeConfiguration<Domain.WebSiteEntities.Entities.Banner>
{
    public void Configure(EntityTypeBuilder<Domain.WebSiteEntities.Entities.Banner> builder)
    {
        builder.Property(b => b.ImageName)
            .HasMaxLength(120).IsRequired();

        builder.Property(b => b.Link)
            .HasMaxLength(500).IsRequired();
    }
}