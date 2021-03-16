using HCMSSMI.Entities.Models.Candidate;
using HCMSSMI.Entities.Models.Profile;
using HCMSSMI.Reader;
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
using PagedList;
using PagedList.Mvc;

namespace HCMSSMI.Controllers
{
    public class CandidateController : Controller
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
        public CandidateController(IReaderService reader, IWriterService writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        #endregion

        //// GET: Candidate
        //public ActionResult CandidateDetail2()
        //{
        //    ViewBag.IsAlertResponse = false;
        //    ViewBag.ActivityResponsMessage = null;
        //    return View();
        //}


        #region Search Candidate

        [HttpGet]
        public async Task<ActionResult> searchCandidate([Bind] SearchCandidate searchCandidate, string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var item = new SearchCandidate()
                {
                    Username = searchCandidate.Username,
                    Experience = searchCandidate.Experience,
                    Gender = searchCandidate.Gender,
                    CreateDate = searchCandidate.CreateDate,
                    Skills = searchCandidate.Skills,
                    Type = searchCandidate.Type
                };
                var candidateResult = await reader.SearchCandidate(item);
                ViewBag.CandidateList = candidateResult;


            var count = candidateResult.Count();
            ViewBag.CountCandidateList = count;

            int pageSize = 3;
            int pageNumber = (page ?? 1);

           var PagelistCandidate = candidateResult.ToPagedList(pageNumber, pageSize);

            return View();

        }

        #endregion

        // GET: Candidate
        [HttpGet]
        public async Task<ActionResult> CandidateDetail(string nama, string clientKey = null, string apiKey = null)
        {

            ViewBag.IsAlertResponse = false;
            ViewBag.ActivityResponsMessage = null;


            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var userName = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                                                      .Select(c => c.Value).SingleOrDefault();



          
            var loginUser = userName;
            ViewBag.UserLogin = loginUser;

            if (nama.Equals("")|| nama ==null)
            {
                nama = userName;
            }
            if (userName != null)
            {
                //var resultBenefit = reader.GetBenefitItem();
                Profile model = new Profile();

                var fetchingProfileList = await reader.SearchProfileIndex(nama);

                //side menu validasi 
                var role = fetchingProfileList.Data.FirstOrDefault(x => x.RoleID == x.RoleID)?.RoleID;
                ViewBag.roleID = role;

                //formatRight Createdate kanan
                var CreateDateRightLabel = fetchingProfileList.Data.FirstOrDefault(x => x.CreateDate == x.CreateDate)?.CreateDate;
                var CreateDateRightTemp = CreateDateRightLabel.Substring(0, 10);
                //var time = DateTime.Now.ToString("hh:mm:ss");
                string formatCreateDate = "yyyy-MM-dd";
                CultureInfo CultureCreateDate = CultureInfo.InvariantCulture;
                var CreateDateRight = DateTime.ParseExact(CreateDateRightTemp, formatCreateDate, CultureCreateDate);

                var CreateDateFinalRight = Convert.ToDateTime(CreateDateRight).ToString("dd MMMM yyyy");
                ViewBag.CreateDateLabel = CreateDateFinalRight;

                //formatRight tanggal kanan
                var DOBRightLabel = fetchingProfileList.Data.FirstOrDefault(x => x.DOB == x.DOB)?.DOB;
                var DOBRightTemp = DOBRightLabel.Substring(0, 10);
                //var time = DateTime.Now.ToString("hh:mm:ss");
                string formatRight = "yyyy-MM-dd";
                CultureInfo CultureDOBRight = CultureInfo.InvariantCulture;
                var dateRight = DateTime.ParseExact(DOBRightTemp, formatRight, CultureDOBRight);

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



                    model = new Profile()
                    {
                        ProfileID = itemValue.ProfileID,
                        Email = itemValue.Email,
                        FullName = itemValue.FullName,
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

    }
}