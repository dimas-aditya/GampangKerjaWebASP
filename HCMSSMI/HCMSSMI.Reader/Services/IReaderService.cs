using HCMSSMI.Entities.Models;
using HCMSSMI.Entities.Models.Candidate;
using HCMSSMI.Entities.Models.Employee;
using HCMSSMI.Entities.Models.Login;
using HCMSSMI.Entities.Models.Profile;
using HCMSSMI.Entities.Models.Profile.Address;
using HCMSSMI.Entities.Models.Profile.Sector;
using HCMSSMI.Entities.Models.RestRequest;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCMSSMI.Reader
{
    /// <summary>
    /// Provides method for REST Request to API.
    /// </summary>
    public interface IReaderService
    {
        #region Users Authentification

        Task<RestResponse<Users>> GetUserAccountDetail(int id, string clientKey = null, string apiKey = null);
        Task<RestResponse<Users>> GetUserAccountDetail(string username, string clientKey = null, string apiKey = null);
        Task<IEnumerable<Roles>> GetUserRoles(string clientKey = null, string apiKey = null);

        #endregion


        #region profile

        Task<RestResponse<Profile>> SearchProfileIndex(string userName, string clientKey = null, string apiKey = null);

        Task<IEnumerable<Address>> SearchAddress(SearchAddress Searchaddress, string clientKey = null, string apiKey = null);
        Task<IEnumerable<Sector>> SearchSector(SearchSector searchSector, string clientKey = null, string apiKey = null);

        #endregion

        #region Candidate 

        Task<IEnumerable<Profile>> SearchCandidatePublic(SearchCandidate searchCandidate, string clientKey = null, string apiKey = null);
        Task<IEnumerable<Profile>> SearchCandidate(SearchCandidate searchCandidate, string clientKey = null, string apiKey = null);

        #endregion

        #region Candidate 

        Task<IEnumerable<Profile>> SearchEmployeePublic(SearchEmployee searchEmployee, string clientKey = null, string apiKey = null);
        Task<IEnumerable<Profile>> SearchEmployee(SearchEmployee searchEmployee, string clientKey = null, string apiKey = null);

        #endregion
    }
}
