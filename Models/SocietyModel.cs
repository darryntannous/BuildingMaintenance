using BMDBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMaintenance.Models
{
    public class SocietyModel
    {
        public int? SocietyID { get; set; }
        public List<society> Societies { get; set; }
        public bool IsNewRecord { get { return !this.SocietyID.HasValue; } }
    }
}
