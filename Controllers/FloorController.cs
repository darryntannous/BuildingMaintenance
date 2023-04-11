using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingMaintenance.Models;
using BuildingMaintenance.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuildingMaintenance.Controllers
{
    public class FloorController : BaseController
    {
        public IActionResult Index(int? id)
        {
            FloorModel model = new FloorModel();
            model.FloorID = id;
            model.Floors = ControllerServices.Instance.GetFloors(id, null);
            return View(model);
        }
    }
}