using Common.Application;
using Shop.Application.WebSiteEntities.Banners.Create;
using Shop.Application.WebSiteEntities.Banners.Edit;
using Shop.Query.WebSiteEntities.DTOs;

namespace Shop.Presentation.Facade.WebSiteEntities.Banner;

public interface IBannerFacade
{
    Task<OperationResult> CreateBanner(CreateBannerCommand command);
    Task<OperationResult> EditBanner(EditBannerCommand command);
    Task<OperationResult> DeleteBanner(long bannerId);

    Task<BannerDto?> GetBannerById(long id);
    Task<List<BannerDto>> GetBanners();
}