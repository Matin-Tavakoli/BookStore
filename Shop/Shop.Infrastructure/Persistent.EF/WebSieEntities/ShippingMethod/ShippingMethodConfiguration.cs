using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shop.Infrastructure.Persistent.EF.WebSieEntities.ShippingMethod;

internal class ShippingMethodConfiguration : IEntityTypeConfiguration<Domain.WebSiteEntities.Entities.ShippingMethod>
{
    public void Configure(EntityTypeBuilder<Domain.WebSiteEntities.Entities.ShippingMethod> builder)
    {
        builder.Property(b => b.Title)
            .HasMaxLength(120).IsRequired();
    }
}