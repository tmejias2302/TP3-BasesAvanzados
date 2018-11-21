using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripTEC.Models;
using TripTEC.Services;
using System.IO;
using System.Diagnostics;

namespace TripTEC.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservations
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult getReservations()
        {
            String value = "-1";
            var cookie = Request.Cookies["_idUser"];
            if (cookie != null)
            {
                value = cookie.Value.ToString();
            }
            ReservationService service = new ReservationService();

            return View(service.getReservations(value));

        }

    }
}