using Apartment_Management.Context;
using Apartment_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Apartment_Management.Controllers
{
    public class LoginController : Controller
    {
        private AppContext db = new AppContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Autherize(Employee userlogin)
        {
            //using (AppContext db = new AppContext())
            //{
            //    var obj = db.Employee.Where(a => a.Username == userlogin.Username && a.Password == userlogin.Password).FirstOrDefault();
            //    if (obj == null)
            //    {
            //        userlogin.loginerror = "wrong username or password";
            //        return View("Index", userlogin);
            //    }
            //    else
            //    {
            //        Session["EmployeeID"] = obj.EmployeeID;
            //        return RedirectToAction("~/Views/Shared/Layout.cshtml");
            //    }
            //}
            if (ModelState.IsValid)
            {
                using (AppContext db = new AppContext())
                {
                    var obj = db.Employee.Where(a => a.Username.Equals(userlogin.Username) && a.Password.Equals(userlogin.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["EmployeeID"] = obj.EmployeeID;
                        Session["Username"] = obj.Username;
                        return RedirectToAction("Index","Home");
                    }

                }
            }
            return View("Index");

        }

    }
}