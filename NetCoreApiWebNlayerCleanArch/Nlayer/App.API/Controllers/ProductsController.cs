using App.Repositories.Products;
using App.Services.Filters;
using App.Services.Products;
using App.Services.Products.Create;
using App.Services.Products.Updates;
using App.Services.Products.UpdateStock;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

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
