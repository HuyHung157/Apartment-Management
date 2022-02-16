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
    [Authorize]
    public class ServiceDetailsController : Controller
    {
        private AppContext db = new AppContext();

        // GET: ServiceDetails
        public ActionResult Index(string searchString = "")
        {
            if (searchString != "")
            {
                var serviceDetail = db.ServiceDetail.Where(x => x.ServiceType.ServiceTypeName.ToUpper().Contains(searchString.ToUpper())); ;
                return View(serviceDetail.ToList());
            }
            else
            {
                var serviceDetails = db.ServiceDetail.Include(s => s.Apartment).Include(s => s.ServiceType);
                return View(serviceDetails.ToList());
            }
        }

        // GET: ServiceDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceDetail serviceDetail = db.ServiceDetail.Find(id);
            if (serviceDetail == null)
            {
                return HttpNotFound();
            }
            return View(serviceDetail);
        }

        // GET: ServiceDetails/Create
        public ActionResult Create()
        {
            ViewBag.ApartmentID = new SelectList(db.Apartment, "ApartmentID", "ApartmentName");
            ViewBag.ServiceTypeID = new SelectList(db.ServiceType, "ServiceTypeID", "ServiceTypeName");
            return View();
        }

        // POST: ServiceDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceDetailID,ApartmentID,ServiceTypeID,Status,Quantity,Amount,Year,Month,Description,IsActive,IsArchive")] ServiceDetail serviceDetail)
        {
            if (ModelState.IsValid)
            {
                serviceDetail.IsActive = true;
                serviceDetail.IsArchive = false;
                db.ServiceDetail.Add(serviceDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApartmentID = new SelectList(db.Apartment, "ApartmentID", "ApartmentName", serviceDetail.ApartmentID);
            ViewBag.ServiceTypeID = new SelectList(db.ServiceType, "ServiceTypeID", "ServiceTypeName", serviceDetail.ServiceTypeID);
            return View(serviceDetail);
        }

        // GET: ServiceDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceDetail serviceDetail = db.ServiceDetail.Find(id);
            if (serviceDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApartmentID = new SelectList(db.Apartment, "ApartmentID", "ApartmentName", serviceDetail.ApartmentID);
            ViewBag.ServiceTypeID = new SelectList(db.ServiceType, "ServiceTypeID", "ServiceTypeName", serviceDetail.ServiceTypeID);
            return View(serviceDetail);
        }

        // POST: ServiceDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceDetailID,ApartmentID,ServiceTypeID,Status,Quantity,Amount,Year,Month,Description,IsActive,IsArchive")] ServiceDetail serviceDetail)
        {
            if (serviceDetail.IsActive == false)
            {
                serviceDetail.IsArchive = true;
            }
            if (ModelState.IsValid)
            {
                db.Entry(serviceDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApartmentID = new SelectList(db.Apartment, "ApartmentID", "ApartmentName", serviceDetail.ApartmentID);
            ViewBag.ServiceTypeID = new SelectList(db.ServiceType, "ServiceTypeID", "ServiceTypeName", serviceDetail.ServiceTypeID);
            return View(serviceDetail);
        }

        // GET: ServiceDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceDetail serviceDetail = db.ServiceDetail.Find(id);
            if (serviceDetail == null)
            {
                return HttpNotFound();
            }
            db.ServiceDetail.Remove(serviceDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: ServiceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceDetail serviceDetail = db.ServiceDetail.Find(id);
            db.ServiceDetail.Remove(serviceDetail);
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
