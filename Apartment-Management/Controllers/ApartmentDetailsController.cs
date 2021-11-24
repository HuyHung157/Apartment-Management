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
    public class ApartmentDetailsController : Controller
    {
        private AppContext db = new AppContext();

        // GET: ApartmentDetails
        public ActionResult Index()
        {
            var apartmentDetail = db.ApartmentDetail.Include(a => a.Apartment).Include(a => a.User);
            return View(apartmentDetail.ToList());
        }

        // GET: ApartmentDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmentDetail apartmentDetail = db.ApartmentDetail.Find(id);
            if (apartmentDetail == null)
            {
                return HttpNotFound();
            }
            return View(apartmentDetail);
        }

        // GET: ApartmentDetails/Create
        public ActionResult Create()
        {
            ViewBag.ApartmentID = new SelectList(db.Apartment, "ApartmentID", "ApartmentName");
            ViewBag.UserID = new SelectList(db.User, "UserID", "FirstName");
            return View();
        }

        // POST: ApartmentDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApartmentDetailID,ApartmentID,UserID,IsHost,Relationship,Type,Description,IsActive,IsArchive")] ApartmentDetail apartmentDetail)
        {
            if (ModelState.IsValid)
            {
                db.ApartmentDetail.Add(apartmentDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApartmentID = new SelectList(db.Apartment, "ApartmentID", "ApartmentName", apartmentDetail.ApartmentID);
            ViewBag.UserID = new SelectList(db.User, "UserID", "FirstName", apartmentDetail.UserID);
            return View(apartmentDetail);
        }

        // GET: ApartmentDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmentDetail apartmentDetail = db.ApartmentDetail.Find(id);
            if (apartmentDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApartmentID = new SelectList(db.Apartment, "ApartmentID", "ApartmentName", apartmentDetail.ApartmentID);
            ViewBag.UserID = new SelectList(db.User, "UserID", "FirstName", apartmentDetail.UserID);
            return View(apartmentDetail);
        }

        // POST: ApartmentDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApartmentDetailID,ApartmentID,UserID,IsHost,Relationship,Type,Description,IsActive,IsArchive")] ApartmentDetail apartmentDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apartmentDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApartmentID = new SelectList(db.Apartment, "ApartmentID", "ApartmentName", apartmentDetail.ApartmentID);
            ViewBag.UserID = new SelectList(db.User, "UserID", "FirstName", apartmentDetail.UserID);
            return View(apartmentDetail);
        }

        // GET: ApartmentDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmentDetail apartmentDetail = db.ApartmentDetail.Find(id);
            if (apartmentDetail == null)
            {
                return HttpNotFound();
            }
            return View(apartmentDetail);
        }

        // POST: ApartmentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApartmentDetail apartmentDetail = db.ApartmentDetail.Find(id);
            db.ApartmentDetail.Remove(apartmentDetail);
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
