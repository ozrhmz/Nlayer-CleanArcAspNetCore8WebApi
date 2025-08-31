namespace App.Services.Products;

public record ProductDto(long Id, string Name, decimal Price, int Stock, long CategoryId);
