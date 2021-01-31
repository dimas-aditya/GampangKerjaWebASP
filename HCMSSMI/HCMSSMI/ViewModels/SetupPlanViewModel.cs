using HCMSSMI.Entities.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HCMSSMI.ViewModels
{
    public class SetupPlanViewModel
    {
        public int ID { get; set; }
        public string PayerName { get; set; }
        public int PolicyNumber { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string PlanName { get; set; }
        public int PlanCode { get; set; }
        public Status PlanStatus { get; set; }

    }
}