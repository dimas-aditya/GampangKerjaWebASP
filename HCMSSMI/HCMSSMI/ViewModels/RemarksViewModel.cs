using System.Collections.Generic;
using System.Web.Mvc;

namespace HCMSSMI.ViewModels
{
    public class RemarksViewModel
    {
        public IEnumerable<string> SelectedRemarks { get; set; }
        public IEnumerable<SelectListItem> Remarks { get; set; }
    }
}