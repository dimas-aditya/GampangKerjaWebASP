using System;

namespace HCMSSMI.Entities.Models.Login
{
    public class Users
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool Status { get; set; }
        public string FullName { get; set; }
        public int RoleID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public Roles Roles { get; set; }

    }
}
