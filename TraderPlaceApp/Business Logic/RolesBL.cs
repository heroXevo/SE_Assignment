using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Data_Access;

namespace Business_Logic
{
    public class RolesBL
    {

        public void CreateRole(Role r)
        {
            
            new RoleRepository().CreateRole(r);
        }

        public Role GetRoleByID(int id)
        {
            return new RoleRepository().GetRoleById(id);
        }

        public void UpdateRole(Role gb)
        {
            new RoleRepository().UpdateRole(gb);
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return new RoleRepository().GetRoles();
        }

        public bool IsInRole(string userName, int roleId)
        {

            RoleRepository rr = new RoleRepository();
            UserRepository ur = new UserRepository();
            rr.entities = ur.entities;

            return rr.IsInRole(
                ur.GetUserByUserName(userName),
                rr.GetRoleById(roleId));
        }

        public IEnumerable<Role> GetUserRoles(User u)
        {
            return new RoleRepository().GetUserRoles(u);
        }

        public void dropRole(string userName, int roleid)
        {
            UserRepository ur = new UserRepository();
            RoleRepository rr = new RoleRepository();
            ur.entities = rr.entities;

            User u = ur.GetUserByUserName(userName);
            Role r = rr.GetRoleById(roleid);
            rr.DeallocateUser(u, r);
        }

        public void AllocateRole(string userName , int roleid)
        {
            UserRepository ur = new UserRepository();
            RoleRepository rr = new RoleRepository();

            ur.entities = rr.entities;
            User u = ur.GetUserByUserName(userName);
            Role r = rr.GetRoleById(roleid);
            rr.AllocateRole(u, r);
        }

        public void AllocatePermission(int permissionid, int roleid)
        {
            RoleRepository role = new RoleRepository();
            PermissionRepository permission = new PermissionRepository();
            role.entities = permission.entities;


            Role r = role.GetRoleById(roleid);
            Permission p = permission.GetPermissionById(permissionid);

            permission.AllocatePermission(p, r);
        }


        public void dropRoleandPermissionsAllocated(int id)
        {
            Role r = new RoleRepository().GetRoleById(id);
            List<Permission> plist = new PermissionRepository().GetRolePermissions(r).ToList();

            foreach (Permission zr in plist)
            {
                if (new PermissionRepository().IsInPermission(zr, r))
                {
                    PermissionRepository pr = new PermissionRepository();
                    RoleRepository rr = new RoleRepository();

                    pr.entities = rr.entities;

                    Permission p = pr.GetPermissionById(zr.PermissionID);
                    Role rol = rr.GetRoleById(id);
                    pr.DeallocatePermission(p, rol);
                }
            }

            new RoleRepository().DeleteRole(id);
        }

    }
}
