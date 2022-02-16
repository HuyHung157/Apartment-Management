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
    public class ServiceTypesController : Controller
    {
        private AppContext db = new AppContext();

        // GET: ServiceTypes
        public ActionResult Index(string searchString = "")
        {
            if (searchString != "")
            {
                var serviceTypes = db.ServiceDetail.Where(x => x.ServiceType.ServiceTypeName.ToUpper().Contains(searchString.ToUpper())); ;
                return View(serviceTypes.ToList());
            }
            else
            {
                return View(db.ServiceType.ToList());
            }
        }

        // GET: ServiceTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceType serviceType = db.ServiceType.Find(id);
            if (serviceType == null)
            {
                return HttpNotFound();
            }
            return View(serviceType);
        }

        // GET: ServiceTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceTypeID,ServiceTypeName,UnitPrice,Unit,Year,Month,Description,IsActive,IsArchive")] ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                serviceType.IsActive = true;
                serviceType.IsArchive = false;
                db.ServiceType.Add(serviceType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceType);
        }

        // GET: ServiceTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceType serviceType = db.ServiceType.Find(id);
            if (serviceType == null)
            {
                return HttpNotFound();
            }
            return View(serviceType);
        }

        // POST: ServiceTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceTypeID,ServiceTypeName,UnitPrice,Unit,Year,Month,Description,IsActive,IsArchive")] ServiceType serviceType)
        {
            if (serviceType.IsActive == false)
            {
                serviceType.IsArchive = true;
            }
            if (ModelState.IsValid)
            {
                db.Entry(serviceType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceType);
        }

        // GET: ServiceTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceType serviceType = db.ServiceType.Find(id);
            if (serviceType == null)
            {
                return HttpNotFound();
            }
            db.ServiceType.Remove(serviceType);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: ServiceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceType serviceType = db.ServiceType.Find(id);
            db.ServiceType.Remove(serviceType);
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
