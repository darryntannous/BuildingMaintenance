using BuildingMaintenance.Enums;
using BMDBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMaintenance.CommonCode.Helpers
{
    public static class GlobalVariables
    {
        public static List<int> AdminRoleIDs = new List<int>() { (int)RolesEnum.Admin, (int)RolesEnum.SuperAdmin };
        public static List<cpsidemenunavigation> cpsidemenunavigations = Services.ControllerServices.Instance.GetCPSideMenus();
    }
}
