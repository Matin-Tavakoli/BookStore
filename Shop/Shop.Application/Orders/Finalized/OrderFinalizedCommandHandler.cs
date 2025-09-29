using Common.Application;
using Shop.Domain.Order.Repositories;

namespace Shop.Application.Orders.Finalized;

public class OrderFinalizedCommandHandler : IBaseCommandHandler<OrderFinalizedCommand>
{
    private readonly IOrderRepository _repository;
    public OrderFinalizedCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(OrderFinalizedCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetTracking(request.OrderId);
        if (order == null)
            return OperationResult.NotFound();

        order.Finally();
        await _repository.Save();
        return OperationResult.Success();
    }
}