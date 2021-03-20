namespace WebApiWithSwagger.Models
{
    public class RemoveCarRequest : CarRequest
    {
        //optional
        public string Reason { get; set; }
    }
}