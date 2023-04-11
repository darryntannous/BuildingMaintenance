using BMDBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMaintenance.Models
{
    public class FloorModel
    {
        public int? FloorID { get; set; }
        public List<floor> Floors { get; set; }
        public bool IsNewRecord { get { return !this.FloorID.HasValue; } }
    }
}
