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
    public class BuildingsController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Buildings
        [Authorize]
        public ActionResult Index(string currentFilter, int? page, string searchString = "")
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (searchString != "")
            {
                var buildings = db.Building.Where(x => x.BuildingName.ToUpper().Contains(searchString.ToUpper())).OrderBy(b => b.BuildingID);
                return View(buildings.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                searchString = currentFilter;
                ViewBag.CurrentFilter = currentFilter;
                var building = db.Building.Include(b => b.Branch).OrderBy(b => b.BuildingID);
                return View(building.ToPagedList(pageNumber, pageSize));
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
        public void ExportToCSV()
        {
            StringWriter strw = new StringWriter();
            strw.WriteLine("\"BuildingID\",\"BranchID\",\"Tên tòa nhà\",\"Ghi chú\",\"IsActive\",\"IsArchive\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename=Toanha_{0}.csv", DateTime.Now.ToString("dd/MM/yyyy-H:mm")));
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.UTF8;
            Response.BinaryWrite(Encoding.UTF8.GetPreamble());
            var listproduct = db.Building.OrderBy(x => x.BuildingID);
            foreach (var p in listproduct)
            {
                strw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"",
                   p.BuildingID, p.BranchID,p.BuildingName, p.Description,p.IsActive, p.IsArchive));
            }
            Response.Write(strw.ToString());
            Response.End();
        }
    }
}
