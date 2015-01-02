using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Data_Access;
using Business_Logic;
using TraderPlaceApp.Models;

namespace TraderPlaceApp.Controllers
{
    public class RoleController : Controller
    {
        //
        // GET: /Role/

        public ActionResult UserRoleList(string userName)
        {

            User u = new UsersBL().GetUserByUserName(userName);
            List<Role> userRoles = new RolesBL().GetUserRoles(u).ToList();

            RoleModel rMod = new RoleModel();
            List<RoleModel> rModelList = new List<RoleModel>();

            foreach(Role r in userRoles)
            {

                rMod.RoleName = r.Role_Name;
                rMod.RoleID = r.RoleID;
                rMod.UserName = userName;

                rModelList.Add(rMod);
                rMod = new RoleModel();

            }

            return View(rModelList);

        }

        public ActionResult DeallocateRole(string UserName, int roleID)
        {

            try
            {
                new RolesBL().dropRole(UserName, roleID);
                return Redirect("/Admin/UserList?msg=removedsuccessfully");
            }
            catch
            {
                return Redirect("/Admin/UserList?msg=error");
            }

        }

        public ActionResult AllocateRole()
        {

            return View(new RoleAllocationModel());

        }

        [HttpPost]
        public ActionResult AllocateRole(RoleAllocationModel rm)
        {

            int roleID = rm.roleID;
            string userName = rm.UserName;

            try
            {

                new RolesBL().AllocateRole(userName, roleID);

            }
            catch
            {

                return Redirect("/Admin/UserList?msg=roleAllreadywithUser");

            }
            return Redirect("/Admin/UserList?msg=roleAdded");

        }

        public ActionResult AllocatePermission()
        {

            return View(new RoleAllocationModel());


        }

        [HttpPost]
        public ActionResult AllocatePermission(RoleAllocationModel rm)
        {

            int roleID = rm.roleID;
            int permissionID = rm.permissionId;

            try
            {

                new RolesBL().AllocatePermission(permissionID, roleID);

            }
            catch
            {

                return Redirect("/Admin/Index?msg=error");

            }

            return Redirect("/Admin/Index?msg=permissionAdded");

        }

        public ActionResult RolePermissionList(int roleID)
        {

            Role r = new RolesBL().GetRoleByID(roleID);

            List<Permission> rolePermissionList = new PermissionsBL().GetRolePermissions(r).ToList();

           PermissionModel pModel = new PermissionModel();
           List<PermissionModel> pModelList = new List<PermissionModel>();

           foreach (Permission p in rolePermissionList)
           {

               pModel.RoleID = roleID;
               pModel.permissionID = p.PermissionID;
               pModel.permissionName = p.PermissionName;
               pModel.permissionDescription = p.PermissionDescription;

               pModelList.Add(pModel);
               pModel = new PermissionModel();

           }

           return View(pModelList);

        }

        public ActionResult DeallocatePermission(int permissionId, int RoleID)
        {

            try
            {

                new PermissionsBL().dropPermission(permissionId, RoleID);
                return Redirect("/Admin/Index?msg=permissionDroped");

            }
            catch
            {

                return Redirect("/Admin/Index?msg=error");

            }

        }

    }
}
