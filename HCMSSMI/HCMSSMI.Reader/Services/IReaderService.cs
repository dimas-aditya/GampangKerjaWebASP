using HCMSSMI.Entities.Models;
using HCMSSMI.Entities.Models.Candidate;
using HCMSSMI.Entities.Models.Login;
using HCMSSMI.Entities.Models.Profile;
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


        #endregion

        #region Candidate 

        Task<IEnumerable<Profile>> SearchCandidatePublic(SearchCandidate cases, string clientKey = null, string apiKey = null);
        Task<IEnumerable<Profile>> SearchCandidate(SearchCandidate cases, string clientKey = null, string apiKey = null);

        #endregion
    }
}
