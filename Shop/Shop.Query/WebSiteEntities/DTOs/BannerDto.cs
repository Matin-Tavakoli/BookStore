using Common.Query;
using Shop.Domain.WebSiteEntities.Enums;

namespace Shop.Query.WebSiteEntities.DTOs;

public class BannerDto : BaseDto
{
    public string Link { get;  set; }
    public string ImageName { get;  set; }
    public BannerPosition Position { get;  set; }
}
 