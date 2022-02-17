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
    public class UsersController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Users
        public ActionResult Index(string currentFilter, int? page, string searchString = "")
        {
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            if (searchString != "")
            {
                var users = db.User.Where(x => x.FirstName.ToUpper().Contains(searchString.ToUpper())).OrderBy(o => o.UserID);
                return View(users.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                searchString = currentFilter;
                ViewBag.CurrentFilter = currentFilter;
                var user = db.User.OrderBy(o => o.UserID);
                return View(user.ToPagedList(pageNumber, pageSize));
            }
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            User user = new User();
            user.IsActive = true;
            return View(user);
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,ApartmentDetailID,FirstName,LastName,Dob,PhoneNumber,IdCard,Address,Description,IsActive,IsArchive")] User user)
        {
            user.IsActive = true;
            user.IsArchive = false;
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,ApartmentDetailID,FirstName,LastName,Dob,PhoneNumber,IdCard,Address,Description,IsActive,IsArchive")] User user)
        {
            if(user.IsActive ==  false)
            {
                user.IsArchive = true;
            }    
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
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
            strw.WriteLine("\"UserID\",\"Chi tiết căn hộ\",\"Họ và tên đệm\",\"Tên\",\"Ngày tháng năm sinh\",\"Số điện thoại\",\"CCCD-CMND\",\"Địa chỉ\",\"Ghi chú\",\"IsActive\",\"IsArchive\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename=Nhanvien_{0}.csv", DateTime.Now.ToString("dd/MM/yyyy-H:mm")));
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.UTF8;
            Response.BinaryWrite(Encoding.UTF8.GetPreamble());
            var listproduct = db.User.OrderBy(x => x.UserID);
            foreach (var p in listproduct)
            {
                strw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\"",
                   p.UserID,p.ApartmentDetailID,p.FirstName,p.LastName,p.Dob,p.PhoneNumber,p.IdCard,p.Address,p.Description,p.IsActive,p.IsArchive));
            }
            Response.Write(strw.ToString());
            Response.End();
        }
    }
}
