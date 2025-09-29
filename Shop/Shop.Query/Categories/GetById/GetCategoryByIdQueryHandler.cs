using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF.Context;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories.GetById;

internal class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly BookStoreContext _context;

    public GetCategoryByIdQueryHandler(BookStoreContext context)
    {
        _context = context;
    }

    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _context.Categories
            .FirstOrDefaultAsync(f => f.Id == request.CategoryId, cancellationToken);
        return model.Map();
    }
}