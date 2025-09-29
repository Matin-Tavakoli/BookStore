using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.Category.Repositories;
using Shop.Domain.Comment.Repositories;
using Shop.Domain.Order.Repositories;
using Shop.Domain.Product.Repositories;
using Shop.Domain.Role.Repositories;
using Shop.Domain.Seller.Repositories;
using Shop.Domain.User.Repositories;
using Shop.Domain.WebSiteEntities.Repositories;
using Shop.Infrastructure._Utilities.MediatR;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.EF.Category;
using Shop.Infrastructure.Persistent.EF.Comment;
using Shop.Infrastructure.Persistent.EF.Context;
using Shop.Infrastructure.Persistent.EF.Order;
using Shop.Infrastructure.Persistent.EF.Product;
using Shop.Infrastructure.Persistent.EF.Role;
using Shop.Infrastructure.Persistent.EF.Seller;
using Shop.Infrastructure.Persistent.EF.User;
using Shop.Infrastructure.Persistent.EF.WebSieEntities.Banner;
using Shop.Infrastructure.Persistent.EF.WebSieEntities.ShippingMethod;
using Shop.Infrastructure.Persistent.EF.WebSieEntities.Slider;

namespace Shop.Infrastructure;

public class InfrastructureBootstrapper
{
    public static void Init(IServiceCollection services, string connectionString)
    {
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<ISellerRepository, SellerRepository>();
        services.AddTransient<IBannerRepository, BannerRepository>();
        services.AddTransient<ISliderRepository, SliderRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddTransient<IShippingMethodRepository, ShippingMethodRepository>();

        services.AddSingleton<ICustomPublisher, CustomPublisher>();

        services.AddTransient(_ => new DapperContext(connectionString));
        services.AddDbContext<BookStoreContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });
    }
}