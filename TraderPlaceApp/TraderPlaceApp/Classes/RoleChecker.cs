using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Business_Logic;
using TraderPlaceApp.Models;
using System.Web.Security;

namespace TraderPlaceApp.Classes
{
    public class RoleChecker 
    {
        public bool checkIfAdmin(string Username)
        {
            User u = new UsersBL().GetUserByUserName(Username);
            List<Role> userRoles = new RolesBL().GetUserRoles(u).ToList();

            bool userIsInRole = false;
            foreach (Role r in userRoles)
            {
                if (r.RoleID == 1)
                {
                    userIsInRole = true;
                }
            }

            return userIsInRole; // if the role id and the permission id are in the permission table then he is an admin
        }

        public bool checkIfSeller(string Username)
        {
            User u = new UsersBL().GetUserByUserName(Username);
            List<Role> userRoles = new RolesBL().GetUserRoles(u).ToList();

            bool userIsInRole = false;
            foreach (Role r in userRoles)
            {
               
                if (r.RoleID == 3)
                {
                    userIsInRole = true;
                }
            }

            return userIsInRole;
        }

        public bool checkIfBuyer(string Username)
        {
            User u = new UsersBL().GetUserByUserName(Username);
            List<Role> userRoles = new RolesBL().GetUserRoles(u).ToList();

            bool userIsInRole = false;
            foreach (Role r in userRoles)
            {


                if (r.RoleID == 2)
                {
                    userIsInRole = true;
                }
            }

            return userIsInRole;
        }

    }
}
