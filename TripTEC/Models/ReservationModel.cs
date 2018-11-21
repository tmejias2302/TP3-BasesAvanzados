using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripTEC.Models
{
    public class ReservationModel
    {
        [Display(Name = "ID Reservación")]
        public string reservacionId { get; set; }

        [Display(Name = "ID Cliente")]
        public string clienteId { get; set; }

        [Display(Name = "Nombre de cliente")]
        public string nombreCliente { get; set; }

        [Display(Name = "Cantidad de personas")]
        public int cantidadPersonas { get; set; }

        [Display(Name = "Fecha de llegada")]
        public string fechaLlegada { get; set; }

        [Display(Name = "Fecha de salida")]
        public string fechaSalida { get; set; }

        [Display(Name = "Sitio turístico")]
        public string sitioTuristico { get; set; }

    }
}