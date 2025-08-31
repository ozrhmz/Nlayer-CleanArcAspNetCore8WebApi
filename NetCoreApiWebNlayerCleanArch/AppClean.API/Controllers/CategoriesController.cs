using App.Application.Features.Categories;
using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Update;
using App.Domain.Entities;
using AppClean.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AppClean.API.Controllers;

public class CategoriesController(ICategoryService categoryService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetCategories() => CreateActionResult(await categoryService.GetAllListAsync());

    [ServiceFilter(typeof(NotFoundFilter<Category, long>))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(long id) => CreateActionResult(await categoryService.GetByIdAsync(id));

    [HttpGet("products")]
    public async Task<IActionResult> GetCategoryWithProducts() =>
        CreateActionResult(await categoryService.GetCategoryWithProductsAsync());

    [HttpGet("{id}/products")]
    public async Task<IActionResult> GetCategoryWithProducts(long id) =>
        CreateActionResult(await categoryService.GetCategoryWithProductsAsync(id));

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryRequest request) =>
        CreateActionResult(await categoryService.CreateAsync(request));

    [ServiceFilter(typeof(NotFoundFilter<Category, long>))]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(long id, UpdateCategoryRequest request) =>
        CreateActionResult(await categoryService.UpdateAsync(id, request));

    [ServiceFilter(typeof(NotFoundFilter<Category, long>))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(long id) =>
        CreateActionResult(await categoryService.DeleteAsync(id));
}
