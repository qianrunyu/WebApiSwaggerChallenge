using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiWithSwagger.Models
{
    public class CarStock
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string DealerCode { get; set; } // in real world the code can be a more complex unique string, such as a token
        public int StockLevel { get; set; } = 0;
    }
}