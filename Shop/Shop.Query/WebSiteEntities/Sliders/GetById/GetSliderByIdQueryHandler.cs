using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF.Context;
using Shop.Query.WebSiteEntities.DTOs;

namespace Shop.Query.WebSiteEntities.Sliders.GetById;

public class GetSliderByIdQueryHandler : IQueryHandler<GetSliderByIdQuery, SliderDto>
{
    private readonly BookStoreContext _context;

    public GetSliderByIdQueryHandler(BookStoreContext context)
    {
        _context = context;
    }

    public async Task<SliderDto> Handle(GetSliderByIdQuery request, CancellationToken cancellationToken)
    {
        var slider = await _context.Sliders
            .FirstOrDefaultAsync(f => f.Id == request.SliderId, cancellationToken);
        if (slider == null)
            return null;

        return new SliderDto()
        {
            Id = slider.Id,
            CreationDate = slider.CreationDate,
            ImageName = slider.ImageName,
            Link = slider.Link,
            Title = slider.Title
        };
    }
}