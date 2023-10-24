using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? MinimumStockLevel { get; set; }
        public int ProductCategoryId { get; set; }
        public bool IsActive { get; set; }
    }

    public class GetProductDto : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? MinimumStockLevel { get; set; }
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
    }
}