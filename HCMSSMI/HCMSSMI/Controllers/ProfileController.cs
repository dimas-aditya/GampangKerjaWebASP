using HCMSSMI.Entities.Models.Login;
using HCMSSMI.Entities.Models.Profile;
using HCMSSMI.Entities.Models.Profile.Helper;
using HCMSSMI.Reader;
using HCMSSMI.ViewModels;
using HCMSSMI.Writer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HCMSSMI.Controllers
{
    public class ProfileController : Controller
    {

        #region Private Members

        private readonly IReaderService reader;
        private readonly IWriterService writer;
        private readonly IOAuth oAuth;

        #endregion

        #region Properties

        private bool IsAlertResponse { get; set; } = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="reader">A single instance parameter from <see cref="IReaderService"/></param>
        /// <param name="writer">A single instance parameter from <see cref="IWriterService"/></param>
        public ProfileController(IReaderService reader, IWriterService writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        #endregion


        [HttpGet]
        public ActionResult Index2()
        {
            ViewBag.IsAlertResponse = false;
            ViewBag.ActivityResponsMessage = null;

            return this.View();
        }


        // GET: Profile
        [HttpGet]
        public async Task<ActionResult> UpdateProfile(string userName, string clientKey = null, string apiKey = null)
        {

            ViewBag.IsAlertResponse = false;
            ViewBag.ActivityResponsMessage = null;

            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            userName = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                                                      .Select(c => c.Value).SingleOrDefault();

            var fetchingProfileList = await reader.SearchProfileIndex(userName);


        

            if (userName != null)
            {
              
                //var resultBenefit = reader.GetBenefitItem();
                Profile model = new Profile();


                //side menu validasi 
                var role = fetchingProfileList.Data.FirstOrDefault(x => x.RoleID == x.RoleID)?.RoleID;
                ViewBag.roleID = role;
                var loginUser = userName;
                ViewBag.UserLogin = loginUser;

                //Get dropdown list gender
                string genderString = "Laki-laki,Wanita";
                IEnumerable<GenderProfile> Infogender =
                    from name in genderString.Split(',')
                    select new GenderProfile()
                    {
                        Gender = name,
                        NameGender = name
                    };
                List<GenderProfile> listGender = Infogender.ToList();
                ViewBag.GenderList = listGender;

                //Get dropdown list Experience
                string experienceString = "Fresh Graduate,1 Tahun,2 Tahun,3 Tahun,4 Tahun,5 Tahun";
                IEnumerable<ExperienceProfile> Infoexperience =
                    from name in experienceString.Split(',')
                    select new ExperienceProfile()
                    {
                        Experience = name,
                        NameExperience = name
                    };
                List<ExperienceProfile> listExp = Infoexperience.ToList();
                ViewBag.ExpList = listExp;


                //Get dropdown list Qualification
                string QualificationString = "SMK/SMA,Diploma 1,Diploma 2,Diploma 3,Sarjana 1,Sarjana 2,Sarjana 3";
                IEnumerable<QualificationProfile> InfoQualification =
                    from name in QualificationString.Split(',')
                    select new QualificationProfile()
                    {
                        Qualification = name,
                        NameQualification = name
                    };
                List<QualificationProfile> listQualification = InfoQualification.ToList();
                ViewBag.QualificationList = listQualification;


                //Get dropdown list Jabatan
                string JabatanString = "Staff,Manager,Supervisor";
                IEnumerable<JabatanProfile> InfoJabatan =
                    from name in JabatanString.Split(',')
                    select new JabatanProfile()
                    {
                        Jabatan = name,
                        NameJabatan = name
                    };
                List<JabatanProfile> listJabatan = InfoJabatan.ToList();
                ViewBag.JabatanList = listJabatan;

                //Get dropdown list Type
                string TypeString = "Freelance,Kontrak,Paruh Waktu,Penuh Waktu";
                IEnumerable<TypeProfile> InfoType =
                    from name in TypeString.Split(',')
                    select new TypeProfile()
                    {
                        Type = name,
                        NameType = name
                    };
                List<TypeProfile> listType = InfoType.ToList();
                ViewBag.TypeList = listType;

                //Get dropdown list Setyourprofile
                string SetyourprofileString = "Public,Private";
                IEnumerable<SetYourProfile> InfoSetyourprofile =
                    from name in SetyourprofileString.Split(',')
                    select new SetYourProfile()
                    {
                        Setyourprofile = name,
                        NameSetYourProfile = name
                    };
                List<SetYourProfile> listSetyourprofile = InfoSetyourprofile.ToList();
                ViewBag.SetyourprofileList = listSetyourprofile;


                //Get dropdown list Profession
                string ProfessionString = "Professional,Private";
                IEnumerable<ProfessionProfile> InfoProfession =
                    from name in ProfessionString.Split(',')
                    select new ProfessionProfile()
                    {
                        Profession = name,
                        NameProfession = name
                    };
                List<ProfessionProfile> listProfession = InfoProfession.ToList();
                ViewBag.ProfessionList = listProfession;


              

                //setting label kanan 
                var FullNameRightLabel = fetchingProfileList.Data.FirstOrDefault(x => x.FullName == x.FullName)?.FullName;
                ViewBag.FullNameLabel = FullNameRightLabel;

                var ProfessionRightLabel = fetchingProfileList.Data.FirstOrDefault(x => x.Profession == x.Profession)?.Profession;
                ViewBag.ProfessionLabel = ProfessionRightLabel;

                var EmailRightLabel = fetchingProfileList.Data.FirstOrDefault(x => x.Email == x.Email)?.Email;
                ViewBag.EmailLabel = EmailRightLabel;

                var PhoneRightLabel = fetchingProfileList.Data.FirstOrDefault(x => x.Phone == x.Phone)?.Phone;
                ViewBag.PhoneLabel = PhoneRightLabel;

                var JobTitleRightLabel = fetchingProfileList.Data.FirstOrDefault(x => x.JobTitle == x.JobTitle)?.JobTitle;
                ViewBag.JobTitleLabel = JobTitleRightLabel;

                //formatRight tanggal kanan
                var DOBRightLabel = fetchingProfileList.Data.FirstOrDefault(x => x.DOB == x.DOB)?.DOB;
                var DOBRightTemp = DOBRightLabel.Substring(0, 10);
                //var time = DateTime.Now.ToString("hh:mm:ss");
                string formatRight = "yyyy-MM-dd";
                CultureInfo providerRight = CultureInfo.InvariantCulture;
                var dateRight = DateTime.ParseExact(DOBRightTemp, formatRight, providerRight);

                var dateFinalRight = Convert.ToDateTime(dateRight).ToString("dd MMMM yyyy");
                ViewBag.DOBLabel = dateFinalRight;

                foreach (var itemValue in fetchingProfileList.Data)
                {
                    var DOBTemp = itemValue.DOB.Substring(0, 10);
                    //var time = DateTime.Now.ToString("hh:mm:ss");
                    string format = "yyyy-MM-dd";
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    var date = DateTime.ParseExact(DOBTemp, format, provider);

                    var dateFinal = Convert.ToDateTime(date).ToString("MM/dd/yyyyThh:mm:ss.549Z").Substring(0, 10);

                    if (dateFinal.Equals("01/01/0001"))
                    {
                        var dateval = DateTime.Now.ToString("MM/dd/yyyy");
                        dateFinal = dateval;
                    }

                    model = new Profile()
                    {
                        ProfileID = itemValue.ProfileID,
                        Email = itemValue.Email,
                        FullName = itemValue.FullName,
                        RoleID = itemValue.RoleID,
                        Username = itemValue.Username,
                        Profession = itemValue.Profession,
                        DOB = dateFinal,
                        Gender = itemValue.Gender,
                        Phone = itemValue.Phone,
                        CoverImage = itemValue.CoverImage,
                        Experience = itemValue.Experience,
                        ProfileImage = itemValue.ProfileImage,
                        Jabatan = itemValue.Jabatan,
                        Qualification = itemValue.Qualification,
                        Type = itemValue.Type,
                        SalaryRange = itemValue.SalaryRange,
                        Setyourprofile = itemValue.Setyourprofile,
                        AboutSelf = itemValue.AboutSelf,
                        JobTitle = itemValue.JobTitle,
                        IsActive = itemValue.IsActive,
                    };
                }

                return View(model);
            }
            else
            {
                return RedirectToAction("signin", "Home");
            }


        }


        // PUT Profile

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateProfile([Bind] Profile profile, string clientKey = null, string secretKey = null)
        {

            Profile model = new Profile();

            ViewBag.IsAlertResponse = false;
            ViewBag.ActivityResponsMessage = null;

            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var userName = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                                                      .Select(c => c.Value).SingleOrDefault();

          

            var DateRequest = profile.DOB;
            var time = DateTime.Now.ToString("hh:mm:ss");
            string format = "MM/dd/yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;
            var date = DateTime.ParseExact(DateRequest, format, provider);

            var dateFinal = Convert.ToDateTime(date).ToString("yyyy-MM-ddThh:mm:ss.549Z").Substring(0, 10) + "T" + time + ".549Z";

            var item = new Profile()
            {
                //CallItemDate = Convert.ToDateTime(date).ToString("yyyy-MM-ddThh:mm:ss.549Z"),
                Email = profile.Email,
                FullName = profile.FullName,
                ProfileID = profile.ProfileID,
                RoleID = "0",
                CreateDate = dateFinal,
                Username = userName,
                Profession = profile.Profession,
                DOB = dateFinal,
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
                IsActive = profile.IsActive,
            };

            var result = await writer.UpdateProfile(item, clientKey, secretKey);

            if (result.Item2)
            {
                ViewBag.IsAlertResponse = true;

                var responser = new ActivityResponsMessage()
                {
                    Message = "The data has been updated successfully!",
                    RequestTime = DateTime.Now,
                    ResponseTime = DateTime.Now,
                    Type = "success"
                };

                ViewBag.ActivityResponsMessage = responser;

            }
            else
            {

                ViewBag.IsAlertResponse = true;

                var responser = new ActivityResponsMessage()
                {
                    Message = "Opss.. Failed to update item!",
                    RequestTime = DateTime.Now,
                    ResponseTime = DateTime.Now,
                    Type = "error"
                };

                ViewBag.ActivityResponsMessage = responser;
            }


            //balikin data get atau panggil method get 

            var fetchingProfileList = await reader.SearchProfileIndex(userName);

            //side menu validasi 
            var role = fetchingProfileList.Data.FirstOrDefault(x => x.RoleID == x.RoleID)?.RoleID;
            ViewBag.roleID = role;

            //Get dropdown list gender
            string genderString = "Laki-laki,Wanita";
            IEnumerable<GenderProfile> Infogender =
                from name in genderString.Split(',')
                select new GenderProfile()
                {
                    Gender = name,
                    NameGender = name
                };
            List<GenderProfile> listGender = Infogender.ToList();
            ViewBag.GenderList = listGender;

            //Get dropdown list Experience
            string experienceString = "Fresh Graduate,1 Tahun,2 Tahun,3 Tahun,4 Tahun,5 Tahun";
            IEnumerable<ExperienceProfile> Infoexperience =
                from name in experienceString.Split(',')
                select new ExperienceProfile()
                {
                    Experience = name,
                    NameExperience = name
                };
            List<ExperienceProfile> listExp = Infoexperience.ToList();
            ViewBag.ExpList = listExp;


            //Get dropdown list Qualification
            string QualificationString = "SMK/SMA,Diploma 1,Diploma 2,Diploma 3,Sarjana 1,Sarjana 2,Sarjana 3";
            IEnumerable<QualificationProfile> InfoQualification =
                from name in QualificationString.Split(',')
                select new QualificationProfile()
                {
                    Qualification = name,
                    NameQualification = name
                };
            List<QualificationProfile> listQualification = InfoQualification.ToList();
            ViewBag.QualificationList = listQualification;


            //Get dropdown list Jabatan
            string JabatanString = "Staff,Manager,Supervisor";
            IEnumerable<JabatanProfile> InfoJabatan =
                from name in JabatanString.Split(',')
                select new JabatanProfile()
                {
                    Jabatan = name,
                    NameJabatan = name
                };
            List<JabatanProfile> listJabatan = InfoJabatan.ToList();
            ViewBag.JabatanList = listJabatan;

            //Get dropdown list Type
            string TypeString = "Freelance,Kontrak,Paruh Waktu,Penuh Waktu";
            IEnumerable<TypeProfile> InfoType =
                from name in TypeString.Split(',')
                select new TypeProfile()
                {
                    Type = name,
                    NameType = name
                };
            List<TypeProfile> listType = InfoType.ToList();
            ViewBag.TypeList = listType;

            //Get dropdown list Setyourprofile
            string SetyourprofileString = "Public,Private";
            IEnumerable<SetYourProfile> InfoSetyourprofile =
                from name in SetyourprofileString.Split(',')
                select new SetYourProfile()
                {
                    Setyourprofile = name,
                    NameSetYourProfile = name
                };
            List<SetYourProfile> listSetyourprofile = InfoSetyourprofile.ToList();
            ViewBag.SetyourprofileList = listSetyourprofile;

            //Get dropdown list Profession
            string ProfessionString = "Professional,Private";
            IEnumerable<ProfessionProfile> InfoProfession =
                from name in ProfessionString.Split(',')
                select new ProfessionProfile()
                {
                    Profession = name,
                    NameProfession = name
                };
            List<ProfessionProfile> listProfession = InfoProfession.ToList();
            ViewBag.ProfessionList = listProfession;

            //setting label kanan 
            var FullNameRightLabel = fetchingProfileList.Data.FirstOrDefault(x => x.FullName == x.FullName)?.FullName;
            ViewBag.FullNameLabel = FullNameRightLabel;

            var ProfessionRightLabel = fetchingProfileList.Data.FirstOrDefault(x => x.Profession == x.Profession)?.Profession;
            ViewBag.ProfessionLabel = ProfessionRightLabel;

            var EmailRightLabel = fetchingProfileList.Data.FirstOrDefault(x => x.Email == x.Email)?.Email;
            ViewBag.EmailLabel = EmailRightLabel;

            var PhoneRightLabel = fetchingProfileList.Data.FirstOrDefault(x => x.Phone == x.Phone)?.Phone;
            ViewBag.PhoneLabel = PhoneRightLabel;

            var JobTitleRightLabel = fetchingProfileList.Data.FirstOrDefault(x => x.JobTitle == x.JobTitle)?.JobTitle;
            ViewBag.JobTitleLabel = JobTitleRightLabel;


            //formatRight tanggal kanan
            var DOBRightLabel = fetchingProfileList.Data.FirstOrDefault(x => x.DOB == x.DOB)?.DOB;
            var DOBRightTemp = DOBRightLabel.Substring(0, 10);
            //var time = DateTime.Now.ToString("hh:mm:ss");
            string formatRight = "yyyy-MM-dd";
            CultureInfo providerRight = CultureInfo.InvariantCulture;
            var dateRight = DateTime.ParseExact(DOBRightTemp, formatRight, providerRight);

            var dateFinalRight = Convert.ToDateTime(dateRight).ToString("dd MMMM yyyy");
            ViewBag.DOBLabel = dateFinalRight;


            //await Task.Run(() =>
            //{
            //    ViewBag.MemberList = reader.GetMemberAsync().Result;
            //});

            return await Task.FromResult(View(profile));
        }


        //[HttpGet]
        //public async Task<ActionResult> Edit(int? id, string clientKey = null, string secretKey = null)
        //{
        //    if (id == null)
        //        throw new ArgumentException($"Parameter ID is undefined");

        //    var benefitItem = await reader.GetBenefitItem(id.Value, clientKey, secretKey);

        //    if (benefitItem == null)
        //        throw new ArgumentException($"Could not found any specific data on ID {id}");

        //    ViewBag.IsAlertResponse = false;
        //    ViewBag.ActivityResponsMessage = null;


        //    return View(benefitItem);
        //}


        #region Private Helper 


        #endregion
    }
}
