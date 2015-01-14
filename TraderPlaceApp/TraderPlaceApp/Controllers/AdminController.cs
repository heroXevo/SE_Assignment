using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Data_Access;
using TraderPlaceApp.Models;
using TraderPlaceApp.Classes;
using System.Web.Security;
using Business_Logic;

namespace TraderPlaceApp.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            if (User.Identity.Name != string.Empty)
            {

                if (new RoleChecker().checkIfAdmin(User.Identity.Name))
                {

                    return View();

                }

            }

            return Redirect("~/?msg=noaccess");
        }

        public ActionResult UserList()
        {

            if (User.Identity.Name != string.Empty)
            {

                if(new RoleChecker().checkIfAdmin(User.Identity.Name))
                {

                    List<User> AllUsers = new UsersBL().GetAllUsers().ToList();
                    return View("UserList", AllUsers);

                }

            }
            return Redirect("~/?msg=noaccess");

        }

        public ActionResult DeleteUser(string UserName)
        {

            try
            {

                new UsersBL().DeleteUserDropAllRoles(UserName);
                return Redirect("~/admin/UserList?msg=UserDeleted");

            }
            catch
            {

                return Redirect("~/admin/UserList?msg=UserNotDeleted");

            }

        }

        public ActionResult EditUser(string UserName)
        {
            if (new RoleChecker().checkIfAdmin(User.Identity.Name))
            {
                RegisterModel rm = new RegisterModel();
                User u = new UsersBL().GetUserByUserName(UserName);

                
                rm.UserName = u.UserName;
                rm.Name = u.Name;
                rm.surName = u.Surname;
                rm.Password = "";
                rm.Age = u.Age;
                rm.Address = u.Address;
                             
               

                return View(rm);
            }
            else
            {
                return Redirect("~/?msg=noaccess");
            }
        }

        [HttpPost]
        public ActionResult EditUser(RegisterModel rm)
        {
            try
            {
                User u = new UsersBL().GetUserByUserName(rm.UserName);
                if ((new UsersBL().DoesUserNameExist(rm.UserName)) && (u.UserName != rm.UserName))
                { return Redirect("/admin/UserList?msg=usernametaken"); }
                else
                {
                    //u = new User();
                    
                    u.UserName = u.UserName;
                    u.Name = rm.Name;
                    u.Surname = rm.surName;
                    u.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(rm.Password, "MD5");
                    u.Age = rm.Age;
                    u.TownID = 7;
                    u.Address = rm.Address;
                    
                    new UsersBL().Update(u);

                    return RedirectToAction("UserList", "Admin");
                }
            }
            catch
            {
                return Redirect("/admin/UserList?msg=nochangessaved");
            }
        }

        public ActionResult RoleList()
        {

            List<Role> RoleList = new RolesBL().GetAllRoles().ToList();

            RoleModel model = new RoleModel();

            List<RoleModel> modelList = new List<RoleModel>();

            foreach (Role r in RoleList)
            {

                model.RoleName = r.Role_Name;
                model.RoleID = r.RoleID;

                modelList.Add(model);

                model = new RoleModel();

            }

            return View(modelList);

        }

        public ActionResult CreateRole()
        {

            return View(new RoleModel());

        }

        [HttpPost]
        public ActionResult CreateRole(RoleModel rm)
        {

            try
            {
                Role r = new Role();

                r.Role_Name = rm.RoleName;

                new RolesBL().CreateRole(r);
                return Redirect("/admin/RoleList/?msg=success");
            }
            catch
            {

                return Redirect("/admin/RoleList/?msg=RoleWasNotAdded");

            }

        }

        public ActionResult editRole(int roleId)
        {

            RoleModel rm = new RoleModel();
            Role r = new RolesBL().GetRoleByID(roleId);

            rm.RoleID = r.RoleID;
            rm.RoleName = r.Role_Name;

            return View(rm);

        }

        [HttpPost]
        public ActionResult editRole(RoleModel rm)
        {

            try
            {

                Role r = new Role();
                r.RoleID = rm.RoleID;
                r.Role_Name = rm.RoleName;

                new RolesBL().UpdateRole(r);

                return Redirect("/admin/RoleList/?msg=success");


            }
            catch
            {

                return Redirect("/admin/RoleList/?msg=RoleWasNotUpdated");

            }



        }

        public ActionResult DeleteRole(int roleID)
        {

            try
            {

                new RolesBL().dropRoleandPermissionsAllocated(roleID);
                return Redirect("/admin/RoleList/?msg=success");

            }
            catch
            {

                return Redirect("/admin/RoleList/?msg=RoleWasNotDeleted");

            }

        }

    }
}
