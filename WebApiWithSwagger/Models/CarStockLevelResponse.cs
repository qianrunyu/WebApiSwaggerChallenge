using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiWithSwagger.Models
{
    public class CarStockLevelResponse
    {
        public CarStock[] CarStocks { get; set; }
    }
}