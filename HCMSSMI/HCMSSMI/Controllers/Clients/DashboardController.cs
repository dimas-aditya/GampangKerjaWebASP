using HCMSSMI.Entities.Models.Login;
using HCMSSMI.Entities.Models.RestRequest;
using HCMSSMI.Extensions.Hashing;
using HCMSSMI.Reader;
using HCMSSMI.ViewModels;
using HCMSSMI.Writer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HCMSSMI.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {

        #region Private Members

        private readonly IReaderService reader;
        private readonly IWriterService writer;

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
        public DashboardController(IReaderService reader, IWriterService writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        #endregion

        // GET: Dashboard
        public async Task<ActionResult> Index()
        {

            ViewBag.IsAlertResponse = false;
            ViewBag.ActivityResponsMessage = null;

            // Info.
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            string userName = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                                                      .Select(c => c.Value).SingleOrDefault();
           
            var fetchingProfileList = await reader.SearchProfileIndex(userName);


            //side menu validasi 
            var role = fetchingProfileList.Data.FirstOrDefault(x => x.RoleID == x.RoleID)?.RoleID;
            ViewBag.roleID = role;
            var loginUser = userName;
            ViewBag.UserLogin = loginUser;

            return this.View();
        }
    }
}