using Common.Query;

namespace Shop.Query.WebSiteEntities.DTOs;

public class ShippingMethodDto:BaseDto
{
    public string Title { get; set; }
    public int Cost { get; set; }
}