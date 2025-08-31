using App.Domain.Entities;

namespace App.Application.Contracts.Persistence;

public interface ICategoryRepository : IGenericRepository<Category, long>
{
    Task<List<Category>> GetCategoryByProductAsync();
    Task<Category?> GetCategoryWithProductsAsync(long id);
}
