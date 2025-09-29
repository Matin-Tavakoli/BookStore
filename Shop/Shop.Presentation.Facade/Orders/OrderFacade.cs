using Common.Application;
using MediatR;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemQuantity;
using Shop.Application.Orders.Finalized;
using Shop.Application.Orders.IncreaseItemQuantity;
using Shop.Application.Orders.RemoveItem;
using Shop.Application.Orders.SendOrder;
using Shop.Query.Orders.DTOs;
using Shop.Query.Orders.GetByFilter;
using Shop.Query.Orders.GetById;
using Shop.Query.Orders.GetCurrent;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Presentation.Facade.Orders;

internal class OrderFacade : IOrderFacade
{
    private readonly IMediator _mediator;

    public OrderFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AddOrderItem(AddOrderItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> OrderCheckOut(CheckoutOrderCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveOrderItem(RemoveOrderItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> IncreaseItemCount(IncreaseOrderItemQuantityCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DecreaseItemCount(DecreaseOrderItemQuantityCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> FinallyOrder(OrderFinalizedCommand command)
    {
        return await _mediator.Send(command);

    }

    public async Task<OperationResult> SendOrder(long orderId)
    {
        return await _mediator.Send(new SendOrderCommand(orderId));

    }

    public async Task<OrderDto?> GetOrderById(long orderId)
    {
        return await _mediator.Send(new GetOrderByIdQuery(orderId));
    }

    public async Task<OrderFilterResult> GetOrdersByFilter(OrderFilterParams filterParams)
    {
        return await _mediator.Send(new GetOrdersByFilterQuery(filterParams));
    }

    public async Task<OrderDto?> GetCurrentOrder(long userId)
    {
        return await _mediator.Send(new GetCurrentUserOrderQuery(userId));
    }
}