using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class CreateProductCategoryDto
    {
        public string CategoryName { get; set; }
        public string CreatedBy { get; set; }
    }
}