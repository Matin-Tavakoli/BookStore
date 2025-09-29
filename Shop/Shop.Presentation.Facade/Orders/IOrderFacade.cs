using Common.Application;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemQuantity;
using Shop.Application.Orders.Finalized;
using Shop.Application.Orders.IncreaseItemQuantity;
using Shop.Application.Orders.RemoveItem;
using Shop.Query.Orders.DTOs;

namespace Shop.Presentation.Facade.Orders;

public interface IOrderFacade
{
    Task<OperationResult> AddOrderItem(AddOrderItemCommand command);
    Task<OperationResult> OrderCheckOut(CheckoutOrderCommand command);
    Task<OperationResult> RemoveOrderItem(RemoveOrderItemCommand command);
    Task<OperationResult> IncreaseItemCount(IncreaseOrderItemQuantityCommand command);
    Task<OperationResult> DecreaseItemCount(DecreaseOrderItemQuantityCommand command);
    Task<OperationResult> FinallyOrder(OrderFinalizedCommand command);
    Task<OperationResult> SendOrder(long orderId);



    Task<OrderDto?> GetOrderById(long orderId);
    Task<OrderFilterResult> GetOrdersByFilter(OrderFilterParams filterParams);
    Task<OrderDto?> GetCurrentOrder(long userId);
}