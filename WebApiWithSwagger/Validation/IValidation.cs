using WebApiWithSwagger.Models;

namespace WebApiWithSwagger.Validation
{
    public interface IValidation
    {
        bool ValidateCarRequest(CarRequest request);
        bool ValidateStringParameter(string item);
        bool ValidateIntParameter(int item);
    }
}
