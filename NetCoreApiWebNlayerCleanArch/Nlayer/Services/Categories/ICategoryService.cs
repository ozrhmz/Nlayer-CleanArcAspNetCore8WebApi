using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;

namespace App.Services.Categories;

public interface ICategoryService
{
    public Task<ServiceResult<List<CategoryDto>>> GetAllListAsync();
    public Task<ServiceResult<CategoryDto>> GetByIdAsync(long id);
    public Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync();
    public Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(long categoryId);
    public Task<ServiceResult<long>> CreateAsync(CreateCategoryRequest request);
    public Task<ServiceResult> UpdateAsync(long id, UpdateCategoryRequest request);
    public Task<ServiceResult> DeleteAsync(long id);
}

