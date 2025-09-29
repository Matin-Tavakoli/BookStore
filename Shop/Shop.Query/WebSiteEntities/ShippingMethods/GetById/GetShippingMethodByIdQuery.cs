using Common.Application;
using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF.Context;
using Shop.Query.WebSiteEntities.DTOs;

namespace Shop.Query.WebSiteEntities.ShippingMethods.GetById;

public record GetShippingMethodByIdQuery(long Id) : IQuery<ShippingMethodDto?>;

public class GetShippingMethodByIdQueryHandler : IQueryHandler<GetShippingMethodByIdQuery, ShippingMethodDto?>
{
    private readonly BookStoreContext _context;

    public GetShippingMethodByIdQueryHandler(BookStoreContext context)
    {
        _context = context;
    }

    public async Task<ShippingMethodDto?> Handle(GetShippingMethodByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.ShippingMethods.Select(s => new ShippingMethodDto
        {
            Id = s.Id,
            CreationDate = s.CreationDate,
            Title = s.Title,
            Cost = s.Cost
        }).FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken: cancellationToken);
    }
}