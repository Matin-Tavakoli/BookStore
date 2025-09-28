namespace Shop.Domain.Category.Services;

public interface ICategoryDomainService
{
    public bool IsSlugExist(string slug);

}