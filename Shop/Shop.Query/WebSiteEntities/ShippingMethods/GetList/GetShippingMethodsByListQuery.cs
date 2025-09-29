using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF.Context;
using Shop.Query.WebSiteEntities.DTOs;

namespace Shop.Query.WebSiteEntities.ShippingMethods.GetList;

public class GetShippingMethodsByListQuery : IQuery<List<ShippingMethodDto>>
{

}

internal class GetShippingMethodsByListQueryHandler : IQueryHandler<GetShippingMethodsByListQuery, List<ShippingMethodDto>>
{
    private readonly BookStoreContext _context;

    public GetShippingMethodsByListQueryHandler(BookStoreContext context)
    {
        _context = context;
    }

    public async Task<List<ShippingMethodDto>> Handle(GetShippingMethodsByListQuery request, CancellationToken cancellationToken)
    {
        return await _context.ShippingMethods.Select(s => new ShippingMethodDto
        {
            Id = s.Id,
            CreationDate = s.CreationDate,
            Title = s.Title,
            Cost = s.Cost
        }).ToListAsync(cancellationToken);
    }
}