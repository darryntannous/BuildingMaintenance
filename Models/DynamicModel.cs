using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMaintenance.Models
{
    public class DynamicModel
    {
    }

    public class DynamicDropDownModel
    {
        public string TableName { get; set; }
        public string PrimaryColumnID { get; set; }
        public object PrimaryColumnValue { get; set; }
        public List<DropDownData> DropDownData { get; set; }
    }
    public class DropDownData
    {
        public string DisplayText { get; set; }
        public object Value { get; set; }
        public object Attribute1 { get; set; }
    }
}
