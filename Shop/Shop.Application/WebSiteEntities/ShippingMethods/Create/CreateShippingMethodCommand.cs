using Common.Application;

namespace Shop.Application.WebSiteEntities.ShippingMethods.Create;

public class CreateShippingMethodCommand : IBaseCommand
{
    public string Title { get; set; }
    public int Cost { get; set; }
}