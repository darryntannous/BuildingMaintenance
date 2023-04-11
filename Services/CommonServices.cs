using BMDBConnection;
using BuildingMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMaintenance.Services
{
    public class CommonServices
    {
        #region Define as Singleton
        private static CommonServices _Instance;

        public static CommonServices Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CommonServices();
                }

                return (_Instance);
            }
        }

        private CommonServices()
        {
        }
        #endregion

        public int DynamicInsert(string tableName, string primaryKey, Dictionary<string, object> dictionary)
        {
            using (var context = DataContextHelper.GetCPDataContext(false))
            {
                string columnNames = string.Empty, columnValues = string.Empty;
                foreach (var item in dictionary)
                {
                    string[] keyWithType = item.Key.Split(new string[] { "##" }, StringSplitOptions.RemoveEmptyEntries);
                    columnNames += keyWithType[0] + ",";
                    if (Convert.ToBoolean(keyWithType[1]))
                    {
                        columnValues += "N'" + item.Value + "',";
                    }
                    else
                    {
                        columnValues += item.Value + ",";
                    }
                }
                columnNames = columnNames.TrimEnd(',');
                columnValues = columnValues.TrimEnd(',');
                var ppSql = string.Format("INSERT INTO {0} ({1}) VALUES ({3}); SELECT LAST_INSERT_ID();", tableName, columnNames, primaryKey, columnValues);
                return context.ExecuteScalar<int>(ppSql, null);
            }
        }

        public void DynamicUpdateWithCustom(string tableName, string primaryKey, object primaryKeyValue, Dictionary<string, object> dictionary)
        {
            using (var context = DataContextHelper.GetCPDataContext(false))
            {
                string columnNamesWithValues = string.Empty;
                Dictionary<string, object> poco = new Dictionary<string, object>();
                foreach (var item in dictionary)
                {
                    string[] keyWithType = item.Key.Split(new string[] { "##" }, StringSplitOptions.RemoveEmptyEntries);
                    columnNamesWithValues += keyWithType[0] + " = ";
                    if (Convert.ToBoolean(keyWithType[1]))
                    {
                        columnNamesWithValues += "N'" + item.Value + "',";
                    }
                    else
                    {
                        columnNamesWithValues += item.Value + ",";
                    }
                }
                columnNamesWithValues = columnNamesWithValues.TrimEnd(',');
                var ppSql = string.Format("UPDATE {0} SET {1} WHERE {2} = {3}", tableName, columnNamesWithValues, primaryKey, primaryKeyValue);
                context.Execute(ppSql, null);
                //context.Update(tableName, primaryKey, poco, primaryKeyValue, columns);
            }
        }

        public void DynamicDelete(string tableName, string column, object id)
        {
            using (var context = DataContextHelper.GetCPDataContext(false))
            {
                context.Execute(string.Format("Delete From {0} Where {1} = @0", tableName, column), id);
            }
        }

        public List<cpsidemenunavigation> GetCPSideMenuNavigations()
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                var ppSql = PetaPoco.Sql.Builder.Select(@"CPSMN.*")
                            .From("cpsidemenunavigations CPSMN")
                            .Where("CPSMN.IsActive = @0", true);
                return context.Fetch<cpsidemenunavigation>(ppSql).ToList();
            }
        }
    }
}
