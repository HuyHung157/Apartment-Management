using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Apartment_Management.Context;
using Apartment_Management.Models;

namespace Apartment_Management.Controllers
{
    public class BuildingEmployeesController : Controller
    {
        private AppContext db = new AppContext();

        // GET: BuildingEmployees
        [Authorize]
        public ActionResult Index()
        {
            var buildingEmployees = db.BuildingEmployees.Include(b => b.Building);
            return View(buildingEmployees.ToList());
        }

        // GET: BuildingEmployees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingEmployees buildingEmployees = db.BuildingEmployees.Find(id);
            if (buildingEmployees == null)
            {
                return HttpNotFound();
            }
            return View(buildingEmployees);
        }

        // GET: BuildingEmployees/Create
        public ActionResult Create()
        {
            ViewBag.BuildingID = new SelectList(db.Building, "BuildingID", "Description");
            return View();
        }

        // POST: BuildingEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BuildingEmployeesID,BuildingID,EmployeeID,RoleInBuilding,OfficeName,Description,IsActive,IsArchive")] BuildingEmployees buildingEmployees)
        {
            if (ModelState.IsValid)
            {
                db.BuildingEmployees.Add(buildingEmployees);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BuildingID = new SelectList(db.Building, "BuildingID", "Description", buildingEmployees.BuildingID);
            return View(buildingEmployees);
        }

        // GET: BuildingEmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingEmployees buildingEmployees = db.BuildingEmployees.Find(id);
            if (buildingEmployees == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildingID = new SelectList(db.Building, "BuildingID", "Description", buildingEmployees.BuildingID);
            return View(buildingEmployees);
        }

        // POST: BuildingEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BuildingEmployeesID,BuildingID,EmployeeID,RoleInBuilding,OfficeName,Description,IsActive,IsArchive")] BuildingEmployees buildingEmployees)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buildingEmployees).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuildingID = new SelectList(db.Building, "BuildingID", "Description", buildingEmployees.BuildingID);
            return View(buildingEmployees);
        }

        // GET: BuildingEmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingEmployees buildingEmployees = db.BuildingEmployees.Find(id);
            if (buildingEmployees == null)
            {
                return HttpNotFound();
            }
            return View(buildingEmployees);
        }

        // POST: BuildingEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BuildingEmployees buildingEmployees = db.BuildingEmployees.Find(id);
            db.BuildingEmployees.Remove(buildingEmployees);
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
