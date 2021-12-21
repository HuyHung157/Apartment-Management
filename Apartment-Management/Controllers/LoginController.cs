using Apartment_Management.Context;
using Apartment_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
        public ActionResult Autherize(Employee userlogin, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (AppContext db = new AppContext())
                {
                    var obj = db.Employee.Where(a => a.Username.Equals(userlogin.Username) 
                                && a.Password.Equals(userlogin.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        FormsAuthentication.SetAuthCookie(obj.Username, false);
                        if(Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") 
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return RedirectToAction("Index", returnUrl.Replace("/",""));
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                       
                    }
                    else
                    {
                        ModelState.AddModelError("accountInvalid", "Username or password invalid");
                        return View("Index");
                    }

                }
            }
            return View("Index");
        }

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

    }
}