using Common.Query;
using Shop.Query.WebSiteEntities.DTOs;

namespace Shop.Query.WebSiteEntities.Sliders.GetById;

public record GetSliderByIdQuery(long SliderId) : IQuery<SliderDto>;
