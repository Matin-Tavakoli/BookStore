using Common.Application;
using Common.Domain.ValueObjects.Money;
using Shop.Domain.Order.Entities;
using Shop.Domain.Order.Repositories;
using Shop.Domain.Order.ValueObjects;
using Shop.Domain.WebSiteEntities.Repositories;

namespace Shop.Application.Orders.Checkout
{
    public class CheckoutOrderCommandHandler : IBaseCommandHandler<CheckoutOrderCommand>
    {
        private readonly IOrderRepository _repository;
        private IShippingMethodRepository _shippingMethodRepository;
        public CheckoutOrderCommandHandler(IOrderRepository repository, IShippingMethodRepository shippingMethodRepository)
        {
            _repository = repository;
            _shippingMethodRepository = shippingMethodRepository;
        }

        public async Task<OperationResult> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var currentOrder = await _repository.GetCurrentUserOrder(request.UserId);
            if (currentOrder == null)
                return OperationResult.NotFound();

            var address = new OrderAddress(request.Shire, request.City, request.PostalCode,
                request.PostalAddress, request.PhoneNumber, request.Name,
                request.Family, request.NationalCode);

            var shippingMethod = await _shippingMethodRepository.GetAsync(request.ShippingMethodId);
            if (shippingMethod == null)
                return OperationResult.Error();


            currentOrder.Checkout(address, new OrderShippingMethod(shippingMethod.Title, new Toman(shippingMethod.Cost)));

            await _repository.Save();
            return OperationResult.Success();
        }
    }
}