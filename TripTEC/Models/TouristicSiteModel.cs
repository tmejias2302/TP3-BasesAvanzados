using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripTEC.Models
{
    public class TouristicSiteModel
    {
        public string _id { get; set; }
        [Required]
        [Display(Name = "Nombre del sitio turístico")]
        public string nombre { get; set; }
        [Required]
        [Display(Name = "Precio")]
        public int precio { get; set; }
        [Required]
        [Display(Name = "Descripcion")]
        public string descripcion { get; set; }
        [Required]
        [Display(Name = "Actividades")]
        public string actividades { get; set; }

        [Display(Name = "Cantidad de personas")]
        public int cantidadPersonas { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de salida")]
        public DateTime fechaSalida { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de llegada")]
        public DateTime fechaLlegada { get; set; }

    }
}