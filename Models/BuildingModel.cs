using BMDBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMaintenance.Models
{
    public class BuildingModel
    {
        public int? BuildingID { get; set; }
        public List<building> Buildings { get; set; }
        public bool IsNewRecord { get { return !this.BuildingID.HasValue; } }
        public DynamicDropDownModel Societies { get; set; }
    }
}
