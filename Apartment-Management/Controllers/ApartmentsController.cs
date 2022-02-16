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
    public class ApartmentsController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Apartments
        [Authorize]
        public ActionResult Index(string searchString = "")
        {
            if (searchString != "")
            {
                var apartments = db.Apartment.Where(x => x.ApartmentName.ToUpper().Contains(searchString.ToUpper()));
                return View(apartments.ToList());
            }
            else
            {
                var apartment = db.Apartment.Include(a => a.Building);
                return View(apartment.ToList());
            }
            
        }

        // GET: Apartments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartment.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }

        // GET: Apartments/Create
        public ActionResult Create()
        {
            ViewBag.BuildingID = new SelectList(db.Building, "BuildingID", "BuildingName");
            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApartmentID,BuildingID,ApartmentName,Description,IsActive,IsArchive")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                apartment.IsActive = true;
                apartment.IsArchive = false;
                db.Apartment.Add(apartment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BuildingID = new SelectList(db.Building, "BuildingID", "BuildingName", apartment.BuildingID);
            return View(apartment);
        }

        // GET: Apartments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartment.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildingID = new SelectList(db.Building, "BuildingID", "BuildingName", apartment.BuildingID);
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApartmentID,BuildingID,ApartmentName,Description,IsActive,IsArchive")] Apartment apartment)
        {
            if (apartment.IsActive == false)
            {
                apartment.IsArchive = true;
            }
            if (ModelState.IsValid)
            {
                db.Entry(apartment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuildingID = new SelectList(db.Building, "BuildingID", "BuildingName", apartment.BuildingID);
            return View(apartment);
        }

        // GET: Apartments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartment.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            db.Apartment.Remove(apartment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apartment apartment = db.Apartment.Find(id);
            db.Apartment.Remove(apartment);
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
