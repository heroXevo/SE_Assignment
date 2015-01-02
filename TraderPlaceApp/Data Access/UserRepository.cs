using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Data_Access
{
    public class UserRepository : ConnectionClass
    {

        public UserRepository() : base() { }

        public User GetUserByUserName(string UserName)
        {

            return entities.Users.SingleOrDefault(x => x.UserName == UserName);
        }

        public void AddUser(User newUser)
        {

            if (GetUserByUserName(newUser.UserName) != null)
            {

                //Username already exists

            }
            else
            {

                entities.AddToUsers(newUser);
                entities.SaveChanges();

            }

        }

        public IEnumerable<User> GetAllUsers()
        {

            return entities.Users.AsEnumerable();

        }

        public void DeleteUser(string userName)
        {

            entities.DeleteObject(GetUserByUserName(userName));
            entities.SaveChanges();

        }

        public void UpdateUser(User updateUser)
        {
            entities.Users.Attach(GetUserByUserName(updateUser.UserName));
            entities.Users.ApplyCurrentValues(updateUser);
            entities.SaveChanges();
        }

        public bool IsAuthenticationValid(string userName, string passWord)
        {

            if (entities.Users.SingleOrDefault(x => x.UserName == userName && x.Password == passWord) == null)
            {

                return false;

            }
            else return true;

        }

        public bool DoesUserNameExist(string UserName)
        {

            if (entities.Users.Count(user => user.UserName == UserName) == 0)
                return false;
            else
                return true;
        }

        public bool isAuthenticated(string userName, string password)
        {

            if(entities.Users.Count(x => x.UserName == userName && x.Password == password) > 0)
                return true; //this means that the userName and password are correct
            else return false; //this means that the credentials are incorrect

        }
    }
}
