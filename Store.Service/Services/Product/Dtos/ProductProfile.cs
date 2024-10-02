﻿using AutoMapper;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Product.Dtos
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<Products, ProductDetailsDto>()
                .ForMember(dest => dest.BrandName, options => options.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.TypeName, options => options.MapFrom(src => src.type.Name))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<ProductPictureUrlResolver>());

            CreateMap<ProductBrand, BrandTypeDetailDto>();
            CreateMap<ProductType, BrandTypeDetailDto>();
        }
    }
}
