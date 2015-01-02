using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;


namespace Data_Access
{
    public class RoleRepository : ConnectionClass
    {

         public RoleRepository()
            : base()
        {
        }

        public void CreateRole(Role entry)
        {
            entities.AddToRoles(entry);
            entities.SaveChanges();
        }

        public IEnumerable<Role> GetRoles()
        {
            return entities.Roles;
        }

        public void DeleteRole(int roleid)
        {
            entities.DeleteObject(GetRoleById(roleid)); 
            entities.SaveChanges();
        }

        public void UpdateRole(Role gb)
        {
            entities.Roles.Attach(GetRoleById(gb.RoleID)); 
            entities.Roles.ApplyCurrentValues(gb); 
            entities.SaveChanges(); 
        }


        public IEnumerable<Role> GetUserRoles(User u)
        {
            return u.Roles;
        }

        public Role GetRoleById(int id)
        {
            return entities.Roles.SingleOrDefault(x => x.RoleID == id);
        }

        public void AllocateRole(User u, Role r)
        {
            u.Roles.Add(r);
            entities.SaveChanges();
        }

        public void DeallocateRole(User u, Role r)
        {
            u.Roles.Remove(r);
            entities.SaveChanges();
        }

        public void DeallocateUser(User u, Role r)
        {
            r.Users.Remove(u);
            entities.SaveChanges();
        }


        public bool IsInRole(User u, Role r)
        {
            if (u.Roles.SingleOrDefault(x => x.RoleID == r.RoleID) == null)
                return false;
            else return true;
        }

    }
}
