using Common.Application;
using MediatR;
using Shop.Application.WebSiteEntities.ShippingMethods.Create;
using Shop.Application.WebSiteEntities.ShippingMethods.Delete;
using Shop.Application.WebSiteEntities.ShippingMethods.Edit;
using Shop.Query.WebSiteEntities.DTOs;
using Shop.Query.WebSiteEntities.ShippingMethods.GetById;
using Shop.Query.WebSiteEntities.ShippingMethods.GetList;

namespace Shop.Presentation.Facade.WebSiteEntities.ShippingMethods;

public interface IShippingMethodFacade
{
    Task<OperationResult> Create(CreateShippingMethodCommand command);
    Task<OperationResult> Edit(EditShippingMethodCommand command);
    Task<OperationResult> Delete(long id);


    Task<ShippingMethodDto?> GetShippingMethodById(long id);
    Task<List<ShippingMethodDto>> GetList();
}

internal class ShippingMethodFacade : IShippingMethodFacade
{
    private readonly IMediator _mediator;

    public ShippingMethodFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Create(CreateShippingMethodCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditShippingMethodCommand command)
    {
        return await _mediator.Send(command);

    }

    public async Task<OperationResult> Delete(long id)
    {
        return await _mediator.Send(new DeleteShippingMethodCommand(id));

    }

    public async Task<ShippingMethodDto?> GetShippingMethodById(long id)
    {
        return await _mediator.Send(new GetShippingMethodByIdQuery(id));

    }

    public async Task<List<ShippingMethodDto>> GetList()
    {
        return await _mediator.Send(new GetShippingMethodsByListQuery());

    }
}