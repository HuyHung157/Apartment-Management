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
                    var account = checkAccount(userlogin.Username, userlogin.Password);
                    if (account != null)
                    {
                        FormsAuthentication.SetAuthCookie(account.Username, false);
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

        private Employee checkAccount(string username, string password)
        {
            
            var account = db.Employee.SingleOrDefault(a => a.Username.Equals(username));
            if (account != null)
            {
                //TODO: remove check account by equal function
                var obj = db.Employee.Where(a => a.Username.Equals(username)
                               && a.Password.Equals(password)).FirstOrDefault();
                if(obj == null)
                {
                    if (BCrypt.Net.BCrypt.Verify(password, account.Password))
                    {
                        return account;
                    }
                    return null;
                }
                return obj;
            }
            return null;
        }

    }
}