using HCMSSMI.Entities.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        #region Policy

        /// <summary>
        /// Adds policy data item.
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="clientKey"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public async Task<Tuple<Policy, bool>> AddPolicy(List<Policy> policy, string clientKey = null, string apiKey = null)
        {
            try
            {
                // Client URL
                configuration.ClientURL = @"http://localhost:5000/";

                // Add authorization key if exist
                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = @"api/v1/Policy/Create";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.POST, DataFormat.Json);

                configuration.JsonObject = new JObject();

                var requestTime = DateTime.Now.ToString("yyyy-MM-ddThh:ss:mm");

                var collection = new Policy();

                foreach (var item in policy)
                {
                    collection = new Policy()
                    {
                        PolicyNo = item.PolicyNo,
                        PolicyHolder = item.PolicyHolder,
                        PolicyInformation = item.PolicyInformation,
                        NewPolicyID = 0,
                        Corporate = new Corporate()
                        {
                            ID = item.Corporate.ID,
                        },
                        Status = new Status()
                        {
                            ID = item.Status.ID
                        },
                        PolicyLimit = new PolicyLimit()
                        {
                            ID = item.PolicyLimit.ID,
                        },
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        SummaryTc = item.SummaryTc,
                        PathFileTc = item.PathFileTc,
                        CreateBy = item.CreateBy,
                        UpdateBy = item.UpdateBy

                    };


                    configuration.JsonObject.Add("policyNo", item.PolicyNo);
                    configuration.JsonObject.Add("policyHolder", item.PolicyHolder);
                    configuration.JsonObject.Add("policyInformation", item.PolicyInformation);
                    // Set the default value to zero, because we've not implemented 
                    configuration.JsonObject.Add("newPolicyID", 0);
                    configuration.JsonObject.Add("subPolicyNo", item.SubPolicyNo);
                    configuration.JsonObject.Add("corporateID", item.Corporate.ID);
                    configuration.JsonObject.Add("statusID", item.Status.ID);
                    // Set the default value to zero, because we've not implemented 
                    configuration.JsonObject.Add("policyLimitID", item.PolicyLimit.ID);
                    configuration.JsonObject.Add("startDate", requestTime);
                    configuration.JsonObject.Add("endDate", requestTime);
                    configuration.JsonObject.Add("summaryTc", item.SummaryTc);
                    configuration.JsonObject.Add("pathFileTc", item.PathFileTc);
                    configuration.JsonObject.Add("createBy", item.CreateBy);
                    configuration.JsonObject.Add("createDate", requestTime);
                    configuration.JsonObject.Add("updateBy", item.UpdateBy);
                    configuration.JsonObject.Add("updateDate", requestTime);

                    // Attention for this object: Make sure this value is always false.
                    // just follow the service. (Hari)
                    configuration.JsonObject.Add("isDeleted", false);
                }

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);
                    
                var response = await configuration.Client.ExecuteAsync(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));


                return Tuple.Create(collection, true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }

        }

        /// <summary>
        /// Updates specific policy data item by using ID.
        /// </summary>
        /// <param name="id">The identifier parameter</param>
        /// <param name="policy">An instance object Policy</param>
        /// <param name="clientKey">The client key parameter</param>
        /// <param name="apiKey">The api key paramater</param>
        /// <returns></returns>
        public async Task<Tuple<Policy, bool>> UpdatePolicy(int id, Policy policy, string clientKey = null, string apiKey = null)
        {
            try
            {
                // Client URL
                configuration.ClientURL = @"http://localhost:5000/";

                // Add authorization key if exist
                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = @"api/v1/Policy/Update";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.PUT, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var requestTime = DateTime.Now.ToString("yyyy-MM-ddThh:ss:mm");

                configuration.JsonObject.Add("id", id);
                configuration.JsonObject.Add("policyNo", policy.PolicyNo);
                configuration.JsonObject.Add("policyHolder", policy.PolicyHolder);
                configuration.JsonObject.Add("policyInformation", policy.PolicyInformation);
                // Set the default value to zero, because we've not implemented 
                configuration.JsonObject.Add("newPolicyID", 0);
                configuration.JsonObject.Add("subPolicyNo", policy.SubPolicyNo);
                configuration.JsonObject.Add("corporateID", policy.Corporate.ID);
                configuration.JsonObject.Add("statusID", policy.Status.ID);
                // Set the default value to zero, because we've not implemented  
                configuration.JsonObject.Add("policyLimitID", policy.PolicyLimit.ID);
                configuration.JsonObject.Add("startDate", requestTime);
                configuration.JsonObject.Add("endDate", requestTime);
                configuration.JsonObject.Add("summaryTc", policy.SummaryTc);
                configuration.JsonObject.Add("pathFileTc", policy.PathFileTc);
                configuration.JsonObject.Add("createBy", policy.CreateBy);
                configuration.JsonObject.Add("createDate", requestTime);
                configuration.JsonObject.Add("updateBy", policy.UpdateBy);
                configuration.JsonObject.Add("updateDate", requestTime);

                // Attention for this object: Make sure this value is always false.
                // just follow the service. (Hari)
                configuration.JsonObject.Add("isDeleted", false);



        configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Put(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));


                return await Task.FromResult(Tuple.Create(policy, true));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }

        /// <summary>
        /// Delete specific policy data item by using ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clientKey"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public async Task<bool> DeletePolicy(int id, string clientKey = null, string apiKey = null)
        {
            try
            {
                configuration.ClientURL = @"http://localhost:5000/";

                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = $"api/v1/Policy/Delete/{id}";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.DELETE, DataFormat.Json);

                var response = await configuration.Client.ExecuteAsync(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                return true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }




        #endregion

        #region Benefit

        public async Task<Tuple<BenefitMapping, bool>> AddBenefitMapping(List<BenefitMapping> benefit, string clientKey = null, string apiKey = null)
        {
            try
            {
                // Client URL
                configuration.ClientURL = @"http://localhost:44301/";

                // Add authorization key if exist
                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = @"api/v1/Benefit/BenefitMapping";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.POST, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var requestTime = DateTime.Now;

                var collection = new BenefitMapping();

                foreach(var item in benefit)
                {
                    collection = new BenefitMapping()
                    {
                        Code = item.Code,
                        Name = item.Name,
                        PlanMasterID = item.PlanMasterID,
                        BenefitMasterID = item.BenefitMasterID,
                        StatusID = item.StatusID,
                        PlanConsumption = 0,
                        PolicyLimitConsumption = 0,
                        UpdateBy = item.UpdateBy,
                        UpdateDate = item.UpdateDate,
                        CreateBy = item.CreateBy,
                        CreateDate = item.CreateDate,
                        IsDeleted = false,
                        BenefitItemOptions = new BenefitItemOptions()
                        {
                            IsBiayaAdmin = item.BenefitItemOptions.IsBiayaAdmin,
                            IsBiayaAlat = item.BenefitItemOptions.IsBiayaAlat,
                            IsAmbulance = item.BenefitItemOptions.IsAmbulance,
                            IsDiagnostikLab = item.BenefitItemOptions.IsDiagnostikLab,
                            IsDiagnosticRad = item.BenefitItemOptions.IsDiagnosticRad,
                            IsDiagnosticRadGigi = item.BenefitItemOptions.IsDiagnosticRadGigi,
                            IsDiagnosticRadKb = item.BenefitItemOptions.IsDiagnosticRadKb,
                            IsBiayaFisio = item.BenefitItemOptions.IsBiayaFisio,
                            IsJasaDokUmum = item.BenefitItemOptions.IsJasaDokUmum,
                            IsJasaBidan = item.BenefitItemOptions.IsJasaBidan,
                            IsJasaDokSpecialis = item.BenefitItemOptions.IsJasaDokSpecialis,
                            IsJasaDokSpecialisObgyn = item.BenefitItemOptions.IsJasaDokSpecialisObgyn,
                            IsJasaDokGigiAndSpecialis = item.BenefitItemOptions.IsJasaDokGigiAndSpecialis,
                            IsKamarPerawatanMakan = item.BenefitItemOptions.IsKamarPerawatanMakan,
                            IsKamarPerawatanKhusus = item.BenefitItemOptions.IsKamarPerawatanKhusus,
                            IsKamarBersalin = item.BenefitItemOptions.IsKamarBersalin,
                            IsKamarOperasi = item.BenefitItemOptions.IsKamarOperasi,
                        },
                        SubMedicalServiceOptions = new SubMedicalServiceOptions()
                        {
                            IsKonsulUmum = item.SubMedicalServiceOptions.IsKonsulUmum,
                            IsImunisasi = item.SubMedicalServiceOptions.IsImunisasi,
                            IsKB = item.SubMedicalServiceOptions.IsKB,
                            IsPascaRawatInap = item.SubMedicalServiceOptions.IsPascaRawatInap,
                            IsRawatInap = item.SubMedicalServiceOptions.IsRawatInap,
                            IsGigi = item.SubMedicalServiceOptions.IsGigi,
                            IsPerawatanKehamilan = item.SubMedicalServiceOptions.IsPerawatanKehamilan,
                            IsPascaMelahirkan = item.SubMedicalServiceOptions.IsPascaMelahirkan,
                            IsMelahirkan = item.SubMedicalServiceOptions.IsMelahirkan,
                            IsRawatJalan = item.SubMedicalServiceOptions.IsRawatJalan,
                            IsSantunan = item.SubMedicalServiceOptions.IsSantunan,
                            IsKacamata = item.SubMedicalServiceOptions.IsKacamata
                        },

                    };
                }


                configuration.Request.AddJsonBody(collection);

                var response = configuration.Client.Execute(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));


                return await Task.FromResult(Tuple.Create(collection, true));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }

        public async Task<Tuple<Benefit, bool>> UpdateBenefitMapping(int id, BenefitMapping benefit, string clientKey = null, string apiKey = null)
        {

            try
            {
                // Client URL
                configuration.ClientURL = @"http://localhost:44301/";

                // Add authorization key if exist
                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = @"api/v1/Benefit/BenefitMapping";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.PUT, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var requestTime = DateTime.Now;

                var collection = new BenefitMapping()
                {
                    ID = id,
                    Code = benefit.Code,
                    Name = benefit.Name,
                    PlanMasterID = benefit.PlanMasterID,
                    BenefitMasterID = benefit.BenefitMasterID,
                    StatusID = benefit.StatusID,
                    PlanConsumption = 0,
                    PolicyLimitConsumption = 0,
                    UpdateBy = benefit.UpdateBy,
                    UpdateDate = benefit.UpdateDate,
                    CreateBy = benefit.CreateBy,
                    CreateDate = benefit.CreateDate,
                    IsDeleted = false,
                    BenefitItemOptions = new BenefitItemOptions()
                    {
                        IsBiayaAdmin = benefit.BenefitItemOptions.IsBiayaAdmin,
                        IsBiayaAlat = benefit.BenefitItemOptions.IsBiayaAlat,
                        IsAmbulance = benefit.BenefitItemOptions.IsAmbulance,
                        IsDiagnostikLab = benefit.BenefitItemOptions.IsDiagnostikLab,
                        IsDiagnosticRad = benefit.BenefitItemOptions.IsDiagnosticRad,
                        IsDiagnosticRadGigi = benefit.BenefitItemOptions.IsDiagnosticRadGigi,
                        IsDiagnosticRadKb = benefit.BenefitItemOptions.IsDiagnosticRadKb,
                        IsBiayaFisio = benefit.BenefitItemOptions.IsBiayaFisio,
                        IsJasaDokUmum = benefit.BenefitItemOptions.IsJasaDokUmum,
                        IsJasaBidan = benefit.BenefitItemOptions.IsJasaBidan,
                        IsJasaDokSpecialis = benefit.BenefitItemOptions.IsJasaDokSpecialis,
                        IsJasaDokSpecialisObgyn = benefit.BenefitItemOptions.IsJasaDokSpecialisObgyn,
                        IsJasaDokGigiAndSpecialis = benefit.BenefitItemOptions.IsJasaDokGigiAndSpecialis,
                        IsKamarPerawatanMakan = benefit.BenefitItemOptions.IsKamarPerawatanMakan,
                        IsKamarPerawatanKhusus = benefit.BenefitItemOptions.IsKamarPerawatanKhusus,
                        IsKamarBersalin = benefit.BenefitItemOptions.IsKamarBersalin,
                        IsKamarOperasi = benefit.BenefitItemOptions.IsKamarOperasi,
                    },
                    SubMedicalServiceOptions = new SubMedicalServiceOptions()
                    {
                        IsKonsulUmum = benefit.SubMedicalServiceOptions.IsKonsulUmum,
                        IsImunisasi = benefit.SubMedicalServiceOptions.IsImunisasi,
                        IsKB = benefit.SubMedicalServiceOptions.IsKB,
                        IsPascaRawatInap = benefit.SubMedicalServiceOptions.IsPascaRawatInap,
                        IsRawatInap = benefit.SubMedicalServiceOptions.IsRawatInap,
                        IsGigi = benefit.SubMedicalServiceOptions.IsGigi,
                        IsPerawatanKehamilan = benefit.SubMedicalServiceOptions.IsPerawatanKehamilan,
                        IsPascaMelahirkan = benefit.SubMedicalServiceOptions.IsPascaMelahirkan,
                        IsMelahirkan = benefit.SubMedicalServiceOptions.IsMelahirkan,
                        IsRawatJalan = benefit.SubMedicalServiceOptions.IsRawatJalan,
                        IsSantunan = benefit.SubMedicalServiceOptions.IsSantunan,
                        IsKacamata = benefit.SubMedicalServiceOptions.IsKacamata
                    },


                };
                configuration.Request.AddJsonBody(collection);

                var response = configuration.Client.Execute(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));


                return await Task.FromResult(Tuple.Create(benefit, true));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }

        }

        public async Task<Tuple<Benefit, bool>> AddBenefit(List<Benefit> benefit, string clientKey = null, string apiKey = null)
        {
            try
            {
                // Client URL
                configuration.ClientURL = @"http://localhost:44301/";

                // Add authorization key if exist
                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = @"api/v1/Benefit/Benefit";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.POST, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var requestTime = DateTime.Now;

                foreach (var item in benefit)
                {
                    configuration.JsonObject.Add("planID", item.PlanMaster.ID);
                    configuration.JsonObject.Add("benefitMasterID", item.BenefitMasters.ID);
                    configuration.JsonObject.Add("statusActiveID", item.Status.ID);
                    configuration.JsonObject.Add("name", item.Name);
                    configuration.JsonObject.Add("code", item.Code);
                    configuration.JsonObject.Add("planConsumption", 0);
                    configuration.JsonObject.Add("policyLimitConsumption", 0);
                    configuration.JsonObject.Add("createDate", requestTime);
                    configuration.JsonObject.Add("createBy", item.CreateBy);

                    // Attention for this object: Make sure this value is always false.
                    // just follow the service. (Hari)
                    configuration.JsonObject.Add("isDeleted", false);

                }

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Execute(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                var collection = new Benefit();

                return await Task.FromResult(Tuple.Create(collection, true));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }

        public async Task<Tuple<Benefit, bool>> UpdateBenefit(int id, Benefit benefit, string clientKey = null, string apiKey = null)
        {
            try
            {
                configuration.ClientURL = @"http://localhost:44301/";

                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = @"api/v1/Benefit/Benefit";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.PUT, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var requestTime = DateTime.Now;
                configuration.JsonObject.Add("id", id);
                configuration.JsonObject.Add("planID", benefit.PlanMaster.ID);
                configuration.JsonObject.Add("benefitMasterID", benefit.BenefitMasters.ID);
                configuration.JsonObject.Add("statusActiveID", benefit.Status.ID);
                configuration.JsonObject.Add("name", benefit.Name);
                configuration.JsonObject.Add("code", benefit.Code);
                configuration.JsonObject.Add("planConsumption", 0);
                configuration.JsonObject.Add("policyLimitConsumption", 0);
                configuration.JsonObject.Add("updateBy", benefit.UpdateBy);
                configuration.JsonObject.Add("updateDate", requestTime);


                // Attention for this object: Make sure this value is always false.
                // just follow the service. (Hari)
                configuration.JsonObject.Add("isDeleted", false);

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Put(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                var collection = new Benefit();

                return await Task.FromResult(Tuple.Create(benefit, true));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }

        public async Task<bool> DeleteBenefit(int id, string clientKey = null, string apiKey = null)
        {
            try
            {
                configuration.ClientURL = @"http://localhost:44301/";

                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = $"api/v1/Benefit/Benefit/{id}";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.DELETE, DataFormat.Json);
                configuration.JsonObject = new JObject
                {
                    // its because service is not really delete pysicaly so 
                    // we have to follow the service like this n I dont know why so just follow the service (Hari)
                    //configuration.Request.AddParameter("isDeleted", true);
                    { "id", id }
                };

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Execute(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                return await Task.FromResult(true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }

        #endregion

        #region Benefit Master

        public async Task<Tuple<BenefitMaster, bool>> AddBenefitMaster(List<BenefitMaster> benefitMaster, string clientKey = null, string secretKey = null)
        {
            try
            {
                // Client URL
                configuration.ClientURL = @"http://localhost:44301/";

                // Add authorization key if exist
                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(secretKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, secretKey);

                configuration.RequestURL = @"BenefitMaster";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.POST, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var requestTime = DateTime.Now;

                foreach (var item in benefitMaster)
                {
                    configuration.JsonObject.Add("medicalServiceID", item.MedicalServiceID);
                    configuration.JsonObject.Add("name", item.Name);
                    configuration.JsonObject.Add("code", item.Code);
                    configuration.JsonObject.Add("description", item.Description);
                    //configuration.JsonObject.Add("createDate", item.CreateDate.ToDateTime() is null ? requesTime : item.CreateDate.ToDateTime());
                    configuration.JsonObject.Add("createDate", requestTime);
                    configuration.JsonObject.Add("createBy", item.CreateBy);

                    // Attention for this object: Make sure this value is always false.
                    // just follow the service. (Hari)
                    configuration.JsonObject.Add("isDeleted", false);

                }

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Execute(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                var collection = new BenefitMaster();

                return await Task.FromResult(Tuple.Create(collection, true));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }

        public async Task<Tuple<BenefitMaster, bool>> UpdateBenefitMaster(int id, BenefitMaster benefitMaster, string clientKey = null, string secretKey = null)
        {
            try
            {
                configuration.ClientURL = @"http://localhost:44301/";

                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(secretKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, secretKey);

                configuration.RequestURL = @"BenefitMaster";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.PUT, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var requestTime = DateTime.Now;
                configuration.JsonObject.Add("id", id);
                configuration.JsonObject.Add("medicalServiceID", benefitMaster.MedicalServiceID);
                configuration.JsonObject.Add("name", benefitMaster.Name);
                configuration.JsonObject.Add("code", benefitMaster.Code);
                configuration.JsonObject.Add("description", benefitMaster.Description);
                configuration.JsonObject.Add("updateBy", benefitMaster.UpdateBy);
                //configuration.JsonObject.Add("updateDate", benefit.UpdateDate.ToDateTime() is null ? requesTime : benefit.UpdateDate.ToDateTime());
                configuration.JsonObject.Add("updateDate", requestTime);


                // Attention for this object: Make sure this value is always false.
                // just follow the service. (Hari)
                configuration.JsonObject.Add("isDeleted", false);

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Put(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                var collection = new BenefitMaster();

                return await Task.FromResult(Tuple.Create(benefitMaster, true));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }

        public async Task<bool> DeleteBenefitMaster(int id, string clientKey = null, string secretKey = null)
        {
            try
            {
                configuration.ClientURL = @"http://localhost:44301/";

                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(secretKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, secretKey);

                configuration.RequestURL = $"BenefitMaster/{id}";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.DELETE, DataFormat.Json);
                configuration.JsonObject = new JObject
                {
                    // its because service is not really delete pysicaly so 
                    // we have to follow the service like this n I dont know why so just follow the service (Hari)
                    //configuration.Request.AddParameter("isDeleted", true);
                    { "id", id }
                };

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Execute(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                return await Task.FromResult(true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }

        #endregion

        #region Benefit Items

        public async Task<Tuple<BenefitItem, bool>> AddBenefitItem(List<BenefitItem> benefitItem, string clientKey = null, string secretKey = null)
        {
            try
            {
                // Client URL
                configuration.ClientURL = @"http://localhost:44301/";

                // Add authorization key if exist
                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(secretKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, secretKey);

                configuration.RequestURL = @"api/v1/BenefitItem/BenefitItem";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.POST, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var requestTime = DateTime.Now.ToString("yyyy-MM-ddThh:ss:mm");

                foreach (var item in benefitItem)
                {
                    configuration.JsonObject.Add("name", item.Name);
                    configuration.JsonObject.Add("code", item.Code);
                    configuration.JsonObject.Add("description", item.Description);
                    //configuration.JsonObject.Add("createDate", item.CreateDate.ToDateTime() is null ? requesTime : item.CreateDate.ToDateTime());
                    configuration.JsonObject.Add("createDate", requestTime);
                    configuration.JsonObject.Add("createBy", item.CreateBy);

                    // Attention for this object: Make sure this value is always false.
                    // just follow the service. (Hari)
                    configuration.JsonObject.Add("isDeleted", false);

                }

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Execute(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                var collection = new BenefitItem();

                return await Task.FromResult(Tuple.Create(collection, true));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }

        public async Task<Tuple<BenefitItem, bool>> UpdateBenefitItem(int id, BenefitItem benefitItem, string clientKey = null, string secretKey = null)
        {
            try
            {

                configuration.ClientURL = @"http://localhost:44301/";

                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(secretKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, secretKey);

                configuration.RequestURL = @"api/v1/BenefitItem/BenefitItem";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.PUT, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var requestTime = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss");
                configuration.JsonObject.Add("id", id);
                configuration.JsonObject.Add("name", benefitItem.Name);
                configuration.JsonObject.Add("code", benefitItem.Code);
                configuration.JsonObject.Add("description", benefitItem.Description);
                configuration.JsonObject.Add("updateBy", benefitItem.UpdateBy);
                //configuration.JsonObject.Add("updateDate", benefit.UpdateDate.ToDateTime() is null ? requesTime : benefit.UpdateDate.ToDateTime());
                configuration.JsonObject.Add("updateDate", requestTime);


                // Attention for this object: Make sure this value is always false.
                // just follow the service. (Hari)
                configuration.JsonObject.Add("isDeleted", false);

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Put(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                var collection = new BenefitItem();

                return await Task.FromResult(Tuple.Create(benefitItem, true));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }

        public async Task<bool> DeleteBenefitItem(int id, string clientKey = null, string secretKey = null)
        {
            try
            {
                configuration.ClientURL = @"http://localhost:44301/";

                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(secretKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, secretKey);

                configuration.RequestURL = $"api/v1/BenefitItem/BenefitItem/{id}";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.DELETE, DataFormat.Json);
                configuration.JsonObject = new JObject
                {
                    // its because service is not really delete pysicaly so 
                    // we have to follow the service like this n I dont know why so just follow the service (Hari)
                    //configuration.Request.AddParameter("isDeleted", true);
                    { "id", id }
                };

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Execute(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                return await Task.FromResult(true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }

        #endregion

        #region Benefit Limit Condition

        public async Task<Tuple<BenefitLimitCondition, bool>> AddBenefitLimitCondition(List<BenefitLimitCondition> benefitLimitCondition, string clientKey = null, string secretKey = null)
        {
            try
            {
                // Client URL
                configuration.ClientURL = @"http://localhost:44301/";

                // Add authorization key if exist
                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(secretKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, secretKey);

                configuration.RequestURL = @"api/v1/BenefitLimitCondition/BenefitLimitCondition";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.POST, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var requestTime = DateTime.Now;

                foreach (var item in benefitLimitCondition)
                {
                    configuration.JsonObject.Add("benefitID", item.Benefit.ID);
                    configuration.JsonObject.Add("conditionTypeID", item.ConditionType.ID);
                    configuration.JsonObject.Add("limitTypeID", item.LimitType.ID);
                    configuration.JsonObject.Add("value", item.Value);
                    configuration.JsonObject.Add("flag", item.Flag);
                    configuration.JsonObject.Add("createDate", requestTime);
                    configuration.JsonObject.Add("createBy", item.CreateBy);

                    // Attention for this object: Make sure this value is always false.
                    // just follow the service. (Hari)
                    configuration.JsonObject.Add("isDeleted", false);

                }

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Execute(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                var collection = new BenefitLimitCondition();

                return await Task.FromResult(Tuple.Create(collection, true));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }

        public async Task<Tuple<BenefitLimitCondition, bool>> UpdateBenefitLimitCondition(int id, BenefitLimitCondition benefitLimitCondition, string clientKey = null, string secretKey = null)
        {
            try
            {
                configuration.ClientURL = @"http://localhost:44301/";

                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(secretKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, secretKey);

                configuration.RequestURL = @"api/v1/BenefitLimitCondition/BenefitLimitCondition";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.PUT, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var requestTime = DateTime.Now;
                configuration.JsonObject.Add("id", id);
                configuration.JsonObject.Add("benefitID", benefitLimitCondition.Benefit.ID);
                configuration.JsonObject.Add("conditionTypeID", benefitLimitCondition.ConditionType.ID);
                configuration.JsonObject.Add("limitTypeID", benefitLimitCondition.LimitType.ID);
                configuration.JsonObject.Add("value", benefitLimitCondition.Value);
                configuration.JsonObject.Add("flag", benefitLimitCondition.Flag);
                configuration.JsonObject.Add("updateBy", benefitLimitCondition.UpdateBy);
                configuration.JsonObject.Add("updateDate", requestTime);


                // Attention for this object: Make sure this value is always false.
                // just follow the service. (Hari)
                configuration.JsonObject.Add("isDeleted", false);

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Put(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                var collection = new BenefitLimitCondition();



                return await Task.FromResult(Tuple.Create(benefitLimitCondition, true));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }


        public async Task<bool> DeleteBenefitLimitCondition(int id, string clientKey = null, string secretKey = null)
        {
            try
            {
                configuration.ClientURL = @"http://localhost:44301/";

                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(secretKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, secretKey);

                configuration.RequestURL = $"api/v1/BenefitLimitCondition/BenefitLimitCondition/{id}";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.DELETE, DataFormat.Json);
                configuration.JsonObject = new JObject
                {
                    // its because service is not really delete pysicaly so 
                    // we have to follow the service like this n I dont know why so just follow the service (Hari)
                    //configuration.Request.AddParameter("isDeleted", true);
                    { "id", id }
                };

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Execute(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                return await Task.FromResult(true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }

        #endregion

        #region PlanMaster Master

        public async Task<Tuple<PlanMaster, bool>> AddPlanMaster(List<PlanMaster> planMaster, string clientKey = null, string secretKey = null)
        {
            try
            {
                // Client URL
                configuration.ClientURL = @"http://localhost:44338/";

                // Add authorization key if exist
                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(secretKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, secretKey);

                configuration.RequestURL = @"api/v1/PlanMaster/PlanMaster";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.POST, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var requestTime = DateTime.Now;

                foreach (var item in planMaster)
                {

                    int isProrate = Convert.ToInt32(item.IsProrate);

                    configuration.JsonObject.Add("statusExcessClauseID", 0);
                    configuration.JsonObject.Add("planRoomCategoryID", 0);
                    configuration.JsonObject.Add("code", item.Code);
                    configuration.JsonObject.Add("name", item.Name);
                    configuration.JsonObject.Add("printedPlan", item.PrintedPlan);
                    configuration.JsonObject.Add("isProtate", isProrate);
                    configuration.JsonObject.Add("category", 0);
                    configuration.JsonObject.Add("createBy", item.CreateBy);
                    configuration.JsonObject.Add("createDate", requestTime);
                    configuration.JsonObject.Add("updateBy", item.UpdateBy);
                    configuration.JsonObject.Add("updateDate", requestTime);
                    configuration.JsonObject.Add("policyID", item.Policy.ID);
                    configuration.JsonObject.Add("medicalServiceID", item.MedicalService.ID);
                    configuration.JsonObject.Add("statusActiveID", item.Status.ID);


                    // Attention for this object: Make sure this value is always false.
                    // just follow the service. (Hari)
                    configuration.JsonObject.Add("isDeleted", false);

                }

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Execute(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                var collection = new PlanMaster();

                return await Task.FromResult(Tuple.Create(collection, true));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }
        public async Task<Tuple<PlanMaster, bool>> UpdatePlanMaster(int id, PlanMaster planMaster, string clientKey = null, string secretKey = null)
        {
            try
            {
                configuration.ClientURL = @"http://localhost:44338/";

                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(secretKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, secretKey);

                configuration.RequestURL = @"api/v1/PlanMaster/PlanMaster";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.PUT, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var requestTime = DateTime.Now;

                int isProrate = Convert.ToInt32(planMaster.IsProrate);

                configuration.JsonObject.Add("id", id);
                configuration.JsonObject.Add("statusExcessClauseID", 0);
                configuration.JsonObject.Add("planRoomCategoryID", 0);
                configuration.JsonObject.Add("code", planMaster.Code);
                configuration.JsonObject.Add("name", planMaster.Name);
                configuration.JsonObject.Add("printedPlan", planMaster.PrintedPlan);
                configuration.JsonObject.Add("isProtate", isProrate);
                configuration.JsonObject.Add("category", 0);
                configuration.JsonObject.Add("createBy", planMaster.CreateBy);
                configuration.JsonObject.Add("createDate", requestTime);
                configuration.JsonObject.Add("updateBy", planMaster.UpdateBy);
                configuration.JsonObject.Add("updateDate", requestTime);
                configuration.JsonObject.Add("policyID", planMaster.Policy.ID);
                configuration.JsonObject.Add("medicalServiceID", planMaster.MedicalService.ID);
                configuration.JsonObject.Add("statusActiveID", planMaster.Status.ID);


                // Attention for this object: Make sure this value is always false.
                // just follow the service. (Hari)
                configuration.JsonObject.Add("isDeleted", false);

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Put(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                var collection = new PlanMaster();



                return await Task.FromResult(Tuple.Create(planMaster, true));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }
        public async Task<bool> DeletePlanMaster(int id, string clientKey = null, string secretKey = null)
        {
            try
            {
                configuration.ClientURL = @"http://localhost:44338/";

                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(secretKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, secretKey);

                configuration.RequestURL = $"api/v1/PlanMaster/PlanMaster/{id}";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.DELETE, DataFormat.Json);
                configuration.JsonObject = new JObject
                {
                    // its because service is not really delete pysicaly so 
                    // we have to follow the service like this n I dont know why so just follow the service (Hari)
                    //configuration.Request.AddParameter("isDeleted", true);
                    { "id", id }
                };

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Execute(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                return await Task.FromResult(true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }

        #endregion

        #region PlanMaster Limit Condition

        public async Task<Tuple<PlanLimitCondition, bool>> AddPlanLimitCondition(List<PlanLimitCondition> planLimitCondition, string clientKey = null, string secretKey = null)
        {
            try
            {
                // Client URL
                configuration.ClientURL = @"http://localhost:44338/";

                // Add authorization key if exist
                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(secretKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, secretKey);

                configuration.RequestURL = @"api/PlanLimitCondition/PlanLimitCondition";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.POST, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var requestTime = DateTime.Now;

                foreach (var item in planLimitCondition)
                {
                    configuration.JsonObject.Add("planID", item.PlanMaster.ID);
                    configuration.JsonObject.Add("conditionTypeID", item.ConditionType.ID);
                    configuration.JsonObject.Add("limitTypeID", item.LimitType.ID);
                    configuration.JsonObject.Add("value", item.Value);
                    configuration.JsonObject.Add("flag", item.Flag);
                    configuration.JsonObject.Add("createDate", requestTime);
                    configuration.JsonObject.Add("createBy", item.CreateBy);

                    // Attention for this object: Make sure this value is always false.
                    // just follow the service. (Hari)
                    configuration.JsonObject.Add("isDeleted", false);

                }

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Execute(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                var collection = new PlanLimitCondition();

                return await Task.FromResult(Tuple.Create(collection, true));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }

        }
        public async Task<Tuple<PlanLimitCondition, bool>> UpdatePlanLimitCondition(int id, PlanLimitCondition planLimitCondition, string clientKey = null, string secretKey = null)
        {

            try
            {
                // Client URL
                configuration.ClientURL = @"http://localhost:44338/";

                // Add authorization key if exist
                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(secretKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, secretKey);

                configuration.RequestURL = @"api/PlanLimitCondition/PlanLimitCondition";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.PUT, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var requestTime = DateTime.Now;
                configuration.JsonObject.Add("id", id);
                configuration.JsonObject.Add("planID", planLimitCondition.PlanMaster.ID);
                configuration.JsonObject.Add("conditionTypeID", planLimitCondition.ConditionType.ID);
                configuration.JsonObject.Add("limitTypeID", planLimitCondition.LimitType.ID);
                configuration.JsonObject.Add("value", planLimitCondition.Value);
                configuration.JsonObject.Add("flag", planLimitCondition.Flag);
                configuration.JsonObject.Add("updateDate", requestTime);
                configuration.JsonObject.Add("updateBy", planLimitCondition.CreateBy);

                // Attention for this object: Make sure this value is always false.
                // just follow the service. (Hari)
                configuration.JsonObject.Add("isDeleted", false);


                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Execute(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                var collection = new PlanLimitCondition();

                return await Task.FromResult(Tuple.Create(collection, true));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }

        }
        public async Task<bool> DeletePlanLimitCondition(int id, string clientKey = null, string secretKey = null)
        {
            try
            {
                configuration.ClientURL = @"http://localhost:44338/";

                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(secretKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, secretKey);

                configuration.RequestURL = $"api/PlanLimitCondition/PlanLimitCondition/{id}";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.DELETE, DataFormat.Json);
                configuration.JsonObject = new JObject
                {
                    // its because service is not really delete pysicaly so 
                    // we have to follow the service like this n I dont know why so just follow the service (Hari)
                    //configuration.Request.AddParameter("isDeleted", true);
                    { "id", id }
                };

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Execute(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                return await Task.FromResult(true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }


        }

        #endregion

        #region Benefit Mapping

        public async Task<Tuple<Benefit, bool>> AddBenefitMappingToBenefitItem(List<Benefit> benefitMapping, string clientKey = null, string secretKey = null)
        {
            try
            {
                // Client URL
                configuration.ClientURL = @"http://localhost:44338/";

                // Add authorization key if exist
                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(secretKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, secretKey);

                configuration.RequestURL = @"api/MappingBenefitToBenefitItem/MappingBenefitToBenefitItem";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.POST, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var requestTime = DateTime.Now;

                foreach (var item in benefitMapping)
                {
                    configuration.JsonObject.Add("benefitID", item.ID);
                    configuration.JsonObject.Add("benefitItemID", 1);
                    configuration.JsonObject.Add("createDate", requestTime);
                    configuration.JsonObject.Add("createBy", item.CreateBy);

                    // Attention for this object: Make sure this value is always false.
                    // just follow the service. (Hari)
                    configuration.JsonObject.Add("isDeleted", false);

                }

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Execute(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                var collection = new Benefit();

                return await Task.FromResult(Tuple.Create(collection, true));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
                throw new ArgumentException(($"An error occured when request to API: {ex.Message}"));
            }
        }

        public async Task<Tuple<Benefit, bool>> AddBenefitMappingToSubMedicalService(List<Benefit> benefitMapping, string clientKey = null, string secretKey = null)
        {
            try
            {
                // Client URL
                configuration.ClientURL = @"http://localhost:44338/";

                // Add authorization key if exist
                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(secretKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, secretKey);

                configuration.RequestURL = @"api/MappingSubMedicalServiceToBenefit/MappingSubMedicalServiceToBenefit";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.POST, DataFormat.Json);
                configuration.JsonObject = new JObject();

                var requestTime = DateTime.Now;

                foreach (var item in benefitMapping)
                {
                    configuration.JsonObject.Add("subMedicalServiceID", 1);
                    configuration.JsonObject.Add("benefitItemID", 1);
                    configuration.JsonObject.Add("createDate", requestTime);
                    configuration.JsonObject.Add("createBy", item.CreateBy);

                    // Attention for this object: Make sure this value is always false.
                    // just follow the service. (Hari)
                    configuration.JsonObject.Add("isDeleted", false);

                }

                configuration.Request.AddParameter("application/json", configuration.JsonObject, ParameterType.RequestBody);

                var response = configuration.Client.Execute(configuration.Request);

                if (response.Content == null)
                    throw new Exception(($"An error occured when request to API: {response.ErrorMessage}"));

                var collection = new Benefit();

                return await Task.FromResult(Tuple.Create(collection, true));

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
