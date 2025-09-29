using Common.Application;

namespace Shop.Application.WebSiteEntities.Banners.Delete;

public record DeleteBannerCommand(long Id) : IBaseCommand;