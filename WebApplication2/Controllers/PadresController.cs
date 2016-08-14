using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PadresController : Controller
    {
       

        [AuthorizeUserAccessLevel(UserRole = "padres")]
        public ActionResult Index()
        {
            MyDatabaseEntities dc = new MyDatabaseEntities();

                string CurrentUser = HttpContext.User.Identity.Name.ToString();
                var res = dc.Users.Where(a => a.Username.Equals(CurrentUser)).FirstOrDefault();
                ViewData["rol"] = res.Area;
                ViewData["Nombre"] = res.FirstName;
                ViewData["Apellido"] = res.LastNane;
                ViewData["saldo"] = res.Saldo;
                ViewData["id"] = res.UserID;

            return View();
        }
        [AuthorizeUserAccessLevel(UserRole = "padres")]
        public ActionResult acreditacionPaypal()
        {
            MyDatabaseEntities dc = new MyDatabaseEntities();

            string CurrentUser = HttpContext.User.Identity.Name.ToString();
            if (CurrentUser != "")
            {
                var res = dc.Users.Where(a => a.Username.Equals(CurrentUser)).FirstOrDefault();
                ViewData["ID"] = res.UserID;

                var getData = new GetDataPaypal();
                var order = getData.InformationOrder(getData.GetPayPalResponse(Request.QueryString["tx"]));
                var monto = order.GrossTotal;
                ViewBag.tx = Request.QueryString["tx"];
               // var calculo1 = monto / 100;
                var calculo2 = (res.Saldo + monto);
                res.Saldo = calculo2;
                dc.SaveChanges();

                ViewData["agrego"] = monto;
            }
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult Hijos(int id)
        {

            MyDatabaseEntities dc = new MyDatabaseEntities();
            var res = dc.Alumnos.Where(a => a.Id_Padre == id).ToList();
            var rv = res;

            return PartialView(rv);
        }


    }
}