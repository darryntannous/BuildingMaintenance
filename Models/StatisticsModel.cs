using BMDBConnection;
using BuildingMaintenance.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMaintenance.Models
{
    public class StatisticsModel
    {
        public int BuildingID { get; set; }
        public int? FloorID { get; set; }
        public short? OfficeStatusID { get; set; }
        public List<society> Societies { get; set; }
        public List<building> Buildings { get; set; }
        public List<floor> Floors { get; set; }
        public List<office> Offices { get; set; }
        public List<officestatus> OfficeStatuses { get; set; }
        public List<buildinguser> BuildingUsers { get; set; }
        public List<officefloor> OfficeFloors { get; set; }
        public List<SearchStatisticsEntity> OfficeFloorSummary { get; set; }
        public List<officefloordetail> OfficeFloorDetails { get; set; }


    }
    public class BillingModel
    {
        public List<officefloordetail> Officefloordetails { get; set; }
        public List<building> Buildings { get; set; }
        public List<floor> Floors { get; set; }
        public List<office> Offices { get; set; }
        public List<officestatus> OfficeStatuses { get; set; }
        public List<buildinguser> BuildingUsers { get; set; }
        public List<society> Societies { get; set; }
        public List<Months> Months { get; set; }
        public List<BillingData> BillingData { get; set; }
        public List<officefloor> OfficeFloors { get; set; }
        public OfficeFloorDetailsModel OfficeFloorModel { get; set; }
        public DynamicDropDownModel OfficeFloor { get; set; }
    }
    public class Months
    {
        public int MonthID { get; set; }
        public string MonthName { get; set; }
    }
    public class BillingData
    {
        public int OfficeID { get; set; }
        public int FloorID { get; set; }
        public decimal Space { get; set; }
        public int OfficeFloorID { get; set; }
        public int? OfficeFloorDetailID { get; set; }
        public decimal? MonthlyAmount { get; set; }
        public string PaidDate { get; set; }
        public decimal? CONTRIBUTION { get; set; }
        public decimal? PARKINGFEES { get; set; }
        public decimal MEMBERSHIPFEES { get; set; }
        public decimal? TOTAL { get; set; }
        public int? FORYEAR { get; set; }
        public int? MONTH { get; set; }
        public int BUILDINGID { get; set; }
        public string OfficeNO { get; set; }
    }

    public class OfficeFloorDetailsModel
    {
        public int? OfficeFloorDetailID { get; set; }
        public officefloordetail OfficeFloorDetails { get; set; }

    }
    public class YearAndMont
    {
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
