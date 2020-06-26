using Proyecto.BaseDatos;
using ProyectoWeb.Servicios;
using ProyectoWeb.Utilidades;
using Proyecto.ModelView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ProyectoWeb.Controllers
{
    public class DonacionController : Controller
    {
        DonacionServicio ServicioDonacion = new DonacionServicio();
        PropuestaServicio ServicioPropuesta = new PropuestaServicio();
        SessionHelper sessionHelper = new SessionHelper();
        // GET: Donacion/RealizarDonacion/{id}
        //1:monetario,2:Insumo y 3:Hora
        public ActionResult RealizarDonacion(int Id)
        {
            ViewBag.SesionIniciada = false;
            if (sessionHelper.HaySesionIniciada())
            {
                ViewBag.NombreUsuario = (sessionHelper.getUserNameUsuarioEnSesion() != null) ? sessionHelper.getUserNameUsuarioEnSesion() : "USUARIO";
                ViewBag.SesionIniciada = true;
                ViewBag.EsAdmin = sessionHelper.EsAdmin();
                ViewBag.ID = sessionHelper.GetIdUsuarioEnSesion();
                RealizarDonacionMV modelo = ServicioDonacion.ModelViewDonacionIDPropuesta(Id);
                if (modelo == null)
                {
                    TempData["ErrorInternoEnElServidor"] = true;
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.tipo = ServicioDonacion.TipoDonacion(Id);
                return View(modelo);
            }
            TempData["DebeLoguearse"] = true;
            TempData["Redireccion"] = "../Donacion/RealizarDonacion/"+Id+"";
            return RedirectToAction("Login", "Perfil");
        }
        [HttpPost]
        public ActionResult Donar(RealizarDonacionMV form, string BTN)
        {
            switch (BTN)
            {
                case "Monetario":
                    if (UImagenes.ValidarImagen(Request.Files[0]))
                    {

                        string rutaimagen = UImagenes.Guardar(Request.Files[0], form.DonacionMonetaria.NombreParaElComprobantes);
                        form.DonacionMonetaria.ArchivoTransferencia = rutaimagen;
                        ServicioDonacion.GuardarDonacionmonetaria(form);
                        return Redirect("../Propuesta/Detalle/" + form.Propuesta.IdPropuesta + "");
                    }
                    return Redirect(Request.UrlReferrer.AbsolutePath);

                case "Insumo":
                    ServicioDonacion.GuardarDonacionInsumo(form);
                    return Redirect("../Propuesta/Detalle/" + form.Propuesta.IdPropuesta + "");


                case "Hora":
                    ServicioDonacion.GuardarDonacionHoras(form);
                    return Redirect("../Propuesta/Detalle/" + form.Propuesta.IdPropuesta + "");

            }
            return RedirectToAction("Index", "Home");

        }

        public ActionResult Listar()
        {
            ViewBag.SesionIniciada = false;
           
            if (sessionHelper.HaySesionIniciada())
            {
                ViewBag.NombreUsuario = (sessionHelper.getUserNameUsuarioEnSesion() != null) ? sessionHelper.getUserNameUsuarioEnSesion() : "USUARIO";
                ViewBag.SesionIniciada = true;
                ViewBag.EsAdmin = sessionHelper.EsAdmin();
                ViewBag.ID = sessionHelper.GetIdUsuarioEnSesion();
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

    }
}