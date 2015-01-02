using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Business_Logic;
using TraderPlaceApp.Models;

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

        public ActionResult MyProducts()
        {

            List<Product> prodList = new ProductsBL().GetAllProductsByUsenName(User.Identity.Name).ToList();
            return View("Myproducts", prodList);

        }

        public ActionResult AddProduct()
        {

            return View(new NewProductModel());

        }

        [HttpPost]
        public ActionResult AddProduct(NewProductModel np, HttpPostedFileBase file)
        {


            
            //string filename = System.IO.Path.GetFileName(file.FileName);
            //string strLocation = HttpContext.Server.MapPath("~/Images");
            //file.SaveAs(strLocation + @"\" + filename.Replace('+', '_'));
            
            
            
            Product p = new Product();
            p.Product_Name = np.productName;
            p.C_Product_Description = np.productDesc;
            p.Price = Convert.ToDecimal(np.price);            
            //p.Image = @"\Images\" + filename;
            p.Image = "~/Images/na.png";
            p.UserName = User.Identity.Name;

            new ProductsBL().AddProduct(p);

            return RedirectToAction("MyProducts", "Product");

        }

        public ActionResult EditProduct(int prodID)
        {

            Product p = new ProductsBL().GetProductByID(prodID);
            NewProductModel model = new NewProductModel();

            model.productName = p.Product_Name;
            model.productDesc = p.C_Product_Description;
            model.price = Convert.ToDouble(p.Price);
            model.Image = p.Image;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditProduct(int prodID, NewProductModel m)
        {
            Product oldProd = new ProductsBL().GetProductByID(prodID);
            Product updatedProd = new Product();

            updatedProd.ProductID = prodID;
            updatedProd.Product_Name = m.productName;
            updatedProd.C_Product_Description = m.productDesc;
            updatedProd.Price = Convert.ToDecimal(m.price);
            updatedProd.Image = m.Image;
            updatedProd.UserName = oldProd.UserName;

            new ProductsBL().UpdateProduct(updatedProd);

            return RedirectToAction("Index", "Product");

        }

        public ActionResult Delete(int prodID)
        {

            new ProductsBL().DeleteProductByID(prodID);
            return Redirect("Index");

        }


    }
}
