using BMDBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMaintenance.Models
{
    public class OfficeModel
    {
        public int? OfficeID { get; set; }
        public List<office> Offices { get; set; }
        public bool IsNewRecord { get { return !this.OfficeID.HasValue; } }
        public DynamicDropDownModel OfficeStatuses { get; set; }
        public DynamicDropDownModel User { get; set; }

    }

    public class OfficeStatusModel
    {
        public int? OfficeStatusID { get; set; }
        public List<officestatus> OfficeStatuses { get; set; }
        public bool IsNewRecord { get { return !this.OfficeStatusID.HasValue; } }
    }

    public class OfficeFloorModel
    {
        public int? OfficeFloorID { get; set; }
        public List<officefloor> OfficeFloors { get; set; }
        public bool IsNewRecord { get { return !this.OfficeFloorID.HasValue; } }
        public DynamicDropDownModel Floors { get; set; }
        public DynamicDropDownModel Offices { get; set; }
        public DynamicDropDownModel Buildings { get; set; }
        public DynamicDropDownModel OfficeStatuses { get; set; }



    }
}
