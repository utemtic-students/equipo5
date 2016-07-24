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

        public ActionResult Crud(int id = 0)
        {
            return View();
        }
    }
}