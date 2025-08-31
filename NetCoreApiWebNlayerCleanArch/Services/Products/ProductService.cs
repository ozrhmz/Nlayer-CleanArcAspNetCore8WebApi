using App.Repositories;
using App.Repositories.Products;
using App.Services.Products.Create;
using App.Services.Products.Updates;
using App.Services.Products.UpdateStock;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Services.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper) : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        List<Product> products = await productRepository.GetTopPriceProductAsync(count);

        List<ProductDto> productAsDto = mapper.Map<List<ProductDto>>(products);

        return new ServiceResult<List<ProductDto>>()
        {
            Data = productAsDto
        };
    }

    public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
    {
        List<Product> products = await productRepository.GetAll().ToListAsync();
        List<ProductDto> productsAsDto = mapper.Map<List<ProductDto>>(products);
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
    {
        List<Product> products = await productRepository.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        List<ProductDto> productAsDto = mapper.Map<List<ProductDto>>(products);
        return ServiceResult<List<ProductDto>>.Success(productAsDto);
    }

    public async Task<ServiceResult<ProductDto?>> GetByIdAsync(long id)
    {
        Product? product = await productRepository.GetByIdAsync(id);
        ProductDto productAsDto = mapper.Map<ProductDto>(product);
        return ServiceResult<ProductDto>.Success(productAsDto)!;
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        bool anyProduct = await productRepository.Where(x => x.Name == request.Name).AnyAsync();

        if (anyProduct)
            return ServiceResult<CreateProductResponse>.Fail("Product already exist", HttpStatusCode.BadRequest);

        Product product = mapper.Map<Product>(request);

        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id),
            $"api/products/{product.Id}");
    }

    public async Task<ServiceResult> UpdateAsync(long id, UpdateProductRequest request)
    {
        bool anyProduct = await productRepository.Where(x => x.Name == request.Name && x.Id != id).AnyAsync();

        if (anyProduct)
            return ServiceResult.Fail("Product already exist", HttpStatusCode.BadRequest);

        Product? product = mapper.Map<Product>(request);
        product.Id = id;
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
    {
        Product? product = await productRepository.GetByIdAsync(request.ProductId);
        product.Stock = request.Quantity;
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        Product product = await productRepository.GetByIdAsync(id);
        productRepository.Delete(product!);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}