using Common.Domain.Repository;
using Shop.Domain.WebSiteEntities.Entities;

namespace Shop.Domain.WebSiteEntities.Repositories;

public interface IBannerRepository : IBaseRepository<Banner>
{
    void Delete(Banner banner);
}