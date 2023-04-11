using BuildingMaintenance.CommonCode.Helpers;
using BuildingMaintenance.Models;
using BuildingMaintenance.Models.Entities;
using BuildingMaintenance.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMaintenance.Components
{
    public class CPSideMenuNavigationViewComponent : ViewComponent
    {
        AuthenticationService authenticationService;
        public CPSideMenuNavigationViewComponent(AuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }
        public IViewComponentResult Invoke()
        {
            CPSideMenuNavigationModel model = new CPSideMenuNavigationModel();
            if (authenticationService.IsLoggedIn)
            {
                var cpsidemenunavigations = CommonServices.Instance.GetCPSideMenuNavigations();
                ApplicationUser user = authenticationService.User;
                if (authenticationService.IsAdminUser)
                {
                    model.CPSideMeNunavigations = cpsidemenunavigations;
                }
                else if(user.CPSideMenuNavigationIDs.Count > 0)
                {
                    model.CPSideMeNunavigations = cpsidemenunavigations.Where(x => user.CPSideMenuNavigationIDs.Contains(x.CPSideMenuNavigationID)).ToList();
                }
            }
            return View("~/Views/Components/_CPSideMenuNavigation.cshtml", model);
        }
    }
}
