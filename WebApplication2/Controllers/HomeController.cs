using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous] // Esto es para usuarios sin registro, la página a la que accederan todos.
        public ActionResult Index()
        {
            MyDatabaseEntities dc = new MyDatabaseEntities();

            string CurrentUser = HttpContext.User.Identity.Name.ToString();
            if (CurrentUser != "")
            {
                var res = dc.Users.Where(a => a.Username.Equals(CurrentUser)).FirstOrDefault();
                ViewData["rol"] = res.Area;
            }
            return View();
        }


        [AuthorizeUserAccessLevel(UserRole ="servicios_escolares")]
        public ActionResult Servicios()
        {
            return View();
        }
        
        [AuthorizeUserAccessLevel(UserRole = "padres")]
        public ActionResult Padres()
        {
            MyDatabaseEntities dc = new MyDatabaseEntities();

            string CurrentUser = HttpContext.User.Identity.Name.ToString();
            if (CurrentUser != "")
            {
                var res = dc.Users.Where(a => a.Username.Equals(CurrentUser)).FirstOrDefault();
                ViewData["ID"] = res.UserID;
            }

            return View();
        }
       
    }
}