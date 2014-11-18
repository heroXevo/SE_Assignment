using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Business_Logic;

namespace TraderPlaceApp.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {

            List<Product> prodList = new ProductsBL().GetAllProducts().ToList();

            return View("Index", prodList);
        }

    }
}
