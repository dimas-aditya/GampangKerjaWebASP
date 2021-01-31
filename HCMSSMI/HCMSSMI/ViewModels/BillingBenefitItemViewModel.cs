using Entities.Models;
using HCMSSMI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCMSSMI.ViewModels
{
    public class BillingBenefitItemViewModel
    {
        //    public int BenefitItemID { get; set; }
        //    public string BenefitItemCode { get; set; }
        //    public string BenefitItemName { get; set; }


        public int? PlanID { get; set; }
        public int? ClaimReasonCodeID { get; set; }

        public int? ClaimID { get; set; }
        public int ? ClaimDetailID { get; set; }
        public decimal ClaimAmountIncurred { get; set; }
        public decimal ClaimAmountCovered { get; set; }
        public decimal ClaimExcess { get; set; }
        public string ClaimRemarks { get; set; }
        public int? ClaimVersion { get; set; }


    }
}