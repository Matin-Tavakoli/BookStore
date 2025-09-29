using Common.Application;
using Shop.Domain.WebSiteEntities.Entities;
using Shop.Domain.WebSiteEntities.Repositories;

namespace Shop.Application.WebSiteEntities.ShippingMethods.Create;

internal class CreateShippingMethodCommandHandler : IBaseCommandHandler<CreateShippingMethodCommand>
{
    private readonly IShippingMethodRepository _repository;

    public CreateShippingMethodCommandHandler(IShippingMethodRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(CreateShippingMethodCommand request, CancellationToken cancellationToken)
    {
        _repository.Add(new ShippingMethod(request.Cost, request.Title));
        await _repository.Save();
        return OperationResult.Success();
    }
}