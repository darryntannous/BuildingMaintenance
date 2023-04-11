using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BMDBConnection;
using BuildingMaintenance.CommonCode.Helpers;
using BuildingMaintenance.Models;
using BuildingMaintenance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BuildingMaintenance.CommonCode.Helpers;

namespace BuildingMaintenance.Controllers
{
    [AuthorizationExtend]
    public class BaseController : Controller
    {
        [HttpGet]
        public IActionResult CPSideMenuNavigations(int? userID)
        {
            CPSideMenuNavigationModel model = new CPSideMenuNavigationModel();
            if(userID.HasValue && User.Identity.IsAuthenticated)
            {
                model.CPSideMeNunavigations = CommonServices.Instance.GetCPSideMenuNavigations(); 
            }
            //HttpContext.Session
            return PartialView("_CPSideMenuNavigation", model);
        }


    }
}