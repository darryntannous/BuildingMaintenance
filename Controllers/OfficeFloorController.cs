using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingMaintenance.Models;
using BuildingMaintenance.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuildingMaintenance.Controllers
{
    public class OfficeFloorController : BaseController
    {
        public IActionResult Index(int? id)
        {
            OfficeFloorModel model = new OfficeFloorModel();
            model.OfficeFloorID = id;
            model.OfficeFloors = ControllerServices.Instance.GetOfficeFloors(id);

            DynamicDropDownModel dropDownModel = new DynamicDropDownModel();
            dropDownModel.TableName = "OfficeFloors";
            dropDownModel.PrimaryColumnID = "FloorID";
            dropDownModel.DropDownData = ControllerServices.Instance.GetFloors(null, true).Select(x => new DropDownData() { Value = x.FloorID, DisplayText = x.FloorName, Attribute1 = x.FloorRate }).ToList();
            if (id.HasValue && model.OfficeFloors != null && model.OfficeFloors.Count > 0)
            {
                dropDownModel.PrimaryColumnValue = model.OfficeFloors.FirstOrDefault().FloorID;
            }
            model.Floors = dropDownModel;

            dropDownModel = new DynamicDropDownModel();
            dropDownModel.TableName = "OfficeFloors";
            dropDownModel.PrimaryColumnID = "OfficeID";
            dropDownModel.DropDownData = ControllerServices.Instance.GetOffices(null).Select(x => new DropDownData() { Value = x.OfficeID, DisplayText = x.OfficeName }).ToList();
            if (id.HasValue && model.OfficeFloors != null && model.OfficeFloors.Count > 0)
            {
                dropDownModel.PrimaryColumnValue = model.OfficeFloors.FirstOrDefault().OfficeID;
            }
            model.Offices = dropDownModel;

            dropDownModel = new DynamicDropDownModel();
            dropDownModel.TableName = "OfficeFloors";
            dropDownModel.PrimaryColumnID = "BuildingID";
            dropDownModel.DropDownData = ControllerServices.Instance.GetBuildings(null, true).Select(x => new DropDownData() { Value = x.BuildingID, DisplayText = x.BuildingName }).ToList();
            if (id.HasValue && model.OfficeFloors != null && model.OfficeFloors.Count > 0)
            {
                dropDownModel.PrimaryColumnValue = model.OfficeFloors.FirstOrDefault().BuildingID;
            }
            model.Buildings = dropDownModel;

            dropDownModel = new DynamicDropDownModel();
            dropDownModel.TableName = "OfficeFloors";
            dropDownModel.PrimaryColumnID = "OfficeStatusID";
            dropDownModel.DropDownData = ControllerServices.Instance.GetOfficeStatuses(null, true).Select(x => new DropDownData() { Value = x.OfficeStatusID, DisplayText = x.StatusName }).ToList();
            if (id.HasValue && model.OfficeFloors != null && model.OfficeFloors.Count > 0)
            {
                dropDownModel.PrimaryColumnValue = model.OfficeFloors.FirstOrDefault().OfficeStatusID;
            }
            model.OfficeStatuses = dropDownModel;

            return View(model);
        }
    }
}