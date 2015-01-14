using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.Common;
using System.Text.RegularExpressions;


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

            if (GetRoleByName(entry.Role_Name) == null)
            {
                if (entry.Role_Name == "" || entry == null)
                {

                    throw new NullReferenceException("Role Name is empty");

                }
                else
                {
                    if (entry.Role_Name.Length < 3 || entry.Role_Name.Length >= 20)
                    {

                        throw new Exception("Role Name is too long or too short");

                    }
                    else
                    {

                        if (Regex.IsMatch(entry.Role_Name, @"^[a-zA-Z]+$"))
                        {

                            entities.AddToRoles(entry);
                            entities.SaveChanges();

                        }
                        else
                        {

                            throw new Exception("The name has some invalid characters");

                        }


                    }
                }



            }
            else
            {

                throw new NullReferenceException("Role Name Already Exists");

            }
            
           
        }

        public IEnumerable<Role> GetRoles()
        {
            return entities.Roles;
        }

        public void DeleteRole(int roleid)
        {
            if (roleid > 0)
            
            {

                if (GetRoleById(roleid) != null)
                {

                    entities.DeleteObject(GetRoleById(roleid));
                    entities.SaveChanges();
                }
                else
                {

                    throw new NullReferenceException("Role does not exists");

                }

            }
            else
            {
                throw new IndexOutOfRangeException("Role ID is 0 or less");
                
            }
            
            
        }

        public void UpdateRole(Role gb)
        {

            if (gb.Role_Name == "" || gb == null)
            {

                throw new ArgumentNullException("The role Name is empty");

            }
            else
            {
                if (GetRoleByName(gb.Role_Name) == null)
                {

                    if (gb.Role_Name.Length < 3 || gb.Role_Name.Length >= 20)
                    {

                        throw new Exception("Role Name is too long or too short");

                    }
                    else
                    {
                        if (Regex.IsMatch(gb.Role_Name, @"^[a-zA-Z]+$"))
                        {

                            entities.Roles.Attach(GetRoleById(gb.RoleID));
                            entities.Roles.ApplyCurrentValues(gb);
                            entities.SaveChanges();
                        }
                        else
                        {

                            throw new Exception("The name has some invalid characters");

                        }
                    }
                }
                else
                {
                    if (gb.RoleID == GetRoleByName(gb.Role_Name).RoleID)
                    {

                        if (gb.Role_Name.Length < 3 || gb.Role_Name.Length >= 20)
                        {

                            throw new Exception("Role Name is too long or too short");

                        }
                        else
                        {
                            if (Regex.IsMatch(gb.Role_Name, @"^[a-zA-Z]+$"))
                            {
                                entities.Roles.Attach(GetRoleById(gb.RoleID));
                                entities.Roles.ApplyCurrentValues(gb);
                                entities.SaveChanges();
                            }
                            else
                            {

                                throw new Exception("The name has some invalid characters");

                            }
                        }
                    }
                    else
                    {

                        throw new Exception("This role name already exists");
                    }

                }
            }
        }


        public IEnumerable<Role> GetUserRoles(User u)
        {
            return u.Roles;
        }

        public Role GetRoleById(int id)
        {

            return entities.Roles.SingleOrDefault(x => x.RoleID == id);
        }

        public Role GetRoleByName(string name)
        {
            if (name == "" || name == null)
            {
                throw new ArgumentNullException("The name is empty");
            }
            else
            {
                if (name.Length < 3 || name.Length >= 20)
                {

                    throw new Exception("Role Name is too long or too short");

                }
                else
                {

                    if (Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                    {

                        return entities.Roles.SingleOrDefault(x => x.Role_Name == name);
                    }
                    else
                    {
                        throw new Exception("The name has some invalid characters");
                    }
                }
            }

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

        public void AddRole(Role r, DbTransaction t)
        {

            
            
            entities.Roles.AddObject(r);
            t.Commit();

            

        }

    }
}
