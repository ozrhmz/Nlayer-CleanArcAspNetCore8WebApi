using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;

namespace App.Application.Features.Categories;

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

