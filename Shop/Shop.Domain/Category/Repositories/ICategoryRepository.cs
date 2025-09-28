using Common.Domain.Repository;

namespace Shop.Domain.Category.Repositories;

public interface ICategoryRepository : IBaseRepository<Entities.Category>
{
    Task<bool> DeleteCategory(long categoryId);
}