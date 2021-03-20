using WebApiWithSwagger.Models;

namespace WebApiWithSwagger.Validation
{
    public class MyValidation : IValidation
    {
        public bool ValidateCarRequest(CarRequest request)
        {
            if (!ValidateStringParameter(request.Make) || !ValidateStringParameter(request.Model) || !ValidateStringParameter(request.DealerCode))
            {
                return false;
            }
            if (!ValidateIntParameter(request.Year))
            {
                return false;
            }
            return true;
        }
        public bool ValidateStringParameter(string item)
        {
            return (item == null || item.Trim().Length == 0) ? false : true;
        }
        public bool ValidateIntParameter(int item)
        {
            return item < 0 ? false : true;
        }
    }
}
