using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMSSMI.Entities.Models.RestRequest
{
    public class RestResponse<T>
    {
        public string Message { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "Not Available";
        public bool IsSuccess { get; set; } = false;
        public DateTime ResponseAt { get; set; }
        public List<T> Data { get; set; }
    }
}
