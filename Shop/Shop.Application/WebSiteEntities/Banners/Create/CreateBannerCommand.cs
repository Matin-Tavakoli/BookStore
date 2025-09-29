﻿using Common.Application;
using Microsoft.AspNetCore.Http;
using Shop.Domain.WebSiteEntities.Enums;

namespace Shop.Application.WebSiteEntities.Banners.Create;

public class CreateBannerCommand : IBaseCommand
{
  
    public string Link { get;  set; }
    public IFormFile ImageFile { get;  set; }
    public BannerPosition Position { get;  set; }
}