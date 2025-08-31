namespace App.Repositories.Categories;

public interface ICategoryRepository : IGenericRepository<Category,long>
{
    IQueryable<Category> GetCategoryByProductAsync();
    Task<Category?> GetCategoryWithProductsAsync(long id);
}
