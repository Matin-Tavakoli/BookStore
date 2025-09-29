using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF.Context;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories.GetByParentId;

internal class GetCategoryByParentIdQueryHandler : IQueryHandler<GetCategoryByParentIdQuery, List<ChildCategoryDto>>
{
    private readonly BookStoreContext _context;

    public GetCategoryByParentIdQueryHandler(BookStoreContext context)
    {
        _context = context;
    }

    public async Task<List<ChildCategoryDto>> Handle(GetCategoryByParentIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Categories
            .Include(c => c.Childs)
            .Where(r => r.ParentId == request.ParentId).ToListAsync(cancellationToken);

        return result.MapChildren();
    }
}