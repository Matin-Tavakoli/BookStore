using Common.Application;

namespace Shop.Application.WebSiteEntities.ShippingMethods.Delete;

public record DeleteShippingMethodCommand(long Id):IBaseCommand;