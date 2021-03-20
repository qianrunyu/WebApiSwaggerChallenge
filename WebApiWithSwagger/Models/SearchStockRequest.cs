using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiWithSwagger.Models
{
    public class SearchStockRequest
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string DealerCode { get; set; }
    }
}