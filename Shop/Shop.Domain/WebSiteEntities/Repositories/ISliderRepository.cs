using Common.Domain.Repository;
using Shop.Domain.WebSiteEntities.Entities;

namespace Shop.Domain.WebSiteEntities.Repositories;

public interface ISliderRepository : IBaseRepository<Slider>
{
    void Delete(Slider slider);
}