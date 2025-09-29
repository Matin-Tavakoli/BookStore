using Common.Application;
using MediatR;
using Shop.Application.WebSiteEntities.Banners.Create;
using Shop.Application.WebSiteEntities.Banners.Delete;
using Shop.Application.WebSiteEntities.Banners.Edit;
using Shop.Query.WebSiteEntities.Banners.GetById;
using Shop.Query.WebSiteEntities.Banners.GetList;
using Shop.Query.WebSiteEntities.DTOs;

namespace Shop.Presentation.Facade.WebSiteEntities.Banner;

internal class BannerFacade : IBannerFacade
{
    private readonly IMediator _mediator;

    public BannerFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult> CreateBanner(CreateBannerCommand command)
    {
        return await _mediator.Send(command);
    }
    public async Task<OperationResult> EditBanner(EditBannerCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DeleteBanner(long bannerId)
    {
        return await _mediator.Send(new DeleteBannerCommand(bannerId));
    }

    public async Task<BannerDto?> GetBannerById(long id)
    {
        return await _mediator.Send(new GetBannerByIdQuery(id));

    }

    public async Task<List<BannerDto>> GetBanners()
    {
        return await _mediator.Send(new GetBannerListQuery());

    }


}