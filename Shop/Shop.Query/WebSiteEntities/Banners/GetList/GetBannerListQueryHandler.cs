using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF.Context;
using Shop.Query.WebSiteEntities.DTOs;

namespace Shop.Query.WebSiteEntities.Banners.GetList;

public class GetBannerListQueryHandler : IQueryHandler<GetBannerListQuery, List<BannerDto>>
{
    private readonly BookStoreContext _context;

    public GetBannerListQueryHandler(BookStoreContext context)
    {
        _context = context;
    }

    public async Task<List<BannerDto>> Handle(GetBannerListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Banners.OrderByDescending(d => d.Id)
            .Select(banner => new BannerDto()
            {
                Id = banner.Id,
                CreationDate = banner.CreationDate,
                ImageName = banner.ImageName,
                Link = banner.Link,
                Position = banner.Position
            }).ToListAsync(cancellationToken);
    }
}