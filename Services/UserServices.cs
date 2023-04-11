using BMDBConnection;
using BuildingMaintenance.Models;
using BuildingMaintenance.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMaintenance.Services
{
    public class UserServices
    {
        #region Define as Singleton
        private static UserServices _Instance;

        public static UserServices Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new UserServices();
                }

                return (_Instance);
            }
        }

        private UserServices()
        {
        }
        #endregion

        public ApplicationUser GetUserByEmail(string email, string password)
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                return context.FirstOrDefault<ApplicationUser>("SELECT U.* FROM users U WHERE U.Email = @0 AND U.Password = @1 AND UserStatusID = @2 AND IsCPUser = @2", email, password, true);
            }
        }

        public ApplicationUser GetUserByEmailAndName(string email, string UserName)
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                return context.FirstOrDefault<ApplicationUser>("SELECT U.* FROM users U WHERE U.Email = @0 OR U.UserName = @1", email, UserName, true);
            }
        }

        public void RegisterUser(ApplicationUser user)
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                context.Insert(user);
            }
        }

        public List<UserRoleEntity> GetUserRoles(int userID)
        {
            using (var context = DataContextHelper.GetCPDataContext())
            {
                var cpSql = PetaPoco.Sql.Builder.Select(@"UR.UserID, UR.RoleID, R.RoleName, RP.PermissionID, RP.CPSideMenuNavigationID, P.PermissionName")
                                .From("userroles UR")
                                .InnerJoin("roles R").On("R.RoleID = UR.RoleID")
                                .LeftJoin("roles_permissions RP").On("RP.RoleID = UR.RoleID AND RP.IsActive = @0", true)
                                .LeftJoin("permissions P").On("P.PermissionID = RP.PermissionID AND P.IsActive = @0", true)
                                .Where("UserID = @0 AND UR.IsActive = @1 AND R.RecordStatusID = @1", userID, true);
                return context.Fetch<UserRoleEntity>(cpSql);
            }
        }

    }
}
