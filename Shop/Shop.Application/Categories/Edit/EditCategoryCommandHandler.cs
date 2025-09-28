using Common.Application;
using Shop.Domain.Category;
using Shop.Domain.Category.Repositories;
using Shop.Domain.Category.Services;

namespace Shop.Application.Categories.Edit
{
    public class EditCategoryCommandHandler : IBaseCommandHandler<EditCategoryCommand>
    {
        private readonly ICategoryRepository _repository;
        private readonly ICategoryDomainService _domainServicer;

        public EditCategoryCommandHandler(ICategoryRepository repository, ICategoryDomainService domainServicer)
        {
            _repository = repository;
            _domainServicer = domainServicer;
        }

        public async Task<OperationResult> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetTracking(request.Id);
            if (category == null)
                return OperationResult.NotFound();

            category.Edit(request.Title, request.Slug, request.SeoData, _domainServicer);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}