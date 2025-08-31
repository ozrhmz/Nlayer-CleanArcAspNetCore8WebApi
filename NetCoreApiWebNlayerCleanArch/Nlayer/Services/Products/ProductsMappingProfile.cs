﻿using App.Repositories.Products;
using App.Services.Products.Create;
using App.Services.Products.Updates;
using AutoMapper;

namespace App.Services.Products;
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
