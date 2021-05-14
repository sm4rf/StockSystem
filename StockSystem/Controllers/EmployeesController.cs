using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockSystem.Models;

namespace StockSystem.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index(string name)
        {
            var list = db.Employees.ToList();

            if (name != null)
            {
                list = list.Where(c => c.FirstName == name).ToList();
            }
            return View(list);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,UniqueId,FirstName,SurName,Position,Contact,Address,Hours,WagePH,Pay,TaxCode, TaxC")] Employees employees, HttpPostedFileBase file)
        {
            if(db.Employees.Where(c=>c.UniqueId == employees.UniqueId).Any())
            {
                ModelState.AddModelError("UniqueId", "Can not duplicate unique id");
                return View(employees);
            }


            if (ModelState.IsValid)
            {

                if (file != null)
                {

                    Guid g = Guid.NewGuid();
                    string path = Server.MapPath("~/Images/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    file.SaveAs(path + Path.GetFileName(g + file.FileName));
                    //ViewBag.Message = "File uploaded successfully.";
                    var pathimg = string.Format("/Images/{0}", g + file.FileName);
                    employees.FilePath = pathimg;
                }

                db.Employees.Add(employees);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employees);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,UniqueId,FirstName,SurName,Position,Contact,Address,Hours,WagePH,Pay,TaxCode, TaxC")] Employees employees,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                if (file != null)
                {

                    Guid g = Guid.NewGuid();
                    string path = Server.MapPath("~/Images/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    file.SaveAs(path + Path.GetFileName(g + file.FileName));
                    //ViewBag.Message = "File uploaded successfully.";
                    var pathimg = string.Format("/Images/{0}", g + file.FileName);
                    employees.FilePath = pathimg;
                }

                db.Entry(employees).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employees);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employees employees = db.Employees.Find(id);
            db.Employees.Remove(employees);
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
