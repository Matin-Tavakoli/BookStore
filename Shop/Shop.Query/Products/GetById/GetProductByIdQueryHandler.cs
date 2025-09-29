using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF.Context;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products.GetById;

public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly BookStoreContext _context;

    public GetProductByIdQueryHandler(BookStoreContext context)
    {
        _context = context;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(f => f.Id == request.ProductId, cancellationToken);

        var model = product.Map();
        if (model == null)
            return null;
        await model.SetCategories(_context);
        return model;
    }
}