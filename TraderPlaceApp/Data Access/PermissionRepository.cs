using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Data_Access
{
    public class PermissionRepository :ConnectionClass
    {

        public PermissionRepository()
            : base()
        {
        }

        public IEnumerable<Permission> GetPermissions()
        {
            return entities.Permissions;
        }

        public Permission GetPermissionById(int id)
        {
            return entities.Permissions.SingleOrDefault(x => x.PermissionID == id);
        }

        public void AllocatePermission(Permission p, Role r)
        {
            p.Roles.Add(r);
            entities.SaveChanges();
        }

        public void DeallocatePermission(Permission p, Role r)
        {
            p.Roles.Remove(r);
            entities.SaveChanges();
        }

        public bool IsInPermission(Permission p, Role r)
        {
            if (p.Roles.SingleOrDefault(x => x.RoleID == r.RoleID) == null)
                return false;
            else return true;
        }

        public IEnumerable<Permission> GetRolePermissions(Role r)
        {
            return r.Permissions;
        }


    }
}
