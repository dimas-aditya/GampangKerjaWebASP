namespace HCMSSMI.Entities.Models.RestRequest
{
    public class RestBillingItem
    {
        public int ? BeneficiaryID { get; set; }
        public int ? PlanID { get; set; }
        public int? ClaimID { get; set; }
        public int ? BenefitID { get; set; }
        public int ? CaseID { get; set; }
        public string ClientBenefit { get; set; }
    }
}
