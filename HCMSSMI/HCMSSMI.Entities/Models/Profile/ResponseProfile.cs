using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMSSMI.Entities.Models.Profile
{
   public class ResponseProfile
    {
        public DateTime ResponseAt { get; set; }
        public DateTime RequestAt { get; set; } = DateTime.Now;
        public string Message { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "Not Available";
        public bool IsSuccess { get; set; } = false;
        public Profile Data { get; set; }
    }
}
