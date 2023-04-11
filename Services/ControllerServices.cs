using BMDBConnection;
using BuildingMaintenance.Enums;
using BuildingMaintenance.Models;
using BuildingMaintenance.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMaintenance.Services
{
    public class ControllerServices
    {
        #region Define as Singleton
        private static ControllerServices _Instance;

        public static ControllerServices Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ControllerServices();
                }

                return (_Instance);
            }
        }

        private ControllerServices()
        {
        }
        #endregion

        public List<society> GetSocieties(int? societyID, bool? isActive)
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                var ppSql = PetaPoco.Sql.Builder.Select(@"S.*")
                            .From("societies S");
                if (societyID.HasValue)
                {
                    ppSql = ppSql.Where("S.SocietyID = @0", societyID);
                }
                if (isActive.HasValue)
                {
                    ppSql = ppSql.Where("S.IsActive = @0", isActive);
                }
                return context.Fetch<society>(ppSql).ToList();
            }
        }

        public List<building> GetBuildings(int? buildingID, bool? isActive)
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                var ppSql = PetaPoco.Sql.Builder.Select(@"*")
                            .From("buildings");
                if (buildingID.HasValue)
                {
                    ppSql = ppSql.Where("BuildingID = @0", buildingID);
                }
                if (isActive.HasValue)
                {
                    ppSql = ppSql.Where("IsActive = @0", isActive);
                }
                return context.Fetch<building>(ppSql).ToList();
            }
        }

        public List<floor> GetFloors(int? floorID, bool? isActive)
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                var ppSql = PetaPoco.Sql.Builder.Select(@"*")
                            .From("floors");
                if (floorID.HasValue)
                {
                    ppSql = ppSql.Where("FloorID = @0", floorID);
                }
                if (isActive.HasValue)
                {
                    ppSql = ppSql.Where("IsActive = @0", isActive);
                }
                return context.Fetch<floor>(ppSql).ToList();
            }
        }

        public List<office> GetOffices(int? officeID)
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                var ppSql = PetaPoco.Sql.Builder.Select(@"*")
                            .From("offices");
                if (officeID.HasValue)
                {
                    ppSql = ppSql.Where("OfficeID = @0", officeID);
                }
                return context.Fetch<office>(ppSql).ToList();
            }
        }

        public List<officestatus> GetOfficeStatuses(int? officeStatusID, bool? isActive)
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                var ppSql = PetaPoco.Sql.Builder.Select(@"*")
                            .From("officestatuses");
                if (officeStatusID.HasValue)
                {
                    ppSql = ppSql.Where("OfficeStatusID = @0", officeStatusID);
                }
                if (isActive.HasValue)
                {
                    ppSql = ppSql.Where("IsActive = @0", isActive);
                }
                return context.Fetch<officestatus>(ppSql).ToList();
            }
        }

        public List<officefloor> GetOfficeFloors(int? officeFloorID)
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                var ppSql = PetaPoco.Sql.Builder.Select(@"*")
                            .From("officefloors");
                if (officeFloorID.HasValue)
                {
                    ppSql = ppSql.Where("OfficeFloorID = @0", officeFloorID.Value);
                }
                return context.Fetch<officefloor>(ppSql).ToList();
            }
        }

        public List<SearchStatisticsEntity> GetOfficeFloorsSummary(int buildingID, int year, int month, int? floorID, short? officeStatusID)
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                var ppSql = PetaPoco.Sql.Builder.Select(@"FloorID, SUM(MonthlyAmount) MonthlyAmount, SUM(Contribution) Contribution, SUM(ParkingFees) ParkingFees, SUM(MembershipFees) MembershipFees, SUM(Total) Total")
                            .From("officefloors OFl")
                            .LeftJoin("officefloordetails ofd").On("ofd.OfficeFloorID = OFl.OfficeFloorID AND ofd.ForYear = @0 AND ofd.Month = @1", year, month)
                            .Where("BuildingID = @0", buildingID);
                if (floorID.HasValue && floorID.Value > 0)
                {
                    ppSql = ppSql.Where("OFl.FloorID = @0", floorID.Value);
                }
                if (officeStatusID.HasValue && officeStatusID.Value > 0)
                {
                    ppSql = ppSql.Where("OFl.OfficeStatusID = @0", officeStatusID.Value);
                }
                ppSql = ppSql.GroupBy("OFl.FloorID");
                return context.Fetch<SearchStatisticsEntity>(ppSql).ToList();
            }
        }


        public List<buildinguser> GetBuildingUsers(int? userID)
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                var ppSql = PetaPoco.Sql.Builder.Select(@"*")
                            .From("buildingusers");
                if (userID.HasValue)
                {
                    ppSql = ppSql.Where("UserID = @0", userID);
                }
                return context.Fetch<buildinguser>(ppSql).ToList();
            }
        }

        public List<officefloordetail> GetOfficeFloorDetails(int forYear, int month)
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                var ppSql = PetaPoco.Sql.Builder.Select(@"*")
                            .From("officefloordetails")
                            .Where("ForYear = @0 AND Month = @1", forYear, month);
                return context.Fetch<officefloordetail>(ppSql).ToList();
            }
        }

        public List<buildinguser> GetSearch(int? userID)
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                var ppSql = PetaPoco.Sql.Builder.Select(@"")
                            .From("buildingusers");
                if (userID.HasValue)
                {
                    ppSql = ppSql.Where("UserID = @0", userID);
                }
                return context.Fetch<buildinguser>(ppSql).ToList();
            }
        }

        public List<BillingData> GetBillingInformation(int monthID, int Year, int OfficeID, int BuildingID)
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                var ppSql = PetaPoco.Sql.Builder.Select(@"O.OfficeID , O.FloorID , O.BUILDINGID,O.OfficeNO , O.Space  , O.OfficeFloorID , OFD.OfficeFloorDetailID, F.FLOORRATE * O.Space AS MonthlyAmount , OFD.PAIDDATE , OFD.CONTRIBUTION,OFD.PARKINGFEES,OFD.MEMBERSHIPFEES,OFD.TOTAL,OFD.FORYEAR,OFD.MONTH")
                            .From("OfficeFloors O")
                            .InnerJoin("Floors F")
                            .On("F.FLOORID = O.FLOORID")
                            .LeftJoin("OfficeFloorDetails OFD")
                            .On("O.OfficeFloorID = OFD.OfficeFloorID");
                if (monthID > 0)
                {
                    ppSql = ppSql.Append("AND OFD.MONTH  = @0", monthID);
                }
                if (Year > 0)
                {
                    ppSql = ppSql.Append("AND OFD.FORYEAR  = @0", Year);
                }
                if (OfficeID > 0)
                {
                    ppSql = ppSql.Where("O.OfficeID  = @0", OfficeID);
                }
                if (BuildingID > 0)
                {
                    var condition = OfficeID > 0 ? "AND O.BuildingID = @0" : "WHERE  O.BuildingID = @0";
                    ppSql = ppSql.Append(condition, BuildingID);
                }
                return context.Fetch<BillingData>(ppSql).ToList();
            }
        }

        public List<officefloordetail> GetBillingStats(int buildingID, List<YearAndMont> yearMonths)
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                var ppSql = PetaPoco.Sql.Builder.Select(@"ForYear, month, SUM(MonthlyAmount) MonthlyAmount, SUM(Contribution) Contribution, SUM(ParkingFees) ParkingFees, SUM(MembershipFees) MembershipFees, SUM(Total) Total")
                           .From("OfficeFloorDetails OFD")
                           .InnerJoin("officefloors OFl").On("OFl.OfficeFloorID = OFD.OfficeFloorID")
                           .Where("OFl.BuildingID = @0 AND OFl.OfficeStatusID = @1", buildingID, (int)OfficeStatusesEnum.Open);
                if(yearMonths.Count > 0)
                {
                    int count = 0;
                    ppSql = ppSql.Append(" AND ( ");
                    foreach (var item in yearMonths)
                    {
                        ppSql = ppSql.Append("(OFD.Month = @0 AND OFD.ForYear = @1)", item.Month, item.Year);
                        count++;
                        if(yearMonths.Count > count)
                        {
                            ppSql = ppSql.Append(" OR ");
                        }
                    }
                    ppSql = ppSql.Append(" ) ");
                }
                ppSql = ppSql.GroupBy("OFD.ForYear, OFD.Month");
                return context.Fetch<officefloordetail>(ppSql).ToList();
            }
        }

        public int GetNumberOfOffices(int buildingID)
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                var ppSql = PetaPoco.Sql.Builder.Select(@"COUNT(OfficeID)")
                           .From("OfficeFloorDetails OFD")
                           .InnerJoin("officefloors OFl").On("OFl.OfficeFloorID = OFD.OfficeFloorID")
                           .Where("OFl.BuildingID = @0 AND OFl.OfficeStatusID = @1", buildingID, (int)OfficeStatusesEnum.Open);
                return context.FirstOrDefault<int>(ppSql);
            }
        }

        public int GetUnpaidOfficesByFiscal(int buildingID, YearAndMont yearMonth)
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                var ppSql = PetaPoco.Sql.Builder.Select(@"COUNT(OFl.OfficeFloorID)")
                           .From("officefloors OFl")
                           .LeftJoin("OfficeFloorDetails OFD").On("OFD.OfficeFloorID = OFl.OfficeFloorID  AND OFD.Month = @0 AND OFD.ForYear = @1", yearMonth.Month, yearMonth.Year)
                           .Where("OFl.BuildingID = @0 AND OFl.OfficeStatusID = @1", buildingID, (int)OfficeStatusesEnum.Open);
                return context.FirstOrDefault<int>(ppSql);
            }
        }
        public List<cpsidemenunavigation> GetCPSideMenus()
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                var ppSql = PetaPoco.Sql.Builder.Select(@"*")
                            .From("cpsidemenunavigations")
                            .Where("IsActive = 1");
                return context.Fetch<cpsidemenunavigation>(ppSql).ToList();
            }
        }
    }
}
