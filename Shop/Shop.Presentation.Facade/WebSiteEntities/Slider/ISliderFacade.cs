using Common.Application;
using Shop.Application.WebSiteEntities.Sliders.Create;
using Shop.Application.WebSiteEntities.Sliders.Edit;
using Shop.Query.WebSiteEntities.DTOs;

namespace Shop.Presentation.Facade.WebSiteEntities.Slider;

public interface ISliderFacade
{
    Task<OperationResult> CreateSlider(CreateSliderCommand command);
    Task<OperationResult> EditSlider(EditSliderCommand command);
    Task<OperationResult> DeleteSlider(long sliderId);

    Task<SliderDto?> GetSliderById(long id);
    Task<List<SliderDto>> GetSliders();
}