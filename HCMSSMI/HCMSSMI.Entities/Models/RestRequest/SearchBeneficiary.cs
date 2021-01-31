using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMSSMI.Entities.Models.RestRequest
{
   public class SearchBeneficiary
    {
        public int? PayerID { get; set; } = 0;
        public int? CorporateID { get; set; } = 0;
        public int? PolicyNumber { get; set; } = 0;
        public string CardNo { get; set; }
        public string MemberNo { get; set; }
        public string MemberName { get; set; }
    }
}
