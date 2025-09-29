using Common.Application.Validation;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Category.Repositories;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.EF.Context;

namespace Shop.Infrastructure.Persistent.EF.Category;

internal class CategoryRepository : BaseRepository<Domain.Category.Entities.Category>, ICategoryRepository
{
    public CategoryRepository(BookStoreContext context) : base(context)
    {
    }

    public async Task<bool> DeleteCategory(long categoryId)
    {
        var category =await Context.Categories
            .Include(c=>c.Childs)
            .ThenInclude(c=>c.Childs).FirstOrDefaultAsync(f => f.Id == categoryId);
        if (category == null)
            return false;


        var isExistProduct = await Context.Products
            .AnyAsync(f => f.CategoryId == categoryId ||
                           f.SubCategoryId == categoryId ||
                           f.SecondarySubCategoryId == categoryId);

        if (isExistProduct)
            return false;

        if (category.Childs.Any(c => c.Childs.Any()))
        {
            Context.RemoveRange(category.Childs.SelectMany(s=>s.Childs));
        }
        Context.RemoveRange(category.Childs);
        Context.RemoveRange(category);
        return true;
    }
}