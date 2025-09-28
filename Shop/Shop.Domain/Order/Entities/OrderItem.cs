using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects.Money;

namespace Shop.Domain.Order.Entities;

public class OrderItem : BaseEntity
{
    public OrderItem(long inventoryId, int count, Toman price)
    {
        CountGuard(count);

        InventoryId = inventoryId;
        Count = count;
        Price = price;
    }

    public long OrderId { get; internal set; }
    public long InventoryId { get; private set; }
    public int Count { get; private set; }
    public Toman Price { get; private set; }
    public Toman TotalPrice => Price * Count;

    public void IncreaseCount(int count)
    {
        Count += count;
    }

    public void DecreaseCount(int count)
    {
        if (Count == 1)
            return;
        if (Count - count <= 0)
            return;

        Count -= count;
    }

    public void ChangeCount(int newCount)
    {
        CountGuard(newCount);

        Count = newCount;
    }

    public void SetPrice(Toman newPrice)
    {
        Price = newPrice;
    }


    public void CountGuard(int count)
    {
        if (count < 1)
            throw new InvalidDomainDataException();
    }
}