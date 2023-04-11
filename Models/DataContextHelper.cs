using BMDBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMaintenance.Models
{
    public class DataContextHelper
    {
        private static BMDBConnectionDB GetNewDataContext(string connectionStringName, bool enableAutoSelect)
        {
            BMDBConnectionDB repository = new BMDBConnectionDB(connectionStringName);
            repository.EnableAutoSelect = enableAutoSelect;
            //repository.ELHelperInstance = elHelperInstance;

            return (repository);
        }

        public static BMDBConnectionDB GetCPDataContext(bool enableAutoSelect = true)
        {
            return (GetNewDataContext("BMDBConnection", enableAutoSelect));
        }
    }
}
