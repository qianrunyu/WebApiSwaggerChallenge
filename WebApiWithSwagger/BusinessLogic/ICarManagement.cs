using System.Collections.Generic;
using WebApiWithSwagger.Models;

namespace WebApiWithSwagger.BusinessLogic
{
    public interface ICarManagement
    {
        CarStockLevelResponse GetAllCarsStock(IList<CarStock> cars, string dealercode);
        SearchCarResponse SearchStock(IList<CarStock> cars, string dealercode, string make, string model);
    }
}
