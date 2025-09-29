using Common.Application;
using Shop.Domain.Order.Repositories;

namespace Shop.Application.Orders.DecreaseItemQuantity;

public class DecreaseOrderItemQuantityCommandHandler : IBaseCommandHandler<DecreaseOrderItemQuantityCommand>
{
    private readonly IOrderRepository _repository;

    public DecreaseOrderItemQuantityCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(DecreaseOrderItemQuantityCommand request, CancellationToken cancellationToken)
    {
        var currentOrder = await _repository.GetCurrentUserOrder(request.UserId);
        if (currentOrder == null)
            return OperationResult.NotFound();

        currentOrder.DecreaseItemCount(request.ItemId, request.Count);
        await _repository.Save();
        return OperationResult.Success();
    }
}