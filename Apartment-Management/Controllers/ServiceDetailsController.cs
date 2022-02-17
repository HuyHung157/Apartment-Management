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
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using PagedList;

namespace Apartment_Management.Controllers
{
    [Authorize]
    public class ServiceDetailsController : Controller
    {
        private AppContext db = new AppContext();

        // GET: ServiceDetails
        public ActionResult Index(string currentFilter, int? page, string searchString = "")
        {
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            if (searchString != "")
            {
                var serviceDetail = db.ServiceDetail.Where(x => x.ServiceType.ServiceTypeName.ToUpper()
                .Contains(searchString.ToUpper())).OrderBy(o => o.ServiceDetailID);
                return View(serviceDetail.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                searchString = currentFilter;
                ViewBag.CurrentFilter = currentFilter;
                var serviceDetails = db.ServiceDetail.Include(s => s.Apartment).Include(s => s.ServiceType);
                return View(serviceDetails.ToPagedList(pageNumber, pageSize));
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
            if (ModelState.IsValid && serviceDetail.Status != null && serviceDetail.Quantity != null && serviceDetail.Amount != null && serviceDetail.Year != null && serviceDetail.Month != null)
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
            if (ModelState.IsValid && serviceDetail.Status != null && serviceDetail.Quantity != null && serviceDetail.Amount != null && serviceDetail.Year != null && serviceDetail.Month != null)
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
        public void ExportToCSV()
        {
            StringWriter strw = new StringWriter();
            strw.WriteLine("\"ServiceDetailID\",\"ApartmentID\",\"Mã dịch vụ\",\"Quantity\",\"Amount\",\"Status\",\"Tháng\",\"Năm\",\"IsActive\",\"IsActive\",\"IsArchive\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename=Canho_{0}.csv", DateTime.Now.ToString("dd/MM/yyyy-H:mm")));
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.UTF8;
            Response.BinaryWrite(Encoding.UTF8.GetPreamble());
            var listproduct = db.ServiceDetail.OrderBy(x => x.ServiceDetailID);
            foreach (var p in listproduct)
            {
                strw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\"",
                   p.ServiceDetailID, p.ApartmentID, p.ServiceTypeID, p.Quantity, p.Amount, p.Status,p.Month,p.Year,p.IsActive,p.IsArchive));
            }
            Response.Write(strw.ToString());
            Response.End();
        }
    }
}
