using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCMSSMI.ViewModels
{
    public class SearchMember
    {
        public string PayerCode { get; set; }
        public string CorporateCode { get; set; }
        public string PolicyNumber { get; set; }
        public string CardNo { get; set; }
        public string MemberNo { get; set; }
        public string MemberName { get; set; }
    }
}