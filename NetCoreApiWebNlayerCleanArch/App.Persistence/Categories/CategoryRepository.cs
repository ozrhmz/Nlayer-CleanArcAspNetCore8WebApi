using App.Application.Contracts.Persistence;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Categories;

public class CategoryRepository(AppDbContext context) : GenericRepository<Category, long>(context), ICategoryRepository
{
    public Task<List<Category>> GetCategoryByProductAsync()
    {
        return context.Categories.Include(x => x.Products).ToListAsync();
    }

    public Task<Category?> GetCategoryWithProductsAsync(long id)
    {
        return context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
    }
}

