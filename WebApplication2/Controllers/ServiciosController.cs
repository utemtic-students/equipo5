using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
namespace WebApplication2.Controllers
{
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
            Padre padre = new Padre();
            // Retrieve form data using form collection
            padre.Nombre = formCollection["Nombre"];
            padre.Apellido = formCollection["Apellido"];
            padre.Usuario = formCollection["Usuario"];
            padre.Contrasenia =formCollection["Contrasenia"];

            AgregarPadres agregarPadres =
                new AgregarPadres();

            agregarPadres.AgregarPadre(padre);
            return RedirectToAction("servicios");
        }

        [HttpGet]
        public ActionResult RegistroAlumnos(string searchBy, string search)
        {
            MyDatabaseEntities db = new MyDatabaseEntities();
            return View(db.Padres.Where(x => x.Usuario == search || search == null).ToList());
        }

        [HttpPost]
        public ActionResult Registro_Alumno(FormCollection formCollection)
        {
            return View();
        }

        [HttpGet]
        public ActionResult FuncionInsertar()
        {
            return PartialView();
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
            else
            {

            }
            return RedirectToAction("servicios");
          
        }
    }
}