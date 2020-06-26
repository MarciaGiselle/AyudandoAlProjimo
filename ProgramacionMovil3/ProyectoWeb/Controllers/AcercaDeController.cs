using ProyectoWeb.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWeb.Controllers
{
    public class AcercaDeController : Controller
    {
        SessionHelper sessionHelper = new SessionHelper();

        [ActionName("acerca-de")]
        public ActionResult Index()
        {
            ViewBag.SesionIniciada = false;

            if (sessionHelper.HaySesionIniciada())
            {
                ViewBag.NombreUsuario = (sessionHelper.getUserNameUsuarioEnSesion() != null) ? sessionHelper.getUserNameUsuarioEnSesion() : "USUARIO";
                ViewBag.Id = sessionHelper.GetIdUsuarioEnSesion();
                ViewBag.SesionIniciada = true;
                return View();
            }
            TempData["DebeLoguearse"] = true;
            return RedirectToAction("Login", "Perfil");

        }
    }
}