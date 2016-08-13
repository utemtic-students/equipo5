using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication2.Controllers
{
    public class AgregarPadres
    {
        public void AgregarPadre(User user)
        {
           
            
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {


                dc.Users.Add(user);
                dc.SaveChanges();
                
               
            }
        }


    }
}