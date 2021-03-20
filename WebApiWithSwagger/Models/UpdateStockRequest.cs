using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiWithSwagger.Models
{
    public class UpdateStockRequest
    {
        public int StockId { get; set; }
        public string DealerCode { get; set; }
        public int StockLevel { get; set; }
    }
}