using BMDBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMaintenance.Models
{
    public class BuildingUserModel
    {
        public int? UserID { get; set; }
        public List<buildinguser> BuildingUsers { get; set; }
        public bool IsNewRecord { get { return !this.UserID.HasValue; } }
    }
}
