using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Controllers
{
    public class agregarVenta
    {
        public void AgregarVenta(Venta venta)
        {


            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {


                dc.Ventas.Add(venta);
                dc.SaveChanges();


            }
        }
    }
}