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
            if (CurrentUser != "")
            {
                var res = dc.Users.Where(a => a.Username.Equals(CurrentUser)).FirstOrDefault();
                ViewData["rol"] = res.Area;
            }

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
                var calculo1 = monto / 100;
                var calculo2 = (res.Saldo + calculo1);
                res.Saldo = calculo2;
                dc.SaveChanges();

                ViewData["agrego"] = calculo1;
            }
            
            return View();
        }
    }
}