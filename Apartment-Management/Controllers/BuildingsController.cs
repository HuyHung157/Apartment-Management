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
    public class BuildingsController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Buildings
        [Authorize]
        public ActionResult Index(string searchString = "")
        {
            if (searchString != "")
            {
                var buildings = db.Building.Where(x => x.BuildingName.ToUpper().Contains(searchString.ToUpper()));
                return View(buildings.ToList());
            }
            else
            {
            var building = db.Building.Include(b => b.Branch);
            return View(building.ToList());
            }
        }

        // GET: Buildings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Building.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // GET: Buildings/Create
        public ActionResult Create()
        {
            ViewBag.BranchID = new SelectList(db.Branch, "BranchID", "BranchName");
            return View();
        }

        // POST: Buildings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BuildingID,BranchID,BuildingName,Description,IsActive,IsArchive")] Building building)
        {
            if (ModelState.IsValid)
            {
                building.IsActive = true;
                building.IsArchive = false;
                db.Building.Add(building);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BranchID = new SelectList(db.Branch, "BranchID", "BranchName", building.BranchID);
            return View(building);
        }

        // GET: Buildings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Building.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchID = new SelectList(db.Branch, "BranchID", "BranchName", building.BranchID);
            return View(building);
        }

        // POST: Buildings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BuildingID,BranchID,BuildingName,Description,IsActive,IsArchive")] Building building)
        {
            if (building.IsActive == false)
            {
                building.IsArchive = true;
            }
            if (ModelState.IsValid)
            {
                db.Entry(building).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchID = new SelectList(db.Branch, "BranchID", "BranchName", building.BranchID);
            return View(building);
        }

        // GET: Buildings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Building.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            db.Building.Remove(building);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Building building = db.Building.Find(id);
            db.Building.Remove(building);
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
