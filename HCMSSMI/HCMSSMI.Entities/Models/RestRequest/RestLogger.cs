namespace HCMSSMI.Entities.Models.RestRequest
{
    public class RestLogger
    {
        public string Message { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "Not Available";
        public bool IsSuccess { get; set; } = false;
        public RestBillingCalculation RestBillingCalculation { get; set; }
    }
}
