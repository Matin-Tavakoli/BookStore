using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF.Context;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.GetByUserId;

public class GetSellerByUserIdQueryHandler : IQueryHandler<GetSellerByUserIdQuery, SellerDto?>
{
    private readonly BookStoreContext _context;

    public GetSellerByUserIdQueryHandler(BookStoreContext context)
    {
        _context = context;
    }

    public async Task<SellerDto?> Handle(GetSellerByUserIdQuery request, CancellationToken cancellationToken)
    {
        var seller = await _context.Sellers.FirstOrDefaultAsync(f => f.UserId == request.UserId,
            cancellationToken: cancellationToken);
        return seller.Map();
    }
}