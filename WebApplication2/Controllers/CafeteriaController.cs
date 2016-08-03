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
       
      

        public void Cobrar(int id, float cantidad)
        {
           
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {

                var res = dc.Alumnos.Where(a => a.Id_Hijo.Equals(id)).FirstOrDefault();
                ViewData["Id"] = res.Id_Hijo;
                ViewData["Nombre"] = res.Nombre;
                ViewData["Apellido"] = res.Apellido;
                int idPadre = res.Id_Padre.Value;

                var saldo = dc.Padres.Where(a => a.Id.Equals(idPadre)).FirstOrDefault();
                ViewData["Saldo"] = saldo.Saldo;
                if (saldo.Saldo > cantidad) {

                    var calculo = (saldo.Saldo - cantidad);
                    saldo.Saldo = calculo;
                    dc.SaveChanges();
                }
                else
                {
                    
                }
                
                
            }
        }
        
        [HttpPost]
        [AuthorizeUserAccessLevel(UserRole = "cafeteria")]
        public ActionResult Ventas(FormCollection formCollection)
        {
            int id = Int32.Parse(formCollection["idCliente"]);
            float cantidad = float.Parse(formCollection["Monto"]);

            Venta venta = new Venta();
            venta.IdCliente = Int32.Parse(formCollection["idCliente"]);
            venta.Cliente = formCollection["Cliente"];
            venta.Vendedor = formCollection["Vendedor"];
            venta.DecrpVenta = formCollection["DecrpVenta"];
            venta.Monto = float.Parse(formCollection["Monto"]);
            venta.Fecha = DateTime.Parse(formCollection["Fecha"]);

            Cobrar(id, cantidad);
            agregarVenta ventas = new agregarVenta();
            ventas.AgregarVenta(venta);
            
            
            return RedirectToAction("Cafeteria");
        }
      
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
                return PartialView (id);
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