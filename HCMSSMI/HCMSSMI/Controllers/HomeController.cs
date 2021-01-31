using HCMSSMI.DataModels;
using HCMSSMI.Entities.Models.Login;
using HCMSSMI.Extensions.Hashing;
using HCMSSMI.Reader;
using HCMSSMI.ViewModels;
using HCMSSMI.Writer;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

public class HomeController : Controller
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
    public HomeController(IReaderService reader, IWriterService writer)
    {
        this.reader = reader;
        this.writer = writer;
    }

    #endregion

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        ViewBag.IsAlertResponse = false;
        ViewBag.ActivityResponsMessage = null;

        // Info. 
        var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
        string userName = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                                                  .Select(c => c.Value).SingleOrDefault();
        if (userName != null)
        {
            var fetchingProfileList = await reader.SearchProfileIndex(userName);
            //side menu validasi 
            var role = fetchingProfileList.Data.FirstOrDefault(x => x.RoleID == x.RoleID)?.RoleID;
            ViewBag.roleID = role;
        } else
        {
            ViewBag.roleID = "";
        }

      

        var loginUser = userName;
        ViewBag.UserLogin = loginUser;

        return View();
    }

    [HttpGet]
    public ActionResult signup(int? status, string returnUrl)
    {
        ViewBag.IsAlertResponse = false;
        ViewBag.ActivityResponsMessage = null;

        var roleList = reader.GetUserRoles().Result;
        ViewBag.UserRoleList = roleList;

        try
        {
            if (this.Request.IsAuthenticated)
            {
                // Info.
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                string userName = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                                                          .Select(c => c.Value).SingleOrDefault();



                return RedirectToAction("Index", "Dashboard", new { @isLoggin = true });
            }
            else
            {
                AuthStatusCallBack(status.Value);
                //return RedirectToAction("signin", "Home");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred: {ex.Message}");
        }

        return this.View();
    }


    [AllowAnonymous]
    [HttpGet]
    public ActionResult Signin(int? status, string returnUrl)
    {
        ViewBag.IsAlertResponse = false;
        ViewBag.ActivityResponsMessage = null;

        var roleList = reader.GetUserRoles().Result;
        ViewBag.UserRoleList = roleList;

        try
        {
            if (this.Request.IsAuthenticated)
            {
                // Info.
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                string userName = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                                                          .Select(c => c.Value).SingleOrDefault();

               

                return RedirectToAction("Index", "Dashboard", new { @isLoggin = true });
            }
            else
            {
                AuthStatusCallBack(status.Value);
                //return RedirectToAction("signin", "Home");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred: {ex.Message}");
        }

        return this.View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Login(string username, string password)
    {
        var response = await writer.UsersAuthentificationLogin(username, Cryptography.Base64Encode(password));

        if (response.IsSuccess)
        {
            this.SignInUser(username, false);

            if (response.IsSuccess == true)
                return RedirectToAction("Index", "Dashboard", new { @isLoggin = true });

        }


        return RedirectToAction("signin", "Home", new { @status = AuthStatus.Status(AuthStatusType.LoginFailed) });

    }


    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Registration([Bind]UserAuthViewModel users)
    {
        var user = new Users()
        {
            Email = users.Email,
            Username = users.Username,
            Password = Cryptography.Base64Encode(users.Password),
            FullName = users.FullName,
            RoleID = users.RoleID,
        };

        var response = await writer.UsersAuthentificationRegister(user);

        if (response.IsSuccess)
        {
            return RedirectToAction(nameof(Index), "Home", new { @status = 201 });
        }

        return RedirectToAction(nameof(Index), "Home", new { @status = 202 });


    }

    private void SignInUser(string username, bool isPersistent)
    {
        // Initialization.
        var claims = new List<Claim>();

        try
        {
            // Setting
            claims.Add(new Claim(ClaimTypes.Name, username));
            var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            // Sign In.
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIdenties);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private void AuthStatusCallBack(int status)
    {
        ViewBag.IsAlertResponse = false;
        ViewBag.ActivityResponsMessage = null;

        if (status == AuthStatus.Status(AuthStatusType.Registered))
        {
            ViewBag.IsAlertResponse = true;

            var responser = new ActivityResponsMessage()
            {
                Message = "Congratulation, you have been registered!",
                RequestTime = DateTime.Now,
                ResponseTime = DateTime.Now,
                Type = "success"
            };

            ViewBag.ActivityResponsMessage = responser;

        }
        else if (status == AuthStatus.Status(AuthStatusType.Unregistered))
        {
            ViewBag.IsAlertResponse = true;

            var responser = new ActivityResponsMessage()
            {
                Message = "Please try again, you have not been registered!",
                RequestTime = DateTime.Now,
                ResponseTime = DateTime.Now,
                Type = "error"
            };

            ViewBag.ActivityResponsMessage = responser;

        }
        else if (status == AuthStatus.Status(AuthStatusType.Active))
        {
            ViewBag.IsAlertResponse = true;

            var responser = new ActivityResponsMessage()
            {
                Message = "Congratulation, your account has been activated!",
                RequestTime = DateTime.Now,
                ResponseTime = DateTime.Now,
                Type = "success"
            };

            ViewBag.ActivityResponsMessage = responser;

        }
        else if (status == AuthStatus.Status(AuthStatusType.NotActive))
        {
            ViewBag.IsAlertResponse = true;

            var responser = new ActivityResponsMessage()
            {
                Message = "Please try again, your account has not been activated!",
                RequestTime = DateTime.Now,
                ResponseTime = DateTime.Now,
                Type = "error"
            };

            ViewBag.ActivityResponsMessage = responser;

        }
        else if (status == AuthStatus.Status(AuthStatusType.LoginSuccess))
        {
            ViewBag.IsAlertResponse = true;

            var responser = new ActivityResponsMessage()
            {
                Message = "Congratulation, login successfull!",
                RequestTime = DateTime.Now,
                ResponseTime = DateTime.Now,
                Type = "success"
            };

            ViewBag.ActivityResponsMessage = responser;

        }
        else if (status == AuthStatus.Status(AuthStatusType.LoginFailed))
        {
            ViewBag.IsAlertResponse = true;

            var responser = new ActivityResponsMessage()
            {
                Message = "Please try again, your username & password is wrong!",
                RequestTime = DateTime.Now,
                ResponseTime = DateTime.Now,
                Type = "error"
            };

            ViewBag.ActivityResponsMessage = responser;

        }
        else
        {
            ViewBag.IsAlertResponse = true;

            var responser = new ActivityResponsMessage()
            {
                Message = "It's looks like you are not logged in",
                RequestTime = DateTime.Now,
                ResponseTime = DateTime.Now,
                Type = "error"
            };

            ViewBag.ActivityResponsMessage = responser;
        }
    }


}
