using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class WarehouseStock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public string BatchNumber { get; set; }
        public string BatchExpiryDate { get; set; }
        public int StockQty { get; set; }
    }
}