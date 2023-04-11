using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingMaintenance.Models;
using BuildingMaintenance.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuildingMaintenance.Controllers
{
    public class BuildingUserController : BaseController
    {
        public IActionResult Index(int? id)
        {
            BuildingUserModel model = new BuildingUserModel();
            model.UserID = id;
            model.BuildingUsers = ControllerServices.Instance.GetBuildingUsers(id);
            return View(model);
        }
    }
}