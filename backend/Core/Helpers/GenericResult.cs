using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public class GenericResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string TechnicalErrorMsg { get; set; }
    }
}