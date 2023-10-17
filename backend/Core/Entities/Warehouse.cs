using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Warehouse : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Store> Stores { get; set; }
    }
}