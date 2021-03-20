namespace WebApiWithSwagger.Models
{
    public class CarRequest
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string DealerCode { get; set; }
    }
}