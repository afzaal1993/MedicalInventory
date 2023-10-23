using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Helpers;

namespace Core.DTOs
{
    public class CreateProductCategoryDto
    {
        public string CategoryName { get; set; }
        public string CreatedBy { get; set; }
    }

    public class GetProductCategoryDto : BaseEntity
    {
        public string CategoryName { get; set; }
    }
}