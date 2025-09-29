using Common.Query;
using Shop.Query.WebSiteEntities.DTOs;

namespace Shop.Query.WebSiteEntities.Banners.GetById;

public record GetBannerByIdQuery(long BannerId) : IQuery<BannerDto>;