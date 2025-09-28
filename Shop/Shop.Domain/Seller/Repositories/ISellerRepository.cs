using Common.Domain.Repository;
using Shop.Domain.Seller.Entities;

namespace Shop.Domain.Seller.Repositories;

public interface ISellerRepository : IBaseRepository<Entities.Seller>
{
    Task<InventoryResult?> GetInventoryById(long id);
}