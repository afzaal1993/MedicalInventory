using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Supplier : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactInformation { get; set; }
    }
}