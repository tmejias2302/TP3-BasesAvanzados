using TripTEC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripTEC.Models;

namespace TripTEC.Controllers
{
    public class QueriesController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult selectQuery()
        {
            return View();
        }

        public ActionResult migrateData()
        {
            QueriesService service = new QueriesService();
            service.makeMigration();
            return RedirectToAction("selectQuery", "Queries");
        }

        
        public ActionResult getReservationsPerClient(String nombreCliente)
        {
            QueriesService service = new QueriesService();
            return View(service.getReservationsPerClient(nombreCliente));
        }

        public ActionResult query1()
        {
            ClientService service = new ClientService();
            ViewBag.Clients = service.getClients();
            return View();
        }

        public ActionResult getReservationsSites()
        {
            QueriesService service = new QueriesService();
            return View(service.getTouristicSites());
        }

        public ActionResult getMostVisitedReservationsSites()
        {
            QueriesService service = new QueriesService();
            return View(service.getMostVisitedSites());
        }

        public ActionResult query4()
        {
            ClientService service = new ClientService();
            ViewBag.Clients = service.getClients();
            return View();
        }

        public ActionResult getCommonClients(String nombreCliente)
        {
            QueriesService service = new QueriesService();
            return View(service.getCommonClients(nombreCliente));
        }
    }
}