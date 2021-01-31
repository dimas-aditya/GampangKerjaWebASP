using HCMSSMI.Entities.Models.Login;
using HCMSSMI.Reader;
using HCMSSMI.Writer;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

[Authorize]
public class AccountController : Controller
{
    #region Private Members

    private readonly IReaderService reader;
    private readonly IWriterService writer;

    #endregion

    #region Constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="reader">A single instance parameter from <see cref="IReaderService"/></param>
    /// <param name="writer">A single instance parameter from <see cref="IWriterService"/></param>
    public AccountController(IReaderService reader, IWriterService writer)
    {
        this.reader = reader;
        this.writer = writer;
    }

    #endregion

    public ActionResult Index() => View();


    [HttpGet]
    public JsonResult Roles()
    {
        var roles = reader.GetUserRoles();
        return Json(roles, JsonRequestBehavior.AllowGet);
    }
    
    public ActionResult LogedInUsers()
    {
        var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
        string userName = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                                                  .Select(c => c.Value).SingleOrDefault();

        var loginUser = reader.GetUserAccountDetail(userName).Result;

        Users users = new Users();

        foreach (var user in loginUser.Data)
        {
            users = new Users()
            {
                ID = user.ID,
                Username = user.Username,
                RoleID = user.RoleID,
                Email = user.Email,
                FullName = user.FullName,
                Roles = new Roles()
                {
                    ID = user.Roles.ID,
                    Name = user.Roles.Name,
                    Description = user.Roles.Description,
                }
            };
        }
        return PartialView(users);
    }

    public ActionResult LogedInUsersJet()
    {
        var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
        string userName = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                                                  .Select(c => c.Value).SingleOrDefault();

        var loginUser = reader.GetUserAccountDetail(userName).Result;

        Users users = new Users();

        foreach (var user in loginUser.Data)
        {
            users = new Users()
            {
                ID = user.ID,
                Username = user.Username,
                RoleID = user.RoleID,
                Email = user.Email,
                FullName = user.FullName,
                Roles = new Roles()
                {
                    ID = user.Roles.ID,
                    Name = user.Roles.Name,
                    Description = user.Roles.Description,
                }
            };
        }
        return PartialView(users);
    }

    [HttpGet]
    public JsonResult LoginUser()
    {
        var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
        string userName = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                                                  .Select(c => c.Value).SingleOrDefault();
        var loginUser = reader.GetUserAccountDetail(userName);

        return Json(loginUser, JsonRequestBehavior.AllowGet);
    }


    public ActionResult Logout()
    {
        var oContext = Request.GetOwinContext();
        var authenticationManager = oContext.Authentication;
        authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        return RedirectToAction("signin", "Home");
    }



}
