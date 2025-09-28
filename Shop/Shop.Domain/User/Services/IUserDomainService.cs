namespace Shop.Domain.User.Services;

public interface IUserDomainService
{
    bool IsEmailExist(string Email);
    bool IsPhoneNumberExist(string PhoneNumber);
}