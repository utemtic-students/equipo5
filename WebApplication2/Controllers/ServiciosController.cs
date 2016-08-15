using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
namespace WebApplication2.Controllers
{
    [Authorize]
    public class ServiciosController : Controller
    {
        // GET: Servicios
        public ActionResult Servicios()
        {
            return View();
        }

        public ActionResult Registros()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegistroPadres() {
            return View();
        }
        [HttpPost]
        public ActionResult RegistroPadres(FormCollection formCollection)
        {
            User padre = new User();
            // Retrieve form data using form collection
            padre.Username = formCollection["UserName"];
            padre.Password = formCollection["Password"];
            padre.FirstName = formCollection["FirstName"];
            padre.LastNane = formCollection["LastNane"];
            padre.Area = formCollection["Area"];
            padre.EmailID = formCollection["EmailID"];
            padre.Saldo = float.Parse(formCollection["Saldo"]);

            AgregarPadres agregarPadres =
                new AgregarPadres();

            agregarPadres.AgregarPadre(padre);
            return RedirectToAction("servicios");
        }


        [HttpGet]
        public ActionResult RegistroAlumnos(string searchBy, string search)
        {
            MyDatabaseEntities db = new MyDatabaseEntities();
            return View(db.Users.Where(x => x.Username == search || search == null).ToList());
        }

        [HttpGet]
        public ActionResult FuncionInsertar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FuncionInsertar(FormCollection formCollection)
        {
            string NombreTemporal = "";
            string ApellidoTemporal = "";
            if (formCollection.Count > 0)
            {
                Alumno alumno = new Alumno();
                // Retrieve form data using form collection
                alumno.Nombre = formCollection["Nombre"];
                alumno.Apellido = formCollection["Apellido"];
                // int id = Int32.Parse(alumno.Id_Padre);
                alumno.Id_Padre = Int32.Parse(formCollection["Id_Padre"]);

                NombreTemporal = formCollection["Nombre"];
                ApellidoTemporal = formCollection["Apellido"];

                AgregarAlumnos agregarAlumnos =
                    new AgregarAlumnos();

                agregarAlumnos.AgregarAlumno(alumno);
            }

            MyDatabaseEntities dc = new MyDatabaseEntities();

            var alumno2 = dc.Alumnos.Where(a => a.Nombre.Equals(NombreTemporal) && a.Apellido.Equals(ApellidoTemporal)).FirstOrDefault();
            string id = alumno2.Id_Hijo.ToString();

            return RedirectToAction("QrCode", new { id = id });

        }

        public ActionResult QrCode(string id)
        {
            ViewData["numero"] = id;

            return View();
        }
  
        [ChildActionOnly]
        public PartialViewResult QRALUMNO(string id)
        {
            ViewData["id"] = id;
            return PartialView();
        }

        [HttpGet]
        public ActionResult RegistroCafeteria()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistroCafeteria(FormCollection formCollection)
        {
            User Cafeteria = new User();
            // Retrieve form data using form collection
            Cafeteria.Username = formCollection["UserName"];
            Cafeteria.Password = formCollection["Password"];
            Cafeteria.FirstName = formCollection["FirstName"];
            Cafeteria.LastNane = formCollection["LastNane"];
            Cafeteria.Area = formCollection["Area"];
            Cafeteria.EmailID = formCollection["EmailID"];
           

            AgregarCafeteria agregarCafeteria =
                new AgregarCafeteria();

            agregarCafeteria.AregarCafeteria(Cafeteria);
            return RedirectToAction("servicios");
        }
        [HttpGet]
        public ActionResult RegistroServiciosEscolares()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistroServiciosEscolares(FormCollection formCollection)
        {
            User ServiciosE = new User();
            // Retrieve form data using form collection
            ServiciosE.Username = formCollection["UserName"];
            ServiciosE.Password = formCollection["Password"];
            ServiciosE.FirstName = formCollection["FirstName"];
            ServiciosE.LastNane = formCollection["LastNane"];
            ServiciosE.Area = formCollection["Area"];
            ServiciosE.EmailID = formCollection["EmailID"];

            AgregarServiciosE agregarServiciosE=
                new AgregarServiciosE();

            agregarServiciosE.AgregarServicioE(ServiciosE);
            return RedirectToAction("servicios");
        }
        public ActionResult Crud(int id = 0)
        {
            return View();
        }

        public ActionResult Reporte()
        {
            MyDatabaseEntities dc = new MyDatabaseEntities();
            return View(dc.Users.ToList());

        }
        public ActionResult delete(int id)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var elim = dc.Users.Where(a => a.UserID == id).FirstOrDefault();
                dc.Users.Remove(elim);
                dc.SaveChanges();
            }
            return RedirectToAction("Reporte", "Servicios");

        }
        public ActionResult ReporteAlumnos()
        {
            MyDatabaseEntities dc = new MyDatabaseEntities();
            return View(dc.Alumnos.ToList());
         }
        public ActionResult delAlumno(int id)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var alum = dc.Alumnos.Where(a => a.Id_Padre == id).FirstOrDefault();
                dc.Alumnos.Remove(alum);
                dc.SaveChanges();
            }
            return RedirectToAction("ReporteAlumnos");

        }




    }
}