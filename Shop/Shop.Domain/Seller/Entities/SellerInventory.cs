using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects.Money;

namespace Shop.Domain.Seller.Entities;

public class SellerInventory : BaseEntity
{
    public SellerInventory(long productId, int count, Toman price, int? discountPercentage = null)
    {
        if ( count < 0)
            throw new InvalidDomainDataException();

        ProductId = productId;
        Count = count;
        Price = price;
        DiscountPercentage = discountPercentage;
    }

    public long SellerId { get; internal set; }
    public long ProductId { get; private set; }
    public int Count { get; private set; }
    public Toman Price { get; private set; }
    public int? DiscountPercentage { get; private set; }


    public void Edit(int count, Toman price, int? discountPercentage = null)
    {
        if ( count < 0)
            throw new InvalidDomainDataException();

        Count = count;
        Price = price;
        DiscountPercentage = discountPercentage;
    }
}