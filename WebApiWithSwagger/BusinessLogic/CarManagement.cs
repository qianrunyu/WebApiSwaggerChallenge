using System;
using System.Collections.Generic;
using System.Linq;
using WebApiWithSwagger.Models;

namespace WebApiWithSwagger.BusinessLogic
{
    public class CarManagement : ICarManagement
    {
        public CarStockLevelResponse GetAllCarsStock(IList<CarStock> cars, string dealercode)
        {
            var groupedCarStocks = cars.Where(c => c.DealerCode.Equals(dealercode, StringComparison.InvariantCultureIgnoreCase)).ToArray();
            return new CarStockLevelResponse() { CarStocks = groupedCarStocks };
        }

        public SearchCarResponse SearchStock(IList<CarStock> cars, string dealercode, string make, string model)
        {
            var carStock = this.GetAllCarsStock(cars, dealercode);
            var searchResult = carStock.CarStocks.Where(x => x.Make.Equals(make, StringComparison.InvariantCultureIgnoreCase) && x.Model.Equals(model, StringComparison.InvariantCultureIgnoreCase)).ToArray();
            return new SearchCarResponse() { CarStocks = searchResult };
        }
    }
}
