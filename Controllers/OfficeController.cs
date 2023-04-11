using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingMaintenance.Models;
using BuildingMaintenance.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuildingMaintenance.Controllers
{
    public class OfficeController : BaseController
    {
        public IActionResult Index(int? id)
        {
            OfficeModel model = new OfficeModel();
            model.OfficeID = id;
            model.Offices = ControllerServices.Instance.GetOffices(id);

            DynamicDropDownModel dropDownModel = new DynamicDropDownModel();
            dropDownModel.TableName = "Offices";
            dropDownModel.PrimaryColumnID = "OfficeStatusID";
            if (id.HasValue && model.Offices != null && model.Offices.Count > 0)
            {
                dropDownModel.PrimaryColumnValue = model.Offices.FirstOrDefault().OfficeStatusID;
            }
            dropDownModel.DropDownData = ControllerServices.Instance.GetOfficeStatuses(null, true).Select(x => new DropDownData() { Value = x.OfficeStatusID, DisplayText = x.StatusName }).ToList();
            model.OfficeStatuses = dropDownModel;

            dropDownModel = new DynamicDropDownModel();
            dropDownModel.TableName = "Offices";
            dropDownModel.PrimaryColumnID = "UserID";
            if (id.HasValue && model.Offices != null && model.Offices.Count > 0)
            {
                dropDownModel.PrimaryColumnValue = model.Offices.FirstOrDefault().UserID;
            }
            dropDownModel.DropDownData = ControllerServices.Instance.GetBuildingUsers(null).Select(x => new DropDownData() { Value = x.UserID, DisplayText = x.UserName }).ToList();
            model.User = dropDownModel;
            return View(model);
        }

        public IActionResult OfficeStatus(int? id)
        {
            OfficeStatusModel model = new OfficeStatusModel();
            model.OfficeStatusID = id;
            model.OfficeStatuses = ControllerServices.Instance.GetOfficeStatuses(id, null);
            return View(model);
        }
    }
}