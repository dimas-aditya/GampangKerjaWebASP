using HCMSSMI.Entities.Models;

namespace HCMSSMI.ViewModels
{
    public class PolicyViewModel
    {
        public int? ID { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyHolder { get; set; }
        public string PolicyInformation { get; set; }
        public string PaymentDestination { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Status PlanStatus { get; set; }
        
    }
}