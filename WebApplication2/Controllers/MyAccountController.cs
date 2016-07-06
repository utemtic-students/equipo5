using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class MyAccountController : Controller
    {
       public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login l, string ReturnUrl = "")
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var user = dc.Users.Where(a => a.Username.Equals(l.Username) && a.Password.Equals(l.Password)).FirstOrDefault();
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, l.RememberMe);
                    if (user.Area=="cafeteria")
                    {
                        return RedirectToAction("Cafeteria", "Cafeteria");
                        
                    }
                    if (user.Area == "servicios_escolares")
                    {
                        return RedirectToAction("Servicios", "Servicios");
                    }
                    if (user.Area == "padres")
                    {
                        return RedirectToAction("Padres", "HOme");
                    }
                }
            }
            ModelState.Remove("Password");
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}