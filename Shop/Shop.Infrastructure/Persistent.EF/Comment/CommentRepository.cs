using Shop.Domain.Comment.Repositories;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.EF.Context;

namespace Shop.Infrastructure.Persistent.EF.Comment;

public class CommentRepository : BaseRepository<Domain.Comment.Entities.Comment>, ICommentRepository
{
    public CommentRepository(BookStoreContext context) : base(context)
    {
    }

    public async Task DeleteAndSave(Domain.Comment.Entities.Comment comment)
    {
        Context.Remove(comment);
        await Context.SaveChangesAsync();
    }
}