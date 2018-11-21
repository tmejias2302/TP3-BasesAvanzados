using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripTEC.Models
{
    public class ClientModel
    {
        public string _id { get; set; }


        [Display(Name = "Login")]
        public string login { get; set; }


        [Display(Name = "Contraseña")]
        public string clave { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Apellidos")]
        public string apellidos { get; set; }

        [Required]
        [Display(Name = "Correo")]
        public string correo { get; set; }

        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        public string fechaNacimiento { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        public string telefono { get; set; }

        [Required]
        [Display(Name = "Cédula")]
        public string cedula { get; set; }

    }
}