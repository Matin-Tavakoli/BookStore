using Common.Domain.ValueObjects.Money;

namespace Shop.Domain.Seller.Entities;

public class InventoryResult
{
    public long Id { get; set; }
    public long SellerId { get; set; }
    public long ProductId { get; set; }
    public int Count { get; set; }
    public Toman Price { get; set; }
}