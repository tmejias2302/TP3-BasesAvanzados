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
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult getClient()
        {
            String value="-1";
            var cookie = Request.Cookies["_idUser"];
            if (cookie != null)
            {
                value = cookie.Value.ToString();               
            }
            ClientService service = new ClientService();
            ClientModel model = service.getClient(value);
           

            return View(model);
           
        }


        [HttpGet]
        public ActionResult createClient()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult createClient(ClientModel model)
        {       
            if (ModelState.IsValid)
            {
                string fileName= "";

                ClientService service = new ClientService();
                bool state = service.insertClient(model, fileName);

                
                return RedirectToAction("Login","Main");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult editClient()
        {
            String value = "-1";
            var cookie = Request.Cookies["_idUser"];
            if (cookie != null)
            {
                value = cookie.Value.ToString();
            }
            ClientService service = new ClientService();
            return View(service.getClient(value));
        }

            [HttpPost, ValidateInput(false)]
            public ActionResult editClient(ClientModel model)
            {
                if (ModelState.IsValid)
                {

                    String value = "-1";
                    var cookie = Request.Cookies["_idUser"];
                    if (cookie != null)
                    {
                        value = cookie.Value.ToString();
                    }

                    ClientService service = new ClientService();
                    string login = service.getClient(value).login;

                    bool state = service.editClient(model, value);

                    return RedirectToAction("getClient");
                }
                return View(model);
            }

        

    }
}
