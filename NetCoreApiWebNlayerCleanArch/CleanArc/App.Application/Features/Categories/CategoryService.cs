using App.Application.Contracts.Persistence;
using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;
using App.Domain.Entities;
using AutoMapper;
using System.Net;

namespace App.Application.Features.Categories;

public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
{
    public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
    {
        List<Category> categories = await categoryRepository.GetAllAsync();
        List<CategoryDto> categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);
        return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
    }

    public async Task<ServiceResult<CategoryDto>> GetByIdAsync(long id)
    {
        Category category = await categoryRepository.GetByIdAsync(id);
        CategoryDto categoryAsDto = mapper.Map<CategoryDto>(category);
        return ServiceResult<CategoryDto>.Success(categoryAsDto);
    }

    public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(long categoryId)
    {
        Category category = await categoryRepository.GetCategoryWithProductsAsync(categoryId);

        if (category is null)
            return ServiceResult<CategoryWithProductsDto>.Fail("Kategori bulunamadı", HttpStatusCode.NotFound);

        CategoryWithProductsDto categoryAsDto = mapper.Map<CategoryWithProductsDto>(category);

        return ServiceResult<CategoryWithProductsDto>.Success(categoryAsDto);
    }

    public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync()
    {
        List<Category> category = await categoryRepository.GetCategoryByProductAsync();

        List<CategoryWithProductsDto> categoryAsDto = mapper.Map<List<CategoryWithProductsDto>>(category);

        return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryAsDto);
    }

    public async Task<ServiceResult<long>> CreateAsync(CreateCategoryRequest request)
    {
        var anyCategory = await categoryRepository.AnyAsync(x => x.Name == request.Name);

        if (anyCategory)
            return ServiceResult<long>.Fail("Kategori ismi mevcuttur.", HttpStatusCode.NotFound);

        Category newCategory = mapper.Map<Category>(request);
        await categoryRepository.AddAsync(newCategory);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult<long>.SuccessAsCreated(newCategory.Id, $"api/categories/{newCategory.Id}");
    }

    public async Task<ServiceResult> UpdateAsync(long id, UpdateCategoryRequest request)
    {
        bool isCategoryNameExist =
            await categoryRepository.AnyAsync(x => x.Name == request.Name && x.Id != id);

        if (isCategoryNameExist)
            return ServiceResult.Fail("Kategori mevcuttur", HttpStatusCode.BadRequest);

        Category category = mapper.Map<Category>(request);
        category.Id = id;
        categoryRepository.Update(category);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        categoryRepository.Delete(category);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}

