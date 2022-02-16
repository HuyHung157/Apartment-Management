using Apartment_Management.Context;
using Apartment_Management.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;

namespace Apartment_Management.Controllers
{
    public class BranchesController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Branches
        [Authorize]
        public ActionResult Index(string currentFilter, int? page, string sortOrder, string searchString = "")
        {

            ViewBag.SortByName = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SortByAddress = (sortOrder == "address" ? "address_desc" : "address");
            var branch = db.Branch.Include(b => b.Buildings);
            switch (sortOrder)
            {
                case "name_desc":
                    branch = branch.OrderByDescending(s => s.BranchName);
                    break;
                case "address_desc":
                    branch = branch.OrderByDescending(s => s.Address);
                    break;
                case "address":
                    branch = branch.OrderBy(s => s.Address);
                    break;
                default://mặc định sắp xếp theo tên sản phẩm
                    branch = branch.OrderBy(s => s.BranchName);
                    break;
            }
            int pageSize = 12;
            int pageNumber = (page ?? 1);

            if (searchString != "")
            {
                var branches = branch.Where(x => x.BranchName.ToUpper().Contains(searchString.ToUpper()));
                return View(branches.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                searchString = currentFilter;
                ViewBag.CurrentFilter = currentFilter;
                return View(branch.ToPagedList(pageNumber, pageSize));
            }
            
        }

        // GET: Branches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branch.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // GET: Branches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BranchID,BranchName,Address,Description,IsActive,IsArchive")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                branch.IsActive = true;
                branch.IsArchive = false;
                db.Branch.Add(branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(branch);
        }

        // GET: Branches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branch.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BranchID,BranchName,Address,Description,IsActive,IsArchive")] Branch branch)
        {
            if(branch.IsActive == false)
            {
                branch.IsArchive = true;
            }
            if (ModelState.IsValid)
            {
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branch);
        }

        // GET: Branches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branch.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            db.Branch.Remove(branch);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Branch branch = db.Branch.Find(id);
            db.Branch.Remove(branch);
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
