using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Common;
using Business_Logic;
using Data_Access;
using TraderPlaceApp.Models;

namespace TraderPlaceApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Register()
        {

            return View(new Models.RegisterModel());
            
        }

        [HttpPost]
        public ActionResult Register(Models.RegisterModel reg)
        {
            if (new UsersBL().DoesUserNameExist(reg.UserName))
            {

                return Redirect("/home/register?msg=usernametaken");

            }
            else
            {
                User u = new User();
                u.UserName = reg.UserName;
                u.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(reg.Password, "MD5");
                u.Name = reg.Name;
                u.Surname = reg.surName;
                u.Age = reg.Age;
                u.Address = reg.Address;
                u.TownID = 7;

                new UsersBL().CreateUser(u);
                return Redirect("/?msg=success");
            }
        }

        public ActionResult Login()
        {

            return View();

        }

        [HttpPost]
        public ActionResult Login(Models.LoginModel log)
        {

            if (new UsersBL().DoesUserNameExist(log.userName))
            {

                User u = new UsersBL().GetUserByUserName(log.userName);
                try
                {

                    string password = FormsAuthentication.HashPasswordForStoringInConfigFile(log.Password, "MD5");
                    if (new UsersBL().isAuthenticated(log.userName, password))
                    {

                        FormsAuthentication.RedirectFromLoginPage(log.userName, true);
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {

                        return View();

                    }

                }
                catch
                {
                    return View();
                }

            }
            else
            {
                return View();
            }

        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
