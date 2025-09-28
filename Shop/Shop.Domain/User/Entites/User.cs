using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.User.Enums;
using Shop.Domain.User.Services;

namespace Shop.Domain.User.Entites;

public class User : AggregateRoot
{
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsDeleted { get; private set; }
    public Gender Gender { get; private set; }
    public List<UserRole> Roles { get; private set; }
    public List<Wallet> Wallets { get; private set; }
    public List<UserAddress> Addresses { get; private set; }

    public User(string name, string family, string phoneNumber, string email, string password, Gender gender , IUserDomainService userDomainService)
    {
        Guard(phoneNumber, email, userDomainService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        Gender = gender;
        IsActive = false;
        IsDeleted = false;
    }

    public static User Register(string phoneNumber, string email, string password, IUserDomainService userDomainService)
    {
        var user = new User("", "", phoneNumber, email, password, Gender.None, userDomainService);
        return user;
    }

    public void Edit(string name, string family, string phoneNumber, string email, Gender gender)
    {
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
    }

    public void AddAddress(UserAddress address)
    {
        address.UserId = Id;
        Addresses.Add(address);
    }

    public void EditeAddress(UserAddress address)
    {
        var existAddress = Addresses.FirstOrDefault(a => a.Id == address.Id);
        if (existAddress == null)
            throw new Exception("Address not found");
        Addresses.Remove(existAddress);
        Addresses.Add(address);
    }

    public void DeleteAddress(long addressId)
    {
        var existAddress = Addresses.FirstOrDefault(a => a.Id == addressId);
        if (existAddress == null)
            throw new Exception("Address not found");
        Addresses.Remove(existAddress);
    }
    public void ChargeWallet(Wallet wallet)
    {
        wallet.UserId = Id;
        Wallets.Add(wallet);
    }
    public void SetRole(List<UserRole> roles)
    {
        roles.ForEach(f => f.UserId = Id);
        Roles.Clear();
        Roles.AddRange(roles);
    }

    public void Guard( string phoneNumber , string email, IUserDomainService userDomainService)
    {
        NullOrEmptyDomainDataException.CheckString(email, nameof(email));
        NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));
        if (!email.IsValidEmail())
        {
            throw new InvalidDomainDataException("Email is not valid");
        }

        if (phoneNumber != PhoneNumber)
        {
            if (userDomainService.IsPhoneNumberExist(phoneNumber))
            {
                throw new InvalidDomainDataException("Phone number is exist");
            }
        }

        if (email != Email)
        {
            if (userDomainService.IsEmailExist(email))
            {
                throw new InvalidDomainDataException("Email is exist");
            }
        }
    }
}