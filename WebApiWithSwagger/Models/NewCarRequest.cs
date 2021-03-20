namespace WebApiWithSwagger.Models
{
    public class NewCarRequest : CarRequest
    {
        //Optional
        public string CarSource { get; set; }
    }
}