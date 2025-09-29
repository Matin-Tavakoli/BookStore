
using Shop.Domain.WebSiteEntities.Repositories;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.EF.Context;

namespace Shop.Infrastructure.Persistent.EF.WebSieEntities.ShippingMethod;

internal class ShippingMethodRepository : BaseRepository<Domain.WebSiteEntities.Entities.ShippingMethod>, IShippingMethodRepository
{
    public ShippingMethodRepository(BookStoreContext context) : base(context)
    {
    }

    public void Delete(Domain.WebSiteEntities.Entities.ShippingMethod slider)
    {
        Context.ShippingMethods.Remove(slider);
    }
}