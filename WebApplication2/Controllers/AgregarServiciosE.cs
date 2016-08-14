using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Controllers
{
    public class AgregarServiciosE
    {
        public void AgregarServicioE(User user)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                dc.Users.Add(user);
                dc.SaveChanges();
            }
        }
    }
}