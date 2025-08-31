using App.Services.Products;

namespace App.Services.Categories.Dto;

public record CategoryWithProductsDto(long Id, string Name, List<ProductDto> products)
{
}

