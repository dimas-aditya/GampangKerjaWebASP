using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMSSMI.Entities.Models.RestRequest
{
    public class RestCaseAssessment
    {
        public int ID { get; set; }

        public int CaseId { get; set; }

        public string Symptom { get; set; }

        public DateTime SymptomDate { get; set; }

        public Double Temperature { get; set; }

        public int Pulse { get; set; }

        public int Respiratory { get; set; }

        public int PressureSis { get; set; }

        public int PressureDis { get; set; }

        public string TherapyPlan { get; set; }

        public int MonitoringCategoryid { get; set; }

        public DateTime MonitoringDate { get; set; }

        public string MonitoringNote { get; set; }

        public string GeneralCondition { get; set; }

        public string HospitalizeIndication { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

        public string UpdateBy { get; set; }

        public DateTime UpdateDate { get; set; }

        public Boolean IsDeleted { get; set; }

    }
}
