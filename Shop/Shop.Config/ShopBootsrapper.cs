using FluentValidation; 
using MediatR;         
using Microsoft.Extensions.DependencyInjection;

using Shop.Application._Utilities;
using Shop.Application.Categories;
using Shop.Application.Products;
using Shop.Application.Roles.Create;
using Shop.Application.Sellers;
using Shop.Application.Users;

using Shop.Domain.Category.Services;
using Shop.Domain.Product.Services;
using Shop.Domain.Seller.Services;
using Shop.Domain.User.Services;

using Shop.Infrastructure;
using Shop.Query.Categories.GetById;

namespace Shop.Config
{
    public static class ShopBootstrapper
    {
        public static void RegisterShopDependency(this IServiceCollection services, string connectionString)
        {
            InfrastructureBootstrapper.Init(services, connectionString);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(Directories).Assembly,           
                    typeof(GetCategoryByIdQuery).Assembly   
                );
            });

            // Domain services
            services.AddTransient<IProductDomainService, ProductDomainService>();
            services.AddTransient<IUserDomainService, UserDomainService>();
            services.AddTransient<ICategoryDomainService, CategoryDomainService>();
            services.AddTransient<ISellerDomainService, SellerDomainService>();

            services.AddValidatorsFromAssemblyContaining<CreateRoleCommandValidator>();

        }
    }
}
