using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Categories;

public class CategoryRepository(AppDbContext context) : GenericRepository<Category, long>(context), ICategoryRepository
{
    public IQueryable<Category> GetCategoryByProductAsync()
    {
        return context.Categories.Include(x => x.Products).AsQueryable();
    }

    public Task<Category?> GetCategoryWithProductsAsync(long id)
    {
        return context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
    }
}

