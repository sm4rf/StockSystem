using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockSystem.Models;

namespace StockSystem.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Products);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,Date,ProductId,Quantity")] Orders orders)
        {
            var prod = db.Products.Where(c => c.ProductId == orders.ProductId).SingleOrDefault();

            if(orders.Quantity>prod.Quantity)
            {
                ModelState.AddModelError("","Quantity Not available");
                ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName",orders.ProductId);
                return View(orders);
            }
            

            orders.Date = DateTime.Today;
            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);

                prod.Quantity += orders.Quantity;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", orders.ProductId);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", orders.ProductId);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,Date,ProductId,Quantity")] Orders orders)
        {
            var prod = db.Products.Where(c => c.ProductId == orders.ProductId).SingleOrDefault();
            var order = db.Orders.Where(c => c.OrderId == orders.OrderId).SingleOrDefault();
           
            var diff = order.Quantity - orders.Quantity;
            
            if (diff<0)
            {
                var di = diff * -1;
                if (di > prod.Quantity)
                {
                    ModelState.AddModelError("", "Quantity Not available");
                    ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", orders.ProductId);
                    return View(orders);
                }
                prod.Quantity += diff;
            }
            else
            {
                
                prod.Quantity += diff;
            }
            
            if (ModelState.IsValid)
            {

                order.Quantity = orders.Quantity;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", orders.ProductId);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Find(id);
            db.Orders.Remove(orders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
