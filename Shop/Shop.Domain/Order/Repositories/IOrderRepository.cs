using Common.Domain.Repository;

namespace Shop.Domain.Order.Repositories;

public interface IOrderRepository : IBaseRepository<Entities.Order>
{
    Task<Entities.Order> GetCurrentUserOrder(long userId);
}