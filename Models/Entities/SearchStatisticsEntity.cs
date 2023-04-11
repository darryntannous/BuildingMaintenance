using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMaintenance.Models.Entities
{
    public class SearchStatisticsEntity 
    {
        public int FloorID { get; set; }
        public decimal? MonthlyAmount { get; set; }
        public decimal? Contribution { get; set; }
        public decimal? ParkingFees { get; set; }
        public decimal? MembershipFees { get; set; }
        public decimal? Total { get; set; }
    }
}
