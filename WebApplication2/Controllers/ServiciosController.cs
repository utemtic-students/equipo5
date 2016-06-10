using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class ServiciosController : Controller
    {
       
        public ActionResult Servicios()
        {
            return View();
        }
      
        public ActionResult Registro()
        {
            return View();
        }
        public ActionResult ReporteUsuarios()
        {
            return View();
        }
        public ActionResult EliminarUsuarios()
        {
            return View();
        }
        public ActionResult VentasCafeteria()
        {
            return View();
        }

    }
}