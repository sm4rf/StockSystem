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
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index(int? catid,string name)
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Supplier);

            if(catid!=null)
            {
                products =  products.Where(c => c.SubCategoryId == catid);
            }

            if (name != null)
            {
                products = products.Where(c => c.ProductName == name);
            }



            return View(products.OrderByDescending(c=>c.Quantity).ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }
        public JsonResult GetSub(int id)
        {
            var loc = (from c in db.SubCategories
                       
                       where c.CategoryId == id
                       select new
                       {
                           Name = c.Description,
                           Id = c.SubCategoryId
                       }).ToList();

            return Json(loc, JsonRequestBehavior.AllowGet);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description");
            //ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "Description");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name");

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,AddedBy,ProductName,CategoryId,SubCategoryId,Cost,Price,UnitSize,Quantity,SupplierId")] Products products)
        {
            products.AddedBy = User.Identity.Name;

            if (ModelState.IsValid)
            {
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", products.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "Description", products.SubCategoryId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name", products.SupplierId);
            return View(products);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", products.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "Description", products.SubCategoryId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name", products.SupplierId);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,AddedBy,ProductName,CategoryId,SubCategoryId,Cost,Price,UnitSize,Quantity,SupplierId")] Products products)
        {
            if (ModelState.IsValid)
            {
                products.AddedBy = User.Identity.Name;
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", products.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "Description", products.SubCategoryId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name", products.SupplierId);
            return View(products);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
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
