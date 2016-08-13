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

            if (formCollection.Count > 0)
            {
                Alumno alumno = new Alumno();
                // Retrieve form data using form collection
                alumno.Nombre = formCollection["Nombre"];
                alumno.Apellido = formCollection["Apellido"];
                // int id = Int32.Parse(alumno.Id_Padre);
                alumno.Id_Padre = Int32.Parse(formCollection["Id_Padre"]);

                AgregarAlumnos agregarAlumnos =
                    new AgregarAlumnos();

                agregarAlumnos.AgregarAlumno(alumno);
            }

            return RedirectToAction("servicios");

        }


        public ActionResult Crud(int id = 0)
        {
            return View();
        }
      
        public ActionResult Reporte()
        {
            MyDatabaseEntities dc = new MyDatabaseEntities();
            return View(dc.Padres.ToList());
           
        }
        public ActionResult Reportealumnos()
        {
            MyDatabaseEntities dc = new MyDatabaseEntities();
            return View(dc.Alumnos.ToList());

        }




    }
}