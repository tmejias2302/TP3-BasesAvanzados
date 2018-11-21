using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripTEC.Models;
using TripTEC.Services;

namespace TripTEC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult createEmployee()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult createEmployee(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                EmployeeService service = new EmployeeService();
                service.insertEmployee(model);
                return RedirectToAction("Login","Main");
            }
            return View(model);
        }
    }
}