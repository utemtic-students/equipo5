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
        MyDatabaseEntities db = new MyDatabaseEntities();

      
        public ActionResult BuscarCliente(int id)
        {
                MyDatabaseEntities dc = new MyDatabaseEntities();
            
                var res = dc.Alumnos.Where(a => a.Id_Hijo.Equals(id)).FirstOrDefault();
                ViewData["Id"] = res.Id_Hijo;
                ViewData["Nombre"] = res.Nombre;
                ViewData["Apellido"] = res.Apellido;
                int idPadre = res.Id_Padre.Value;
                var saldo = dc.Padres.Where(a => a.Id.Equals(idPadre)).FirstOrDefault();
                ViewData["Saldo"] = saldo.Saldo;
                return View (id);
        }

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