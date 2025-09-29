using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF.Context;
using Shop.Query.Comments.DTOs;

namespace Shop.Query.Comments.GetById;

internal class GetCommentByIdQueryHandler : IQueryHandler<GetCommentByIdQuery, CommentDto?>
{
    private readonly BookStoreContext _context;

    public GetCommentByIdQueryHandler(BookStoreContext context)
    {
        _context = context;
    }

    public async Task<CommentDto?> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var comment =await _context.Comments.FirstOrDefaultAsync(f => f.Id == request.CommentId, cancellationToken: cancellationToken);

        return comment.Map();
    }
}