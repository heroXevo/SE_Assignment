using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business_Logic;
using Common;

namespace TraderPlaceApp.Models
{
    public class RoleAllocationModel
    {
        public string UserName { get; set; }
        public int roleID { get; set; }
        public int permissionId { get; set; }

        public IEnumerable<SelectListItem> RoleList
        {
            get { return new SelectList(new RolesBL().GetAllRoles(), "RoleID", "Role_Name"); }

        }

        public IEnumerable<SelectListItem> UserList
        {
            get { return new SelectList(new UsersBL().GetAllUsers(), "UserName", "UserName"); }

        }

        public IEnumerable<SelectListItem> PermissionList
        {
            get { return new SelectList(new PermissionsBL().GetAllPermissions(), "PermissionID" , "PermissionName"); }

        }

    }
}
