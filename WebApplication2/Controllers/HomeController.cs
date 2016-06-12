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
            return View();
        }
    }
}