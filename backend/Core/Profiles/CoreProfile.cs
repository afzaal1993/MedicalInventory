using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Controllers;
using Core.DTOs;

namespace Core.Profiles
{
    public class CoreProfile : Profile
    {
        public CoreProfile()
        {
            CreateMap<CreateProductCategoryDto, ProductCategory>();
        }
    }
}