using Common.Application;

namespace Shop.Application.Orders.DecreaseItemQuantity;
public record DecreaseOrderItemQuantityCommand(long UserId, long ItemId, int Count) : IBaseCommand;