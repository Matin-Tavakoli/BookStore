using Common.Application;
using MediatR;
using Shop.Application.WebSiteEntities.Sliders.Create;
using Shop.Application.WebSiteEntities.Sliders.Delete;
using Shop.Application.WebSiteEntities.Sliders.Edit;
using Shop.Query.WebSiteEntities.DTOs;
using Shop.Query.WebSiteEntities.Sliders.GetById;
using Shop.Query.WebSiteEntities.Sliders.GetList;

namespace Shop.Presentation.Facade.WebSiteEntities.Slider;

internal class SliderFacade : ISliderFacade
{
    private readonly IMediator _mediator;

    public SliderFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> CreateSlider(CreateSliderCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditSlider(EditSliderCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DeleteSlider(long sliderId)
    {
        return await _mediator.Send(new DeleteSliderCommand(sliderId));
    }

    public async Task<SliderDto?> GetSliderById(long id)
    {
        return await _mediator.Send(new GetSliderByIdQuery(id));

    }
    public async Task<List<SliderDto>> GetSliders()
    {
        return await _mediator.Send(new GetSliderListQuery());
    }
}