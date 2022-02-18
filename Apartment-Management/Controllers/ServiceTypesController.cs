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
    public class ServiceTypesController : Controller
    {
        private AppContext db = new AppContext();

        // GET: ServiceTypes
        public ActionResult Index(string currentFilter, int? page, string searchString = "")
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (searchString != "")
            {
                var serviceTypes = db.ServiceDetail.Where(x => x.ServiceType.ServiceTypeName.ToUpper()
                .Contains(searchString.ToUpper())).OrderBy(o => o.ServiceTypeID);
                return View(serviceTypes.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                searchString = currentFilter;
                ViewBag.CurrentFilter = currentFilter;
                var serType  = db.ServiceType.OrderBy(o => o.ServiceTypeID);
                return View(serType.ToPagedList(pageNumber, pageSize));
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
        public void ExportToCSV()
        {
            StringWriter strw = new StringWriter();
            strw.WriteLine("\"ServiceTypeID\",\"Tên dịch vụ\",\"Đơn giá\",\"Unit\",\"Tháng\",\"Năm\",\"Ghi chú\",\"IsActive\",\"IsArchive\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename=Canho_{0}.csv", DateTime.Now.ToString("dd/MM/yyyy-H:mm")));
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.UTF8;
            Response.BinaryWrite(Encoding.UTF8.GetPreamble());
            var listproduct = db.ServiceType.OrderBy(x => x.ServiceTypeID);
            foreach (var p in listproduct)
            {
                strw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\"",
                   p.ServiceTypeID, p.ServiceTypeName, p.UnitPrice, p.Unit,p.Month, p.Year,p.Description,p.IsActive, p.IsArchive));
            }
            Response.Write(strw.ToString());
            Response.End();
        }
    }
}
