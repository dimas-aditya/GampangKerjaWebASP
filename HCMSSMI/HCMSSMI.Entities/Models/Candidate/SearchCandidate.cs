using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMSSMI.Entities.Models.Candidate
{
    public class SearchCandidate
    {
        public string Username { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Experience { get; set; }
        public string Skills { get; set; }
    }
}
