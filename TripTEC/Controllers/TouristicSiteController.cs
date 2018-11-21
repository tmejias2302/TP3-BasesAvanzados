using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripTEC.Models;
using TripTEC.Services;
using System.Globalization;

namespace TripTEC.Controllers
{
    public class TouristicSiteController : Controller
    {
        // GET: TouristicSite
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getTouristicSite()
        {
            TouristicSiteService service = new TouristicSiteService();           
            return View(service.getTouristicSites());
        }

        [HttpGet]
        public ActionResult createTouristicSite()
        {
            return View();
        }

        [HttpGet]
        public ActionResult makeSiteReservation(String id)
        {
            TouristicSiteService service = new TouristicSiteService();
            return View(service.getTouristicSiteByID(id));
        }

        [HttpPost]
        public ActionResult makeSiteReservation(TouristicSiteModel model)
        {
            TouristicSiteService service = new TouristicSiteService();
            if (ModelState.IsValid)
            {
                var cookie = Request.Cookies["_idUser"];
                if (cookie != null)
                {
                    service.insertReservation(model, cookie.Value.ToString());
                    return RedirectToAction("getTouristicSite");
                }
                return RedirectToAction("getTouristicSite");
            }
            return View(model);
        }
        


        [HttpPost]
        public ActionResult createTouristicSite([Bind(Include = "nombre,precio,descripcion,actividades")] TouristicSiteModel model)
        {
            if (ModelState.IsValid)
            {
                TouristicSiteService service = new TouristicSiteService();
                service.insertTouristicSite(model);
                return RedirectToAction("getTouristicSite");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult editTouristicSite(String id)
        {
            TouristicSiteService service = new TouristicSiteService();
            return View(service.getTouristicSiteByID(id));
        }

        [HttpPost]
        public ActionResult editTouristicSite([Bind(Include = "_id,nombre,precio,descripcion,actividades")] TouristicSiteModel model)
        {
            if (ModelState.IsValid)
            {
                TouristicSiteService service = new TouristicSiteService();
                service.updateTouristicSite(model);
                return RedirectToAction("getTouristicSite");
            }
            return View(model);
        }
    }
}