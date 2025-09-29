using Common.Application;

namespace Shop.Application.Orders.IncreaseItemQuantity
{
    public record IncreaseOrderItemQuantityCommand(long UserId, long ItemId, int Count) : IBaseCommand;
}