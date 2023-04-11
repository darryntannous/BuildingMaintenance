using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingMaintenance.CommonCode;
using BuildingMaintenance.CommonCode.Helpers;
using BuildingMaintenance.Enums;
using BuildingMaintenance.Models;
using BuildingMaintenance.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static BuildingMaintenance.CommonCode.Helpers.CommonHelper;

namespace BuildingMaintenance.Controllers
{
    public class SocietyController : BaseController
    {
        AuthenticationService authenticationService;
        public SocietyController(AuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }
        public IActionResult Index(int? id)
        {
            SocietyModel model = new SocietyModel();
            model.SocietyID = id;
            model.Societies = ControllerServices.Instance.GetSocieties(id, null);
            return View(model);
        }

        
    }
}