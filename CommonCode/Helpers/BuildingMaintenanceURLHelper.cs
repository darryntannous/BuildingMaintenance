using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Web;

namespace BuildingMaintenance.CommonCode.Helper
{
    public static class BuildingMaintenanceURLHelper 
    {
        public static string HomePageURL(this UrlHelper helper)
        {
            string routeURL = string.Empty;
            routeURL = helper.Link("default", null);
            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

    }
}
