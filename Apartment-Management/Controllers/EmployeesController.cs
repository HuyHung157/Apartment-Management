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

namespace Apartment_Management.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Employees
        [Authorize]
        public ActionResult Index(string searchString = "")
        {
            if (searchString != "")
            {
                var employee = db.Employee.Where(x => x.FirstName.ToUpper().Contains(searchString.ToUpper())); ;
                return View(employee.ToList());
            }
            else
            {
                return View(db.Employee.ToList());
            }
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,Role,Username,Password,FirstName,LastName,Dob,PhoneNumber,IdCard,Address,Description,IsActive,IsArchive")] Employee employee)
        {
            if (ModelState.IsValid)
            {

                employee.Password = BCrypt.Net.BCrypt.HashPassword(employee.Password);
                employee.IsActive = true;
                employee.IsArchive = false;
                db.Employee.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,Role,Username,Password,FirstName,LastName,Dob,PhoneNumber,IdCard,Address,Description,IsActive,IsArchive")] Employee employee)
        {
            if (employee.IsActive == false)
            {
                employee.IsArchive = true;
            }
            if (ModelState.IsValid)
            {
                employee.Password = BCrypt.Net.BCrypt.HashPassword(employee.Password);
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            db.Employee.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employee.Find(id);
            db.Employee.Remove(employee);
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
            strw.WriteLine("\"EmployeeID\",\"Quyền\",\"Username\",\"Họ và tên đệm\",\"Tên\",\"Ngày tháng năm sinh\",\"Số điện thoại\",\"CCCD-CMND\",\"Địa chỉ\",\"Ghi chú\",\"IsActive\",\"IsArchive\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename=Nhanvien_{0}.csv", DateTime.Now.ToString("dd/MM/yyyy-H:mm")));
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.UTF8;
            Response.BinaryWrite(Encoding.UTF8.GetPreamble());
            var listproduct = db.Employee.OrderBy(x => x.EmployeeID);
            foreach (var p in listproduct)
            {
                strw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\"",
                   p.EmployeeID,p.Role,p.Username,p.FirstName,p.LastName,p.Dob,p.PhoneNumber,p.IdCard,p.Address,p.Description,p.IsActive, p.IsArchive));
            }
            Response.Write(strw.ToString());
            Response.End();
        }
    }
}
