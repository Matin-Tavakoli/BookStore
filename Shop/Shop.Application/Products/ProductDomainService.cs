using Shop.Domain.Product.Repositories;
using Shop.Domain.Product.Services;

namespace Shop.Application.Products;

public class ProductDomainService:IProductDomainService
{
    private readonly IProductRepository _repository;

    public ProductDomainService(IProductRepository repository)
    {
        _repository = repository;
    }

    public bool SlugIsExist(string slug)
    {
        return _repository.Exists(s => s.Slug == slug);
    }
}