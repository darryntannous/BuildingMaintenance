using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingMaintenance.Models;
using BuildingMaintenance.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuildingMaintenance.Controllers
{
    public class BuildingController : BaseController
    {
        public IActionResult Index(int? id)
        {
            BuildingModel model = new BuildingModel();
            model.Buildings = ControllerServices.Instance.GetBuildings(id, null);
            model.BuildingID = id;

            DynamicDropDownModel dropDownModel = new DynamicDropDownModel();
            dropDownModel.TableName = "Buildings";
            dropDownModel.PrimaryColumnID = "SocietyID";
            if(id.HasValue && model.Buildings != null && model.Buildings.Count > 0)
            {
                dropDownModel.PrimaryColumnValue = model.Buildings.FirstOrDefault().SocietyID;
            }
            dropDownModel.DropDownData = ControllerServices.Instance.GetSocieties(null, true).Select(x => new DropDownData() { Value = x.SocietyID, DisplayText = x.SocietyName }).ToList();
            model.Societies = dropDownModel;
            return View(model);
        }
    }
}