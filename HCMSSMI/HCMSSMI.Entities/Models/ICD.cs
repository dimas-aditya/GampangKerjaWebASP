using System;

namespace HCMSSMI.Entities.Models
{
    public class ICD
    {
        public int? ID { get; set; }
        public string IcdChapter { get; set; }
        public string PreDiagnoseCode1 { get; set; }
        public string PreDiagnose1 { get; set; }
        public string IcdCode { get; set; }
        public string IcdCode1 { get; set; }
        public string IcdCode2 { get; set; }
        public string IcdCode3 { get; set; }
        public string IcdCode4 { get; set; }
        public string IcdCode5 { get; set; }
        public string IcdBlock { get; set; }
        public string IcdDescription { get; set; }
        public string Diagnose1 { get; set; }
        public string Diagnose2 { get; set; }
        public string Diagnose3 { get; set; }
        public string Diagnose4 { get; set; }
        public string Diagnose5 { get; set; }
        public string CreateBy { get; set; }
        public string CreateDate { get; set; } = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss");
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; } = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss");
    }
}
