using Common.Application;
using Shop.Domain.Seller.Entities;
using Shop.Domain.Seller.Repositories;
using Shop.Domain.Seller.Services;

namespace Shop.Application.Sellers.Create;

internal class CreateSellerCommandHandler : IBaseCommandHandler<CreateSellerCommand>
{
    private readonly ISellerRepository _repository;
    private readonly ISellerDomainService _domainService;
    public CreateSellerCommandHandler(ISellerRepository repository, ISellerDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }

    public async Task<OperationResult> Handle(CreateSellerCommand request, CancellationToken cancellationToken)
    {
        var seller = new Seller(request.UserId, request.ShopName, request.NationalCode, _domainService);

        _repository.Add(seller);
        await _repository.Save();

        return OperationResult.Success();
    }
}