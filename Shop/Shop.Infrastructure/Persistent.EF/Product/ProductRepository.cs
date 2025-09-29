using Shop.Domain.Product.Repositories;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.EF.Context;

namespace Shop.Infrastructure.Persistent.EF.Product;

public class ProductRepository : BaseRepository<Domain.Product.Entities.Product>, IProductRepository
{
    public ProductRepository(BookStoreContext context) : base(context)
    {
    }
}