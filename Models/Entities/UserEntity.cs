using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BMDBConnection;
using Microsoft.AspNetCore.Identity;
using PetaPoco;

namespace BuildingMaintenance.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [ResultColumn] public int UserID { get; set; }
        [ResultColumn] public string FirstName { get; set; }
        [ResultColumn] public string LastName { get; set; }
        //[ResultColumn] public string UserName { get; set; }
        [ResultColumn] public string DisplayName { get; set; }
        //[ResultColumn] public string Email { get; set; }
        [ResultColumn] public string Password { get; set; }
        [ResultColumn] public short UserStatusID { get; set; }
        [ResultColumn] public bool IsCPUser { get; set; }
        [ResultColumn] public int? ParentUserID { get; set; }

        public List<int> RoleIDs { get; set; }
        public bool IsAdminUser { get; set; }
        public List<int> CPSideMenuNavigationIDs { get; set; }

        public List<UserRoleEntity> UserRoles = new List<UserRoleEntity>();

    }

    public class UserRoleEntity
    {
        [ResultColumn]
        public int UserID { get; set; }

        [ResultColumn]
        public int RoleID { get; set; }

        [ResultColumn]
        public int? PermissinoID { get; set; }

        [ResultColumn]
        public int? CPSideMenuNavigationID { get; set; }

        [ResultColumn]
        public string RoleName { get; set; }

        [ResultColumn]
        public string PermissionName { get; set; }


    }
}
