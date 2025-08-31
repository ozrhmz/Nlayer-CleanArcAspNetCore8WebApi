using App.Application.Features.Products;

namespace App.Application.Features.Categories.Dto;

public record CategoryWithProductsDto(long Id, string Name, List<ProductDto> products)
{
}

