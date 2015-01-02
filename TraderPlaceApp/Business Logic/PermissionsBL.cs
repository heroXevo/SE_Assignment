using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Data_Access;

namespace Business_Logic
{
    public class PermissionsBL
    {

        public IEnumerable<Permission> GetAllPermissions()
        {
            return new PermissionRepository().GetPermissions();
        }



        public bool IsInPermission(int permissionId, int roleId)
        {

            RoleRepository rr = new RoleRepository();
            PermissionRepository pr = new PermissionRepository();
            rr.entities = pr.entities;

            return pr.IsInPermission(
                pr.GetPermissionById(permissionId),
                rr.GetRoleById(roleId));
        }

        public IEnumerable<Permission> GetRolePermissions(Role r)
        {
            return new PermissionRepository().GetRolePermissions(r);
        }

        public void dropPermission(int permissionid, int roleid)
        {
            PermissionRepository pr = new PermissionRepository();
            RoleRepository rr = new RoleRepository();
            pr.entities = rr.entities;

            Permission p = pr.GetPermissionById(permissionid);
            Role r = rr.GetRoleById(roleid);
            pr.DeallocatePermission(p, r);
        }
    }
}
