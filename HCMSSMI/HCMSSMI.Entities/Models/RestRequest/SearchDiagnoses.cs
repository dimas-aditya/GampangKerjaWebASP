using System;

namespace HCMSSMI.Entities.Models.RestRequest
{
    public class SearchDiagnoses
    {
        public int? ID { get; set; } = 0;
        public string IcdChapter { get; set; }
        public string IcdCode { get; set; }
        public string IcdBlock { get; set; }
        public string IcdDescription { get; set; }
        public int? CaseMonitoringID { get; set; }
        public int? CaseID { get; set; }
        public string CreateBy { get; set; }
        public string CreateDate { get; set; } = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss");
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; } = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss");
    }
}
