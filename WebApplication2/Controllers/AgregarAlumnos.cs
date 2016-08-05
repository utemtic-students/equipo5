using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication2.Controllers
{
    public class AgregarAlumnos
    {
        public void AgregarAlumno(Alumno alumno)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                dc.Alumnos.Add(alumno);
                dc.SaveChanges();
 
            }
        }
    }
}