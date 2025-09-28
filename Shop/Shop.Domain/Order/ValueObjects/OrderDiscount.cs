using Common.Domain;
using Common.Domain.ValueObjects.Money;

namespace Shop.Domain.Order.ValueObjects;

public class OrderDiscount : ValueObject
{
    public OrderDiscount(string title, Toman? discountAmount , int? discountPercentage)
    {
        Title = title;
        DiscountAmount = discountAmount;
        DiscountPercentage = discountPercentage;
    }
    public string Title { get; private set; }
    public Toman? DiscountAmount { get; private set; } 
    public int? DiscountPercentage { get; private set; } 
}