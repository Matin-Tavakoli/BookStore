using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.EF.Context;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetById;

internal class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly BookStoreContext _shopContext;
    private readonly DapperContext _dapperContext;

    public GetOrderByIdQueryHandler(BookStoreContext shopContext, DapperContext dapperContext)
    {
        _shopContext = shopContext;
        _dapperContext = dapperContext;
    }
    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _shopContext.Orders
            .FirstOrDefaultAsync(f => f.Id == request.OrderId, cancellationToken);
        if (order == null)
            return null;

        var orderDto = order.Map();
        orderDto.UserFullName = await _shopContext.Users.Where(f => f.Id == orderDto.UserId)
            .Select(s => $"{s.Name} {s.Family}").FirstAsync(cancellationToken);

        orderDto.Items = await orderDto.GetOrderItems(_dapperContext);
        return orderDto;
    }
}