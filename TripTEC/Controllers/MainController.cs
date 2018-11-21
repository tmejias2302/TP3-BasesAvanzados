using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TripTEC.Models;
using TripTEC.Services;

namespace TripTEC.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult createAccount()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(loginModel user)
        {
            if (ModelState.IsValid)
            {
                String isValid = "-1";
                if (user.tipoUsuario == "Cliente")
                {
                    ClientService service = new ClientService();
                    isValid = service.login(user);
                }

                if (user.tipoUsuario == "Empleado")
                {
                    EmployeeService service = new EmployeeService();
                    isValid = service.login(user);

                }

                if (isValid != "-1")
                {
                    HttpCookie cookie1 = new HttpCookie("userType");
                    cookie1.Value = user.tipoUsuario;
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie1);

                    HttpCookie cookie2 = new HttpCookie("_idUser");
                    cookie2.Value = isValid;
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie2);

                    FormsAuthentication.SetAuthCookie(user.login, user.rememberMe);
                    return RedirectToAction("Index", "Main");
                }


              


            }
            return View(user);
        }


        public ActionResult logout()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("userType"))
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["userType"];
                cookie.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("_idUser"))
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["_idUser"];
                cookie.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Main");
        }


    }
}