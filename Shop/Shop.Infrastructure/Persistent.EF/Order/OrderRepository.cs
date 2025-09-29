using Microsoft.EntityFrameworkCore;
using Shop.Domain.Order.Enums;
using Shop.Domain.Order.Repositories;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.EF.Context;

namespace Shop.Infrastructure.Persistent.EF.Order
{
    internal class OrderRepository : BaseRepository<Domain.Order.Entities.Order>, IOrderRepository
    {

        public OrderRepository(BookStoreContext context) : base(context)
        {
        }
        public async Task<Domain.Order.Entities.Order?> GetCurrentUserOrder(long userId)
        {
            return await Context.Orders.AsTracking().FirstOrDefaultAsync(f => f.UserId == userId
            && f.Status == OrderStatus.Pending);
        }

       
    }
}