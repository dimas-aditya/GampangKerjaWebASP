using Entities.Models.RestRequest;
using HCMSSMI.Entities.Models;
using HCMSSMI.Entities.Models.Login;
using HCMSSMI.Entities.Models.Profile;
using HCMSSMI.Entities.Models.RestRequest;
using HCMSSMI.Extensions.Hashing;
using HCMSSMI.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HCMSSMI.Writer
{
    public class WriterService : IWriterService
    {
        #region Private Member

        private readonly WriterConfiguration configuration;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="configuration">A single instance parameter from <see cref="WriterConfiguration"/></param>
        public WriterService(WriterConfiguration configuration) => this.configuration = configuration;

        #endregion


        #region Users Authentification

        public async Task<Entities.Models.RestRequest.RestResponse<Users>> UsersAuthentificationLogin(string username, string password, string clientKey = null, string apiKey = null)
        {
            try
            {
                // Client URL
                configuration.ClientURL = @ServerApi.URL_AuthGateway;

                // Add authorization key if exist
                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);


                configuration.RequestURL = $"api/v1/UsersAuthentification/Login?username={username}&password={password}";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.POST, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var response = await configuration.Client.ExecuteAsync<Entities.Models.RestRequest.RestResponse<Users>>(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                return response.Data;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }

        public async Task<Entities.Models.RestRequest.RestResponse<Users>> UsersAuthentificationRegister(Users user, string clientKey = null, string apiKey = null)
        {
            try
            {
                // Client URL
                configuration.ClientURL = @ServerApi.URL_AuthGateway;

                // Add authorization key if exist
                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);


                configuration.RequestURL = $"api/v1/UsersAuthentification/Registration";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.POST, DataFormat.Json);
                configuration.JsonObject = new JObject
                {
                    { "username", user.Username },
                    { "password", user.Password },
                    { "fullName", user.FullName },
                    { "roleID", user.RoleID },
                    { "email", user.Email }
                };

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = await configuration.Client.ExecuteAsync<Entities.Models.RestRequest.RestResponse<Users>>(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                return response.Data;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }

        #endregion

        #region Profile

        public Task<Tuple<Profile, bool>> UpdateProfile(Profile profile, string clientKey = null, string apiKey = null)
        {
            try
            {
                // Client URL
                configuration.ClientURL = @ServerApi.URL_AuthGateway;

                // Add authorization key if exist
                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = @"Profile";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.PUT, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var collection = new Profile()
                {
                    Email = profile.Email,
                    FullName = profile.FullName,
                    ProfileID = profile.ProfileID,
                    RoleID = profile.RoleID.ToString(),
                    Username = profile.Username,
                    Profession = profile.Profession,
                    DOB = profile.DOB,
                    Gender = profile.Gender,
                    Phone = profile.Phone,
                    CoverImage = profile.CoverImage,
                    ProfileImage = profile.ProfileImage,
                    Experience = profile.Experience,
                    Jabatan = profile.Jabatan,
                    Qualification = profile.Qualification,
                    Type = profile.Type,
                    SalaryRange = profile.SalaryRange,
                    Setyourprofile = profile.Setyourprofile,
                    AboutSelf = profile.AboutSelf,
                    JobTitle = profile.JobTitle,
                    SectorID = profile.SectorID,
                    AddID = profile.AddID,
                    PostalCode = profile.PostalCode,
                    FullAddress = profile.FullAddress,
                    IsActive = profile.IsActive,
                    CreateDate = "0001-01-01T00:00:00",

                };
                configuration.Request.AddJsonBody(collection);

                var response = configuration.Client.Execute(configuration.Request);

                //------------------------------------------------------- 

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                var profileResult = new Profile()
                {
                    Email = profile.Email,
                    FullName = profile.FullName,
                    ProfileID = profile.ProfileID,
                    Username = profile.Username,
                    Profession = profile.Profession,
                    DOB = profile.DOB,
                    Gender = profile.Gender,
                    Phone = profile.Phone,
                    CoverImage = profile.CoverImage,
                    ProfileImage = profile.ProfileImage,
                    Experience = profile.Experience,
                    Jabatan = profile.Jabatan,
                    Qualification = profile.Qualification,
                    Type = profile.Type,
                    SalaryRange = profile.SalaryRange,
                    Setyourprofile = profile.Setyourprofile,
                    AboutSelf = profile.AboutSelf,
                    IsActive = profile.IsActive,
                };

                return Task.FromResult(Tuple.Create(profileResult, true));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }


        #endregion

    }
}
