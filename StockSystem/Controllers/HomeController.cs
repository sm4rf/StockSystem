using StockSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockSystem.Controllers
{


    
    //[Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }


        public JsonResult AutoComplete(string term)
        {
           
                var cons = (from stockdetail in db.Products

                            where stockdetail.ProductName.StartsWith(term) 
                            select new
                            {
                                label = stockdetail.ProductName

                            }).ToList();

                return Json(cons, JsonRequestBehavior.AllowGet);
            

        }
        public JsonResult AutoCompleteEmployee(string term)
        {

            var cons = (from stockdetail in db.Employees

                        where stockdetail.FirstName.StartsWith(term)
                        select new
                        {
                            label = stockdetail.FirstName

                        }).ToList();

            return Json(cons, JsonRequestBehavior.AllowGet);


        }

        public JsonResult AutoCompleteSuppliers(string term)
        {

            var cons = (from stockdetail in db.Suppliers

                        where stockdetail.Name.StartsWith(term)
                        select new
                        {
                            label = stockdetail.Name

                        }).ToList();

            return Json(cons, JsonRequestBehavior.AllowGet);


        }
    }
}