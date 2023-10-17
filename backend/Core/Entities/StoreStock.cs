using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class StoreStock
    {
        public int Id { get; set; }
        public string BatchNumber { get; set; }
        public string BatchExpiryDate { get; set; }
        public int StockQty { get; set; }
        public int ProductId { get; set; }
        public List<Product> Products { get; set; }
        public int StoreId { get; set; }
        public List<Store> Stores { get; set; }
    }
}