
using Shop.Domain.WebSiteEntities.Repositories;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.EF.Context;

namespace Shop.Infrastructure.Persistent.EF.WebSieEntities.Slider;

internal class SliderRepository : BaseRepository<Domain.WebSiteEntities.Entities.Slider>, ISliderRepository
{
    public SliderRepository(BookStoreContext context) : base(context)
    {
    }

    public void Delete(Domain.WebSiteEntities.Entities.Slider slider)
    {
        Context.Sliders.Remove(slider);
    }
}