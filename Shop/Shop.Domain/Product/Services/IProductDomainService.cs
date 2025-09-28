namespace Shop.Domain.Product.Services;

public interface IProductDomainService
{
    bool SlugIsExist(string slug);

}