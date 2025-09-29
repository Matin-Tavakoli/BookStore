using Common.Domain.ValueObjects.Money;
using Common.Query;
using Common.Query.Filter;
using Shop.Domain.Order.Entities;
using Shop.Domain.Order.Enums;
using Shop.Domain.Order.ValueObjects;

namespace Shop.Query.Orders.DTOs;

public class OrderDto : BaseDto
{
    public long UserId { get; set; }
    public string UserFullName { get; set; }
    public OrderStatus Status { get; set; }
    public OrderDiscount? Discount { get; set; }
    public OrderAddress? Address { get; set; }
    public OrderShippingMethod? ShippingMethod { get; set; }
    public List<OrderItemDto> Items { get; set; }
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
}

public class OrderItemDto : BaseDto
{
    public string ProductTitle { get; set; }
    public string ProductSlug { get; set; }
    public string ProductImageName { get; set; }
    public string ShopName { get; set; }
    public long OrderId { get; set; }
    public long InventoryId { get; set; }
    public int Count { get; set; }
    public Toman Price { get; private set; }
    public Toman TotalPrice => Price * Count;
}
public class OrderFilterData : BaseDto
{
    public long UserId { get; set; }
    public string UserFullName { get; set; }
    public OrderStatus Status { get; set; }
    public string? Shire { get; set; }
    public string? City { get; set; }
    public string? ShippingType { get; set; }
    public Toman TotalPrice { get; set; }
    public int TotalItemCount { get; set; }
}


public class OrderFilterParams : BaseFilterParam
{
    public long? UserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public OrderStatus? Status { get; set; }

}
public class OrderFilterResult : BaseFilter<OrderFilterData, OrderFilterParams>
{

}