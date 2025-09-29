using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Seller;
using Shop.Domain.Seller.Entities;
using Shop.Domain.Seller.Repositories;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.EF.Context;

namespace Shop.Infrastructure.Persistent.EF.Seller;

internal class SellerRepository : BaseRepository<Domain.Seller.Entities.Seller>, ISellerRepository
{
    private readonly DapperContext _dapperContext;
    public SellerRepository(BookStoreContext context, DapperContext dapperContext) : base(context)
    {
        _dapperContext = dapperContext;
    }
    //public async Task<InventoryResult?> GetInventoryById(long id)
    //{
    //    return await Context.Inventories.Where(r => r.Id==id)
    //        .Select(i=>new InventoryResult()
    //        {
    //            Count = i.Count,
    //            Id = i.Id,
    //            Price = i.Price,
    //            ProductId = i.ProductId,
    //            SellerId = i.SellerId
    //        }).FirstOrDefaultAsync();
    //}
    public async Task<InventoryResult?> GetInventoryById(long id)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $"SELECT * from {_dapperContext.Inventories} where Id=@InventoryId";

        return await connection.QueryFirstOrDefaultAsync<InventoryResult>
            (sql, new { InventoryId = id });
    }
}