using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class SupplierDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactInformation { get; set; }
        public bool IsActive { get; set; }
    }
}