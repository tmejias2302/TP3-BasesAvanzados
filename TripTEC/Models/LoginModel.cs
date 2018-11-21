using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripTEC.Models
{
    public class loginModel
    {
        [Required]
        [Display(Name = "Login")]
        public string login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string password { get; set; }

        [Display(Name = "Recordarme")]
        public bool rememberMe { get; set; }

        [Display(Name = "Tipo de usuario")]
        public String tipoUsuario { get; set; }
    }
}