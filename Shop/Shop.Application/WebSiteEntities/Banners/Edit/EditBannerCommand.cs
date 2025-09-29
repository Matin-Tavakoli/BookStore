using Common.Application;
using Microsoft.AspNetCore.Http;
using Shop.Domain.WebSiteEntities.Enums;

namespace Shop.Application.WebSiteEntities.Banners.Edit;

public class EditBannerCommand:IBaseCommand
{
   
    public long Id { get;  set; }
    public string Link { get;  set; }
    public IFormFile? ImageFile { get;  set; }
    public BannerPosition Position { get;  set; }
}