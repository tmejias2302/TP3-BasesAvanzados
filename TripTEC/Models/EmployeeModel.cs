using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripTEC.Models
{
    public class EmployeeModel
    {
        public string login { get; set; }
        [Display(Name = "Contraseña")]
        public string clave { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
    }
}