using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class CafeteriaController : Controller
    {
        [AuthorizeUserAccessLevel(UserRole = "cafeteria")]
        public ActionResult Cafeteria()
        {
            return View();
        }
        [AuthorizeUserAccessLevel (UserRole = "cafeteria")]
        public ActionResult ReporteVentas()
        {
            var rv = new List<Venta>();
            using(MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                rv = dc.Ventas.ToList();
            }
            return View(rv);
        }


    }
}