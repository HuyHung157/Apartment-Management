using Apartment_Management.Context;
using Apartment_Management.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Apartment_Management.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private AppContext db = new AppContext();
        // GET: Profile
        public ActionResult Index()
        {

            var userName = System.Web.HttpContext.Current.User.Identity.Name;
            Employee employee = db.Employee.Where(o => o.Username.ToUpper().Contains(userName.ToUpper())).FirstOrDefault(); ;
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "EmployeeID,Role,Username,Password,FirstName,LastName,Dob,PhoneNumber,IdCard,Address,Description,IsActive,IsArchive")] Employee employee)
        {
            ModelState.Remove("Password");
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.Entry(employee).Property(x => x.Password).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);

        }

    }
}