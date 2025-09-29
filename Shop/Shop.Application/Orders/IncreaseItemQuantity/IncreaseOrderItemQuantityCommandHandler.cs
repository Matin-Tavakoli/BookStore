using Common.Application;
using Shop.Domain.Order.Repositories;

namespace Shop.Application.Orders.IncreaseItemQuantity
{
    public class IncreaseOrderItemQuantityCommandHandler : IBaseCommandHandler<IncreaseOrderItemQuantityCommand>
    {
        private readonly IOrderRepository _repository;

        public IncreaseOrderItemQuantityCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(IncreaseOrderItemQuantityCommand request, CancellationToken cancellationToken)
        {
            var currentOrder = await _repository.GetCurrentUserOrder(request.UserId);
            if (currentOrder == null)
                return OperationResult.NotFound();

            currentOrder.IncreaseItemCount(request.ItemId, request.Count);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}