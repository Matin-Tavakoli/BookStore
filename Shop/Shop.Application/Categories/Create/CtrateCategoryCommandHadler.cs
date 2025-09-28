using Common.Application;
using Shop.Domain.Category.Entities;
using Shop.Domain.Category.Repositories;
using Shop.Domain.Category.Services;

namespace Shop.Application.Categories.Create;

public class CtrateCategoryCommandHadler : IBaseCommandHandler<CreateCategoryCommand>
{
    private readonly ICategoryRepository _repository;
    private readonly ICategoryDomainService _domainService;

    public CtrateCategoryCommandHadler(ICategoryRepository repository, ICategoryDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }
    public async Task<OperationResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Title, request.Slug, request.SeoData, _domainService);
        _repository.Add(category);
        await _repository.Save();
        return OperationResult.Success();
    }
}