using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Access;
using Common;
using Business_Logic;

namespace TraderPlaceApp.Models
{
    public class NewProductModel
    {
        //
        // GET: /NewProductModel/


        
        public string productName {get; set;}
        public string productDesc {get; set;}
        public double price { get; set; }
        public string Image { get; set; }

        
    }
}
