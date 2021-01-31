using System;

namespace HCMSSMI.Entities.Models.RestRequest
{
    public class SearchBatchClaimAnalystRequest
    {
        public int ? ProviderID { get; set; }
        public string ClaimID { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string BatchNo { get; set; }
        public int? ClaimStatusID { get; set; }
        public bool ? isSuccess { get; set; }
    }
}
