using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiWithSwagger.Models;

namespace WebApiWithSwagger.EFCore
{
    public class ApiContext : DbContext
    {
        public virtual DbSet<CarStock> Cars { get; set; }

        public ApiContext(DbContextOptions options) : base(options)
        {
        }

        public virtual List<CarStock> GetCars()
        {
            return Cars.ToList();
        }
        public virtual bool AddCar(NewCarRequest request)
        {
            var existingCarStock = this.GetSingleStock<NewCarRequest>(request);
            if (existingCarStock != null)
            {
                existingCarStock.StockLevel++;
            }
            else
            {
                Cars.Add(new CarStock() { Make = request.Make.ToUpper(), Model = request.Model.ToUpper(), Year = request.Year, DealerCode = request.DealerCode.ToUpper(), StockLevel = 1 });
            }
            this.SaveChanges();
            return true;
        }
        public virtual bool RemoveCar(RemoveCarRequest request)
        {
            var carToRemove = this.GetSingleStock<RemoveCarRequest>(request);
            if (carToRemove != null && carToRemove.StockLevel > 0)
            {
                carToRemove.StockLevel--;
                this.SaveChanges();
                return true;
            }
            return false;
        }
        public virtual CarStock UpdateStock(UpdateStockRequest request)
        {
            var carStock = Cars.SingleOrDefault(r => r.DealerCode.Equals(request.DealerCode, StringComparison.InvariantCultureIgnoreCase) && r.Id == request.StockId);
            if (carStock != null)
            {
                carStock.StockLevel = request.StockLevel;
                this.SaveChanges();
                return carStock;
            }
            return null;
        }
        private CarStock GetSingleStock<TRequest>(TRequest request) where TRequest : CarRequest
        {
            var existingCarStock = Cars.SingleOrDefault(r => r.DealerCode.Equals(request.DealerCode, StringComparison.InvariantCultureIgnoreCase) && r.Make.Equals(request.Make, StringComparison.InvariantCultureIgnoreCase)
                && r.Model.Equals(request.Model, StringComparison.InvariantCultureIgnoreCase) && r.Year == request.Year);
            return existingCarStock;
        }

    }
}
