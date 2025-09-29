using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shop.Infrastructure.Persistent.EF.WebSieEntities.Slider;

internal class SliderConfiguration : IEntityTypeConfiguration<Domain.WebSiteEntities.Entities.Slider>
{
    public void Configure(EntityTypeBuilder<Domain.WebSiteEntities.Entities.Slider> builder)
    {
        builder.Property(b => b.ImageName)
            .HasMaxLength(120).IsRequired();

        builder.Property(b => b.Title)
            .HasMaxLength(120);

        builder.Property(b => b.Link)
            .HasMaxLength(500);
    }
}