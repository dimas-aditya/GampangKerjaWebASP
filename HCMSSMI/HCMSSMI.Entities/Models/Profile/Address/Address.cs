using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMSSMI.Entities.Models.Profile.Address
{
    public class Address
    {
        public int AddID { get; set; }
        public string Country { get; set; }
        public string Provinsi { get; set; }
        public string City { get; set; }
    }
}
