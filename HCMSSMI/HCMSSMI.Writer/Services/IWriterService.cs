using Entities.Models.RestRequest;
using HCMSSMI.Entities.Models;
using HCMSSMI.Entities.Models.Login;
using HCMSSMI.Entities.Models.Profile;
using HCMSSMI.Entities.Models.RestRequest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCMSSMI.Writer
{
    public interface IWriterService
    {
        #region Users Authentification

        Task<RestResponse<Users>> UsersAuthentificationLogin(string username, string password, string clientKey = null, string apiKey = null);
        Task<RestResponse<Users>> UsersAuthentificationRegister(Users user, string clientKey = null, string apiKey = null);

        #endregion


        #region

        Task<Tuple<Profile, bool>> UpdateProfile(Profile profile, string clientKey = null, string apiKey = null);


        #endregion

    }
}
