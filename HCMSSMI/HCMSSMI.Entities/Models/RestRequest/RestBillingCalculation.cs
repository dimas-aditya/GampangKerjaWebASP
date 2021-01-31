using System;

namespace HCMSSMI.Entities.Models.RestRequest
{
    public class RestBillingCalculation
    {
        public int ? ID { get; set; }
        public int? ClaimDetailID { get; set; }
        public decimal? AmountIncurred { get; set; }
        public decimal? AmountApproved { get; set; }
        public decimal? AmountExcess { get; set; }
        public string ClaimDetailRemark { get; set; }
        public int? Qty { get; set; }
        public int? Version { get; set; } = 0;
        public int? BenefitID { get; set; }
        public int? PlanID { get; set; }
        public int? ClaimID { get; set; }
        public int? CaseID { get; set; }
        public int? BeneficiaryID { get; set; }
        public int? BenefitItemID { get; set; }
        public decimal CurrentLimit { get; set; }
        public string ActivityDate { get; set; }
        public string CreateBy { get; set; }
        public string CreateDate { get; set; } = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss");
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; } = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss");
    }
}
