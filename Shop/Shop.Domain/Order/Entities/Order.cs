using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects.Money;
using Shop.Domain.Order.Enums;
using Shop.Domain.Order.ValueObjects;

namespace Shop.Domain.Order.Entities;

public class Order : AggregateRoot
{
    private Order()
    {
    }

    public Order(long userId)
    {
        UserId = userId;
        Status = OrderStatus.Pending;
        Items = new List<OrderItem>();
    }

    public long UserId { get; private set; }
    public OrderStatus Status { get; private set; }
    public OrderDiscount? Discount { get; private set; }
    public OrderAddress? Address { get; private set; }
    public OrderShippingMethod? ShippingMethod { get; private set; }
    public List<OrderItem> Items { get; private set; }
    public DateTime? LastUpdate { get; set; }

    public Toman TotalPrice
    {
        get
        {
            Toman totalPrice = Items.SumToman(f => f.TotalPrice);
            if (ShippingMethod != null)
                totalPrice += ShippingMethod.ShippingCost;

            if (Discount != null)
            {
                if (Discount.DiscountAmount != null)
                {
                    totalPrice -= Discount.DiscountAmount;
                }
                else if (Discount.DiscountPercentage > 0)
                {
                    Toman discountValue = (totalPrice * (int)Discount.DiscountPercentage) / 100;
                    totalPrice -= discountValue;
                }

            }
            totalPrice += totalPrice * 0.09m; // 9% tax


            return totalPrice;
        }
    }

    public int ItemCount => Items.Count;

    public void AddItem(OrderItem item)
    {
        ChangeOrderGuard();

        var oldItem = Items.FirstOrDefault(f => f.InventoryId == item.InventoryId);
        if (oldItem != null)
        {
            oldItem.ChangeCount(item.Count + oldItem.Count);
            return;
        }
        Items.Add(item);
    }

    public void RemoveItem(long itemId)
    {
        ChangeOrderGuard();

        var currentItem = Items.FirstOrDefault(f => f.Id == itemId);
        if (currentItem != null)
            Items.Remove(currentItem);
    }

    public void IncreaseItemCount(long itemId, int count)
    {
        ChangeOrderGuard();

        var currentItem = Items.FirstOrDefault(f => f.Id == itemId);
        if (currentItem == null)
            throw new NullOrEmptyDomainDataException();

        currentItem.IncreaseCount(count);
    }

    public void DecreaseItemCount(long itemId, int count)
    {
        ChangeOrderGuard();

        var currentItem = Items.FirstOrDefault(f => f.Id == itemId);
        if (currentItem == null)
            throw new NullOrEmptyDomainDataException();

        currentItem.DecreaseCount(count);
    }

    public void ChangeCountItem(long itemId, int newCount)
    {
        ChangeOrderGuard();

        var currentItem = Items.FirstOrDefault(f => f.Id == itemId);
        if (currentItem == null)
            throw new NullOrEmptyDomainDataException();

        currentItem.ChangeCount(newCount);
    }

    public void Finally()
    {
        Status = OrderStatus.Finalized;
        LastUpdate = DateTime.Now;
        //AddDomainEvent(new OrderFinalized(Id));
    }
    public void ChangeStatus(OrderStatus status)
    {
        Status = status;
        LastUpdate = DateTime.Now;
    }

    public void Checkout(OrderAddress orderAddress, OrderShippingMethod shippingMethod)
    {
        ChangeOrderGuard();

        Address = orderAddress;
        ShippingMethod = shippingMethod;
    }

    public void ChangeOrderGuard()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidDomainDataException("امکان ویرایش این سفارش وجود ندارد");
    }
}