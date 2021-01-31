using HCMSSMI.Entities.Models.Login;
using System.Threading.Tasks;

namespace HCMSSMI
{
    public interface IOAuth
    {
        Task<Users> UserIdentityAsync();
    }
}
