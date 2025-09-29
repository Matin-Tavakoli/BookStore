using Common.Application;
using Common.Domain.ValueObjects.Money;

namespace Shop.Application.Sellers.AddInventory;

public class AddSellerInventoryCommand : IBaseCommand
{
    public AddSellerInventoryCommand(long sellerId, long productId, int count,
        Toman price, int? percentageDiscount)
    {
        SellerId = sellerId;
        ProductId = productId;
        Count = count;
        Price = price;
        PercentageDiscount = percentageDiscount;
    }
    public long SellerId { get; private set; }
    public long ProductId { get; private set; }
    public int Count { get; private set; }
    public Toman Price { get; private set; }
    public int? PercentageDiscount { get; private set; }
}