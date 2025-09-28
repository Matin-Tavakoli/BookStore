namespace Shop.Domain.Seller.Services;

public interface ISellerDomainService
{
    bool IsValidSellerInformation(Entities.Seller seller);
    bool NationalCodeExistInDataBase(string nationalCode);
}