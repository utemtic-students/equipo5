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

                var saldo = dc.Users.Where(a => a.UserID.Equals(idPadre)).FirstOrDefault();
                ViewData["Saldo"] = saldo.Saldo;
                string name = saldo.FirstName;
                if (saldo.Saldo > cantidad) {

                    var calculo = (saldo.Saldo - cantidad);
                    saldo.Saldo = calculo;
                    dc.SaveChanges();
                }
                if (saldo.Saldo < 50)
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(saldo.EmailID);
                    mail.From = new MailAddress("kidscoinservices@gmail.com");
                    mail.Subject = "Saldo en su cartera virtual.";
                    string Body = "<br>Hola, " + name + ".</br>" + "<br>Este mensaje se genera automáticamente para avisarle que su saldo en KidsCoin esta por terminarse, entre a su cuenta y agrege más dinero a su cartera virtual para que sus hijos puedan seguir utilizando su credito.</br>" + "<br>Saldo actual: $"+saldo.Saldo+".</br>"+ "<br><a href='http://izeroxy-001-site1.ftempurl.com/'>Click aquí para agregar saldo.</a></br>" + "<br>Atentamente,</br>"+"<br>KidsCoin System.</br>";
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    using(SmtpClient smtp = new SmtpClient()) { 
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential
                        ("kidscoinservices@gmail.com", "utmanzanillo");// Enter seders User name and password
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
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
                var saldo = dc.Users.Where(a => a.UserID.Equals(idPadre)).FirstOrDefault();
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