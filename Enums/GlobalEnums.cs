using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMaintenance.Enums
{
    public enum RolesEnum : int
    {
        Admin = 1,
        SuperAdmin = 2,
        Floor = 3
    }
    public enum PermissionsEnum : short
    {
        All = 1,
        Create = 2,
        Read = 3,
        Update = 4,
        Delete = 5
    }
    public enum DisplayTypesEnum : short
    {
        
    }
    public enum OfficeStatusesEnum : short
    {
        Open = 1,
        Closed = 2
    }
    public enum RecordStatusesEnum : short
    {
        Active = 1,
        Closed = 2
    }
    public enum UserStatusesEnum : short
    {
        Active = 1,
        Deleted = 2,
        Block = 3
    }
    public enum ActionTypesEnum : short
    {
        Change = 0,
        Delete = 1,
        Same = 2
    }
}
