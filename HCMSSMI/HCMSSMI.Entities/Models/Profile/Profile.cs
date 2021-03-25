using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMSSMI.Entities.Models.Profile
{
    public class Profile
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string RoleID { get; set; }
        public string StatusVerify { get; set; }
        public string ProfileID { get; set; }
        public string Username { get; set; }
        public string Profession { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string CoverImage { get; set; }
        public string ProfileImage { get; set; }
        public string Experience { get; set; }
        public string Jabatan { get; set; }
        public string Qualification { get; set; }
        public string Type { get; set; }
        public string SalaryRange { get; set; }
        public string Setyourprofile { get; set; }
        public string AboutSelf { get; set; }
        public string JobTitle { get; set; }
        public string SectorID { get; set; }
        public string AddID { get; set; }
        public string PostalCode { get; set; }
        public string FullAddress { get; set; }
        public string CompanyName { get; set; } //for Employee
        public string CreateDate { get; set; }
        public string IsActive { get; set; }
    }
}
