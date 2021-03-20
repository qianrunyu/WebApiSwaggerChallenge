using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWithSwagger.Models;

namespace WebApiWithSwagger.EFCore
{
    public class GenerateInitialData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApiContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApiContext>>()))
            {
                // Look for any board games.
                if (context.Cars.Any())
                {
                    return;   // Data was already seeded
                }
                context.Cars.AddRange(
                    new CarStock
                    {
                        Make = "AUDI",
                        Model = "RS5",
                        Year = 2021,
                        DealerCode = "A01",
                        StockLevel = 1
                    },
                    new CarStock
                    {
                        Make = "AUDI",
                        Model = "RS7",
                        Year = 2021,
                        DealerCode = "A01",
                        StockLevel = 2
                    },
                    new CarStock
                    {
                        Make = "BMW",
                        Model = "M3",
                        Year = 2020,
                        DealerCode = "A01",
                        StockLevel = 5
                    },
                    new CarStock
                    {
                        Make = "BMW",
                        Model = "M3",
                        Year = 2020,
                        DealerCode = "B02",
                        StockLevel = 2
                    },
                    new CarStock
                    {
                        Make = "BMW",
                        Model = "330i",
                        Year = 2015,
                        DealerCode = "B02",
                        StockLevel = 1
                    },
                    new CarStock
                    {
                        Make = "BMW",
                        Model = "330i",
                        Year = 2018,
                        DealerCode = "B02",
                        StockLevel = 2
                    },
                     new CarStock
                     {
                         Make = "AUDI",
                         Model = "A7",
                         Year = 1999,
                         DealerCode = "B02",
                         StockLevel = 10
                     });

                context.SaveChanges();
            }
        }
    }
}
