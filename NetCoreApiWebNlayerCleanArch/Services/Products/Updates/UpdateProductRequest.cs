namespace App.Services.Products.Updates;

public record UpdateProductRequest(string Name, decimal Price, int Stock,long categoryId);
