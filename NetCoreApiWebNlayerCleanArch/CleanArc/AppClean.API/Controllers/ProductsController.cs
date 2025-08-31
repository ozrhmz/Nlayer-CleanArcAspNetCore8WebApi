using App.Application.Features.Products;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Updates;
using App.Application.Features.Products.UpdateStock;
using App.Domain.Entities;
using AppClean.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AppClean.API.Controllers;

public class ProductsController(IProductService productService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        CreateActionResult(await productService.GetAllListAsync());

    [HttpGet("{pageNumber:int}/{pageSize:int}")]
    public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) =>
        CreateActionResult(await productService.GetPagedAllListAsync(pageNumber, pageSize));

    [ServiceFilter(typeof(NotFoundFilter<Product, long>))]
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id) =>
        CreateActionResult(await productService.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request) =>
        CreateActionResult(await productService.CreateAsync(request));

    [ServiceFilter(typeof(NotFoundFilter<Product, long>))]
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, UpdateProductRequest request) =>
        CreateActionResult(await productService.UpdateAsync(id, request));

    [ServiceFilter(typeof(NotFoundFilter<Product, long>))]
    [HttpPatch("stock")]
    public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request) =>
        CreateActionResult(await productService.UpdateStockAsync(request));

    [ServiceFilter(typeof(NotFoundFilter<Product, long>))]
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id) =>
        CreateActionResult(await productService.DeleteAsync(id));
}
