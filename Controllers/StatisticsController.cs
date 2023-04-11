using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingMaintenance.Models;
using BuildingMaintenance.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuildingMaintenance.Controllers
{
    public class StatisticsController : BaseController
    {
        public IActionResult Index()
        {
            StatisticsModel model = new StatisticsModel();
            model.Societies = ControllerServices.Instance.GetSocieties(null, true);
            model.Buildings = ControllerServices.Instance.GetBuildings(null, true);
            model.Floors = ControllerServices.Instance.GetFloors(null, true);
            model.Offices = ControllerServices.Instance.GetOffices(null);
            model.OfficeStatuses = ControllerServices.Instance.GetOfficeStatuses(null, true);
            model.BuildingUsers = ControllerServices.Instance.GetBuildingUsers(null);
            return View(model);
        }

        public IActionResult Search(int buildingID, int? floorID, short? officeStatusID, int forYear, int month)
        {
            StatisticsModel model = new StatisticsModel();
            model.BuildingID = buildingID;
            model.FloorID = floorID;
            model.OfficeStatusID = officeStatusID; 
            model.OfficeFloorSummary = ControllerServices.Instance.GetOfficeFloorsSummary(buildingID, forYear, month, floorID, officeStatusID);
            model.Floors = ControllerServices.Instance.GetFloors(null, true);

            //model.Societies = ControllerServices.Instance.GetSocieties(null, true);
            //model.Buildings = ControllerServices.Instance.GetBuildings(null, true);
            //model.Floors = ControllerServices.Instance.GetFloors(null, true);
            //model.Offices = ControllerServices.Instance.GetOffices(null);
            //model.OfficeStatuses = ControllerServices.Instance.GetOfficeStatuses(null, true);
            //model.BuildingUsers = ControllerServices.Instance.GetBuildingUsers(null);
            return PartialView("_Search", model);
        }

        public IActionResult Billing()
        {
            BillingModel model = new BillingModel();
            model.Buildings = ControllerServices.Instance.GetBuildings(null, true);
            model.Offices = ControllerServices.Instance.GetOffices(null);
            model.Floors = ControllerServices.Instance.GetFloors(null, true);
            return View(model);
        }
        public IActionResult getbillingdata(int MonthID, int Year, int OfficeID, int BuildingID)
        {
            BillingModel model = new BillingModel();
            model.BillingData = ControllerServices.Instance.GetBillingInformation(MonthID, Year, OfficeID, BuildingID);
            model.Buildings = ControllerServices.Instance.GetBuildings(null, true);
            model.Offices = ControllerServices.Instance.GetOffices(null);
            model.Floors = ControllerServices.Instance.GetFloors(null, true);
            return PartialView("_Billing", model);
        }
    }
}