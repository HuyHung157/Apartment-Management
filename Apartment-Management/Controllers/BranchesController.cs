using Apartment_Management.Context;
using Apartment_Management.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
                var branches = branch.Where(x => x.BranchName.ToUpper().Contains(searchString.ToUpper())).OrderBy(b => b.BranchID);
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
        public void ExportToCSV()
        {
            StringWriter strw = new StringWriter();
            strw.WriteLine("\"BranchID\",\"Tên chi nhánh\",\"Địa chỉ\",\"Ghi chú\",\"IsActive\",\"IsArchive\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename=Chinhanh_{0}.csv", DateTime.Now.ToString("dd/MM/yyyy-H:mm")));
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.UTF8;
            Response.BinaryWrite(Encoding.UTF8.GetPreamble());
            var listproduct = db.Branch.OrderBy(x => x.BranchID);
            foreach (var p in listproduct)
            {
                strw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"", 
                    p.BranchID, p.BranchName, p.Address, p.Description, p.IsActive, p.IsArchive));
            }
            Response.Write(strw.ToString());
            Response.End();
        }
        /*public void ExportToExcel()
        {
            var gv = new GridView();
            gv.DataSource = db.Branch.OrderBy(x => x.BranchID).ToList();
            gv.DataBind();


            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment;filename=Chinhanh{0}.xls", DateTime.Now));
            Response.ContentType = "application/excel";
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            htw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

        }*/
    }
}
