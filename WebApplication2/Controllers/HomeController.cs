using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous] // Esto es para usuarios sin registro
       public ActionResult Index()
        {
            return View();
        }

        [Authorize] // Esto es para usuarios autorizados
        public ActionResult Cafeteria()
        {
            return View();
        }
    }
}