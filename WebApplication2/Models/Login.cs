using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Login
    {
        [Required(ErrorMessage ="Nombre de usuario requerido.", AllowEmptyStrings =false)]
        public string Username { get; set; }
        [Required(ErrorMessage ="Contraseña requerida.", AllowEmptyStrings =false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}