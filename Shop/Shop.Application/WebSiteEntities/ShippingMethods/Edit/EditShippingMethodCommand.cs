using Common.Application;

namespace Shop.Application.WebSiteEntities.ShippingMethods.Edit;

public class EditShippingMethodCommand : IBaseCommand
{
    public long Id { get; set; }
    public string Title { get; set; }
    public int Cost { get; set; }
}