using Common.Domain.Repository;
using Shop.Domain.WebSiteEntities.Entities;

namespace Shop.Domain.WebSiteEntities.Repositories;

public interface IShippingMethodRepository : IBaseRepository<ShippingMethod>
{
    void Delete(ShippingMethod method);
}