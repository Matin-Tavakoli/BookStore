using Common.Domain.Repository;

namespace Shop.Domain.Comment.Repositories;

public interface ICommentRepository : IBaseRepository<Entities.Comment>
{
    Task DeleteAndSave(Entities.Comment comment);
}