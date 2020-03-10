using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Task1_MVC;

namespace Task1_MVC.Controllers
{
    public class EmployeeAddressesController : Controller
    {
        private EmpEntities db = new EmpEntities();

        // GET: EmployeeAddresses
        public ActionResult Index()
        {
            var employeeAddresses = db.EmployeeAddresses.Include(e => e.Employee);
            return View(employeeAddresses.ToList());
        }

        // GET: EmployeeAddresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeAddress employeeAddress = db.EmployeeAddresses.Find(id);
            if (employeeAddress == null)
            {
                return HttpNotFound();
            }
            return View(employeeAddress);
        }

        // GET: EmployeeAddresses/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName");
            return View();
        }

        // POST: EmployeeAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,Address1,Address2,City,State,Country")] EmployeeAddress employeeAddress)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeAddresses.Add(employeeAddress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", employeeAddress.EmployeeId);
            return View(employeeAddress);
        }

        // GET: EmployeeAddresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeAddress employeeAddress = db.EmployeeAddresses.Find(id);
            if (employeeAddress == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", employeeAddress.EmployeeId);
            return View(employeeAddress);
        }

        // POST: EmployeeAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,Address1,Address2,City,State,Country")] EmployeeAddress employeeAddress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeAddress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", employeeAddress.EmployeeId);
            return View(employeeAddress);
        }

        // GET: EmployeeAddresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeAddress employeeAddress = db.EmployeeAddresses.Find(id);
            if (employeeAddress == null)
            {
                return HttpNotFound();
            }
            return View(employeeAddress);
        }

        // POST: EmployeeAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeAddress employeeAddress = db.EmployeeAddresses.Find(id);
            db.EmployeeAddresses.Remove(employeeAddress);
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
