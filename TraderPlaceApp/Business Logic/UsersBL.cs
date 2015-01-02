using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data_Access;
using Common;
using Common.CustomEeptions;

using System.Data.Common;

namespace Business_Logic
{
   public class UsersBL
    {

        public IEnumerable<User> GetAllUsers()
        {

            return new UserRepository().GetAllUsers();

        }

        public User GetUserByID(string userName)
        {
            return new UserRepository().GetUserByUserName(userName);
        }

        public User GetUserByUser(User u)
        {
            return new UserRepository().GetUserByUserName(u.UserName);
        }

        public void DeleteUserDropAllRoles(string userName)
        {
            List<Role> rlist = new RoleRepository().GetRoles().ToList();

            List<Product> plist = new ProductRepository().GetProductByUser(userName).ToList();

            foreach (Product p in plist)
            {
                new ProductRepository().DeleteProductByID(p.ProductID);
            }

            foreach (Role zr in rlist)
            {
                if (new RoleRepository().IsInRole(new UserRepository().GetUserByUserName(userName), zr))
                {
                    UserRepository ur = new UserRepository();
                    RoleRepository rr = new RoleRepository();
                    ur.entities = rr.entities;

                    User u = ur.GetUserByUserName(userName);
                    Role r = rr.GetRoleById(zr.RoleID);
                    rr.DeallocateUser(u, r);
                }
            }
            new UserRepository().DeleteUser(userName);
        }

        public void Update(User gb)
        {
            new UserRepository().UpdateUser(gb);
        }

        public void CreateUser(User u)
        {
            UserRepository x = new UserRepository();

            if (!x.DoesUserNameExist(u.UserName))
            {
                x.AddUser(u);

                RoleRepository rr = new RoleRepository();
                x.entities = rr.entities;
                x.entities.Connection.Open();

                DbTransaction transaction = x.entities.Connection.BeginTransaction();
                try
                {
                    
                    AddDefaultRole(u.UserName);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    
                }
                finally
                {
                    x.entities.Connection.Close();
                }
            }
        }

        public bool isAuthenticated(string username, string password)
        {
            return new UserRepository().isAuthenticated(username, password);
        }

        public void AddDefaultRole(string username)
        {
            UserRepository ur = new UserRepository();
            RoleRepository rr = new RoleRepository();
            ur.entities = rr.entities;
            User u = ur.GetUserByUserName(username);
            Role r = rr.GetRoleById(2);
            rr.AllocateRole(u, r);
        }

        public void BecomeSeller(string username)
        {
            UserRepository ur = new UserRepository();
            RoleRepository rr = new RoleRepository();
            ur.entities = rr.entities;
            User u = ur.GetUserByUserName(username);
            Role r = rr.GetRoleById(3);
            rr.AllocateRole(u, r);
        }

        public bool DoesUserNameExist(string username)
        {
            return new UserRepository().DoesUserNameExist(username);
        }

        public User GetUserByUserName(string userName)
        {
            return new UserRepository().GetUserByUserName(userName);

        }
        
    }
}
