using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BuildingMaintenance.Models;
using Microsoft.AspNetCore.Authorization;
using BuildingMaintenance.Services;

namespace BuildingMaintenance.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //bool isDeveloper = User.Claims.Any(c => c.Type == "IsDeveloper" && c.Value == "true");
            HomeModel model = new HomeModel();
            model.CurrentDate = DateTime.Now;
            List<YearAndMont> yearMonth = new List<YearAndMont>();
            yearMonth.Add(new YearAndMont() { Year = model.CurrentDate.Year, Month = model.CurrentDate.Month });
            yearMonth.Add(new YearAndMont() { Year = model.CurrentDate.AddMonths(-1).Year, Month = model.CurrentDate.AddMonths(-1).Month });
            model.OfficeFloorDetails = ControllerServices.Instance.GetBillingStats(1, yearMonth);
            model.NoOfOffices = ControllerServices.Instance.GetNumberOfOffices(1);
            model.UnPaidNoOfOffices = ControllerServices.Instance.GetUnpaidOfficesByFiscal(1, yearMonth.FirstOrDefault());

            return View(model);
        }

        public ActionResult Bootstrap()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
