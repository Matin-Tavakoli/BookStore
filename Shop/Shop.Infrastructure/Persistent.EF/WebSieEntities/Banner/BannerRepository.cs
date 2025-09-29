
using Shop.Domain.WebSiteEntities.Repositories;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.EF.Context;

namespace Shop.Infrastructure.Persistent.EF.WebSieEntities.Banner
{
    internal class BannerRepository : BaseRepository<Domain.WebSiteEntities.Entities.Banner>, IBannerRepository
    {
        public BannerRepository(BookStoreContext context) : base(context)
        {
        }

        public void Delete(Domain.WebSiteEntities.Entities.Banner banner)
        {
            Context.Banners.Remove(banner);
        }
    }
}