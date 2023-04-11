using BMDBConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BuildingMaintenance.CommonCode.Helpers
{
    public class AuthorizationExtendAttribute : TypeFilterAttribute
    {
        public AuthorizationExtendAttribute() : base(typeof(AuthorizationExtendFilter))
        {
        }
    }
    public class AuthorizationExtendFilter : Attribute, IAuthorizationFilter
    {
        AuthenticationService authenticationService;
        public AuthorizationExtendFilter(AuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!authenticationService.IsLoggedIn)
            {
                context.Result = new RedirectToActionResult("Logout", "Authentication", null);
            }
            else
            {
                if (authenticationService.IsAdminUser)
                {

                }
                else
                {
                    var userNavigationsAllowed = authenticationService.User.CPSideMenuNavigationIDs;
                    var CurrentNavigationID = GlobalVariables.cpsidemenunavigations.Where(x => x.LinkUrl == context.HttpContext.Request.Path).FirstOrDefault().CPSideMenuNavigationID;
                    if (!userNavigationsAllowed.Contains(CurrentNavigationID))
                    {
                        context.Result = new RedirectToActionResult("Logout", "Authentication", null);
                    }
                }
            }
        }
    }
}
