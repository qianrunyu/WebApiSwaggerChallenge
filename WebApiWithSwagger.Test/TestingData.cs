using System;
using System.Collections.Generic;
using System.Text;
using WebApiWithSwagger.Models;

namespace WebApiWithSwagger.Test
{
    public static class TestingData
    {
        public static IList<CarStock> GetCarStock()
        {
            var cars = new List<CarStock>();
            cars.Add(new CarStock
            {
                Make = "AUDI",
                Model = "RS5",
                Year = 2021,
                DealerCode = "A01",
                StockLevel = 1
            });
            cars.Add(new CarStock
            {
                Make = "AUDI",
                Model = "RS7",
                Year = 2021,
                DealerCode = "A01",
                StockLevel = 2
            });

            cars.Add(new CarStock
            {
                Make = "BMW",
                Model = "M3",
                Year = 2020,
                DealerCode = "A01",
                StockLevel = 5
            });
            cars.Add(new CarStock
            {
                Make = "BMW",
                Model = "M3",
                Year = 2020,
                DealerCode = "B02",
                StockLevel = 2
            });
            cars.Add(new CarStock
            {
                Make = "BMW",
                Model = "330i",
                Year = 2015,
                DealerCode = "B02",
                StockLevel = 1
            });
            cars.Add(new CarStock
            {
                Make = "BMW",
                Model = "330i",
                Year = 2018,
                DealerCode = "B02",
                StockLevel = 2
            });
            cars.Add(new CarStock
            {
                Make = "AUDI",
                Model = "A7",
                Year = 1999,
                DealerCode = "B02",
                StockLevel = 10
            });

            return cars;
        }
    }
}
