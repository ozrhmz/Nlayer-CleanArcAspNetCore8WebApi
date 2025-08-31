using App.Application.Features.Products;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Updates;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Products;
public class ProductsMappingProfile : Profile
{
    public ProductsMappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<CreateProductRequest, Product>().ForMember(dest => dest.Name,
            opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
        CreateMap<UpdateProductRequest, Product>().ForMember(dest => dest.Name,
            opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
    }
}
