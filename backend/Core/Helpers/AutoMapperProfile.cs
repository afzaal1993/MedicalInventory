using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace Core.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateProductCategoryDto, ProductCategory>();
            CreateMap<ProductCategory, GetProductCategoryDto>();

            CreateMap<SupplierDto, Supplier>();

            CreateMap<ProductDto, Product>();
            CreateMap<Product, GetProductDto>()
                        .ForMember(dest => dest.ProductCategoryName,
                        opt => opt.MapFrom(src => src.ProductCategory.CategoryName));
        }
    }
}