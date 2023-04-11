using BMDBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMaintenance.Models
{
    public class HomeModel
    {
        public DateTime CurrentDate { get; set; }
        public decimal? CollectedAmount { get; set; }
        public List<officefloordetail> OfficeFloorDetails { get; set; }
        public int NoOfOffices { get; set; }
        public int UnPaidNoOfOffices { get; set; }

    }
}
