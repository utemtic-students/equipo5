using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

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
                string name = saldo.Nombre;
                if (saldo.Saldo > cantidad) {

                    var calculo = (saldo.Saldo - cantidad);
                    saldo.Saldo = calculo;
                    dc.SaveChanges();
                }
                if (saldo.Saldo > 50)
                {/*
                    PENDIENTE!!!cambiar la tabla padres y anexar los datos a la tabla users, asi mismo
                    agregar los campos faltantes a la tabla users.

                    MailMessage mail = new MailMessage();

                    mail.To.Add(ejemplo@gmail.com); //destinatario
                    mail.From = new MailAddress("kidscoinservices@gmail.com");
                    mail.Subject = "Saldo de su cuenta"; // escribir asunto
                    mail.Body = "Hola, " + name+".\n"+" Este mensaje se genera automáticamente par avisarle que su saldo en KidsCoin esta por terminarse, agrege más credito a su cuenta para que sus hijos puedan seguir utilizando su credito."; // escribir texto.


                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Credentials = new System.Net.NetworkCredential("kidscoinservices@gmail.com", "utmanzanillo");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    */
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
            MyDatabaseEntities dc = new MyDatabaseEntities();

            string CurrentUser = HttpContext.User.Identity.Name.ToString();
            if (CurrentUser != "")
            {
                var res = dc.Users.Where(a => a.Username.Equals(CurrentUser)).FirstOrDefault();
                ViewData["rol"] = res.Area;
            }

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