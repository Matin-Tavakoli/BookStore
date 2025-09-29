using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF.Context;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.GetById;

public class GetSellerByIdQueryHandler : IQueryHandler<GetSellerByIdQuery, SellerDto?>
{
    private BookStoreContext _shopContext;

    public GetSellerByIdQueryHandler(BookStoreContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<SellerDto?> Handle(GetSellerByIdQuery request, CancellationToken cancellationToken)
    {
        var seller = await _shopContext.Sellers.FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken: cancellationToken);
        return seller.Map();
    }
}