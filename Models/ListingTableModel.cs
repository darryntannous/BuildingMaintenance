using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMaintenance.Models
{
    public class ListingTableModel
    {
        public List<string> Columns { get; set; }
        public List<Dictionary<string, object>> TableData { get; set; }
        public string TableName { get; set; }
        public string PrimaryColumn { get; set; } = string.Empty;
        public string EditURL { get; set; }
        public bool IsEditShow { get; set; } = true;
        public bool IsDeleteShow { get; set; } = true;

    }
}
