using App.Application.Contracts.Caching;
using App.Application.Contracts.Persistence;
using App.Application.Contracts.ServiceBus;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Updates;
using App.Application.Features.Products.UpdateStock;
using App.Domain.Entities;
using App.Domain.Events;
using App.Services.Products.Create;
using AutoMapper;
using System.Net;

namespace App.Application.Features.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService, IServiceBus serviceBus) : IProductService
{
    private const string ProductListCacheKey = "ProcudtListCacheKey";

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
        List<ProductDto> productListAsCached = await cacheService.GetAsync<List<ProductDto>>(ProductListCacheKey);

        if (productListAsCached is not null) return ServiceResult<List<ProductDto>>.Success(productListAsCached);

        List<Product> products = await productRepository.GetAllAsync();
        List<ProductDto> productsAsDto = mapper.Map<List<ProductDto>>(products);
        await cacheService.AddAsync(ProductListCacheKey, productsAsDto, TimeSpan.FromMinutes(1));
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
    {
        List<Product> products = await productRepository.GetAllPagedAsync(pageNumber, pageSize);
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
        bool anyProduct = await productRepository.AnyAsync(x => x.Name == request.Name);

        if (anyProduct)
            return ServiceResult<CreateProductResponse>.Fail("Product already exist", HttpStatusCode.BadRequest);

        Product product = mapper.Map<Product>(request);

        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();

        await serviceBus.PublishAsync(new ProductAddedEvent(product.Id, product.Name, product.Price));

        return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id),
            $"api/products/{product.Id}");
    }

    public async Task<ServiceResult> UpdateAsync(long id, UpdateProductRequest request)
    {
        bool anyProduct = await productRepository.AnyAsync(x => x.Name == request.Name && x.Id != id);

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