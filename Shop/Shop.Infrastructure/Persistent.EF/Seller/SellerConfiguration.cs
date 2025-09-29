using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shop.Infrastructure.Persistent.EF.Seller
{
    internal class SellerConfiguration : IEntityTypeConfiguration<Domain.Seller.Entities.Seller>
    {
        public void Configure(EntityTypeBuilder<Domain.Seller.Entities.Seller> builder)
        {
            builder.ToTable("Sellers", "seller");
            builder.HasIndex(b => b.NationalCode).IsUnique();

            builder.Property(b => b.NationalCode)
                .IsRequired();

            builder.Property(b => b.ShopName)
                .IsRequired();

            builder.OwnsMany(b => b.Inventories, option =>
            {
                option.ToTable("Inventories", "seller");
                option.HasKey(b => b.Id);
                option.HasIndex(b => b.ProductId);
                option.HasIndex(b => b.SellerId);

            });
        }
    }
}