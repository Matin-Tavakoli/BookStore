using Shop.Domain.Category.Repositories;
using Shop.Domain.Category.Services;

namespace Shop.Application.Categories;

public class CategoryDomainService:ICategoryDomainService
{
    private readonly ICategoryRepository _repository;

    public CategoryDomainService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public bool IsSlugExist(string slug)
    {
        return _repository.Exists(s => s.Slug == slug);
    }
}