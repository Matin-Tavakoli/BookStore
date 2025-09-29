namespace Shop.Domain.User.Services;

public interface IUserDomainService
{
    bool IsEmailExist(string email);
    bool IsPhoneNumberExist(string phoneNumber);
}