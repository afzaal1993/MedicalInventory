using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProductSupplier
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public Product Product { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}