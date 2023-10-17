using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProductCategory : BaseEntity
    {
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}