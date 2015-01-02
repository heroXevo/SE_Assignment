using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Data_Access;
using Business_Logic;

namespace TraderPlaceApp.Controllers
{
    public class BecomeSellerController : Controller
    {
        //
        // GET: /BecomeSeller/

        public ActionResult Index()
        {
            if (User.Identity.Name != string.Empty)
            {

                User u = new UsersBL().GetUserByUserName(User.Identity.Name);
                if (!new RolesBL().IsInRole(u.UserName, 3))
                {
                    new UsersBL().BecomeSeller(u.UserName);
                    return Redirect("~/?msg=successfully");

                }
                else
                {
                    return Redirect("~/?msg=AlreadyaSeller");
                }

            }
            else
            {
                return Redirect("~/?msg=notLogedOn");
            }
        }

    }
}
