using Common.Domain;
using Common.Domain.ValueObjects.Money;

namespace Shop.Domain.Order.ValueObjects;

public class OrderShippingMethod : ValueObject
{
    public OrderShippingMethod(string shippingType, Toman shippingCost)
    {
        ShippingType = shippingType;
        ShippingCost = shippingCost;
    }

    public string ShippingType { get; private set; }
    public Toman ShippingCost { get; private set; }
}