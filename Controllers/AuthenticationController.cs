using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BMDBConnection;
using customHelper = BuildingMaintenance.CommonCode.Helpers;
using BuildingMaintenance.Enums;
using BuildingMaintenance.Models;
using BuildingMaintenance.Models.Entities;
using BuildingMaintenance.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using BuildingMaintenance.CommonCode.Helpers;

namespace BuildingMaintenance.Controllers
{
    public class AuthenticationController : Controller
    {
         customHelper.AuthenticationService authenticationService;
        public AuthenticationController(customHelper.AuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Enter Email And Password.");
                return View();
            }
            ApplicationUser user = UserServices.Instance.GetUserByEmail(email, password);
            if (user == null || user.Password != password)
            {
                ModelState.AddModelError("", "Incorrect username or password.");
                return View();
            }
            return RedirectToAction("LoginUser", user);
        }

        public async Task<IActionResult> LoginUser(ApplicationUser user_)
        {
            // Create the identity
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, user_.UserName));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user_.FirstName));
            identity.AddClaim(new Claim(ClaimTypes.Surname, user_.LastName));

            var userRolesPermissions = UserServices.Instance.GetUserRoles(user_.UserID);

            // Add roles
            foreach (var role in userRolesPermissions)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.RoleID.ToString()));
                identity.AddClaim(new Claim("RoleID", role.RoleID.ToString()));
                identity.AddClaim(new Claim("PermissionID", role.PermissinoID.ToString()));
                identity.AddClaim(new Claim("CPSideMenuNavigationID", role.CPSideMenuNavigationID.ToString()));
            }
            user_.UserRoles = userRolesPermissions;
            user_.RoleIDs = user_.UserRoles.Select(x => x.RoleID).ToList();
            user_.CPSideMenuNavigationIDs = user_.UserRoles.Where(x => x.CPSideMenuNavigationID.HasValue).Select(x => (int)x.CPSideMenuNavigationID).ToList();
            user_.IsAdminUser = user_.RoleIDs.Any(x => GlobalVariables.AdminRoleIDs.Contains(x));
            
            // Sign in
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            authenticationService.AddUserInSession(user_);

            return RedirectToAction("Index", "Home");
        }

        public async Task Logout()
        {
            var prop = new AuthenticationProperties()
            {
                RedirectUri = "/Home/Index"
            };
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, prop);
        }
        #endregion Login

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            ApplicationUser model = new ApplicationUser();
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(ApplicationUser user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.UserName))
            {
                ModelState.AddModelError("", "Enter Email, Password And UserName.");
                return View(user);
            }
            ApplicationUser user_ = UserServices.Instance.GetUserByEmailAndName(user.Email, user.UserName);
            if (user_ != null)
            {
                string userNameExist = user.UserName == user_.UserName ? "user name already exist." : string.Empty;
                ModelState.AddModelError("", user.Email == user_.Email ? "email" + (!string.IsNullOrEmpty(userNameExist) ? " and " : " already exist") + userNameExist : userNameExist);
                return View(user);
            }
            user.DisplayName = user.DisplayName ?? user.UserName;
            user.FirstName = user.FirstName ?? user.UserName;
            user.LastName = user.LastName ?? user.UserName;
            user.UserStatusID = (short)UserStatusesEnum.Active;
            UserServices.Instance.RegisterUser(user);
            if (user.UserID > 0)
            {
                return RedirectToAction("LoginUser", user);
            }
            else
            {
                return View();
            }
        }
        #endregion Register

    }
}