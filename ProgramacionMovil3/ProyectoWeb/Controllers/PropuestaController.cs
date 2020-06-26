using System.Web.Mvc;
using ProyectoWeb.Servicios;
using System;
using System.Collections.Generic;
using Proyecto.BaseDatos;
using ProyectoWeb.Utilidades;
using Proyecto.ModelView;
using System.Linq;

namespace ProyectoWeb.Controllers
{
    public class PropuestaController : Controller
    {
        PropuestaServicio propuestaServicio = new PropuestaServicio();
        PerfilServicio perfilServicio = new PerfilServicio();
        SessionHelper sessionHelper = new SessionHelper();
        UMensajes mensajes = new UMensajes();


        // GET CREAR
        [HttpGet]
        public ActionResult Crear()
        {
            ViewBag.SesionIniciada = false;
            if (sessionHelper.HaySesionIniciada())
            {
                ViewBag.NombreUsuario = (sessionHelper.getUserNameUsuarioEnSesion() != null) ? sessionHelper.getUserNameUsuarioEnSesion() : "USUARIO";
                ViewBag.SesionIniciada = true;
                ViewBag.EsAdmin = sessionHelper.EsAdmin();

                Boolean puedeCrearPropuesta = propuestaServicio.ValidarCantidadDePropuestasActivas(sessionHelper.GetIdUsuarioEnSesion());
               // Boolean camposLlenos = perfilServicio.ValidarCamposLlenos(sessionHelper.GetIdUsuarioEnSesion());
               // if (camposLlenos) { 
                   
                    if (puedeCrearPropuesta)
                    {
                        var listaProfesiones = UDropDownList.GetProfesiones("1"); //select option por default;
                        ViewData["listaProfesiones"] = listaProfesiones;
                        return View();
                    }
                    else{
                        TempData["estaExcedido"] = true;
                        return RedirectToAction("Index", "Home");
                    }
               // }
              //  return RedirectToAction("Editar", "Perfil");
            }
            else
            {
                TempData["DebeLoguearse"] = true;
                TempData["Redireccion"] = "../Propuesta/Crear";
                return RedirectToAction("Login", "Perfil");
            }
        }
      
        // POST CREAR
        [HttpPost]
        public ActionResult Crear(Propuestas propuesta)
        {
            if (ModelState.IsValid) { 
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    string nombreSignificativo = propuesta.NombreSignificativoImagen;
                    string pathRelativoImagen = UImagenes.Guardar(Request.Files[0], nombreSignificativo);
                    propuesta.Foto = pathRelativoImagen;
                }
            
                TempData["propuestaCreada"] = true;

                if (propuesta.TipoDonacion == (int)TipoDePropuesta.HorasDeTrabajo)
                {
                    foreach(var p in propuesta.PropuestasDonacionesHorasTrabajo)
                    if (p.Profesion == "10") //si es otros
                    {
                        p.Profesion = Request["otraProfesion"]; //recupera lo del input
                    }
                }

                int idUsuario = sessionHelper.GetIdUsuarioEnSesion();
                propuestaServicio.Crear(propuesta, idUsuario);
                return RedirectToAction("Index", "Home");
            }
            var listaProfesiones = UDropDownList.GetProfesiones("1"); //select option por default;
            ViewData["listaProfesiones"] = listaProfesiones;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewBag.SesionIniciada = true;

            return View(propuesta);

        }

        // GET MODIFICAR
        [HttpGet]
        public ActionResult Modificar(int id)
        {
            ViewBag.SesionIniciada = false;
            if (sessionHelper.HaySesionIniciada())
            {
                ViewBag.NombreUsuario = (sessionHelper.getUserNameUsuarioEnSesion() != null) ? sessionHelper.getUserNameUsuarioEnSesion() : "USUARIO";
                ViewBag.SesionIniciada = true;
                ViewBag.EsAdmin = sessionHelper.EsAdmin();

                Propuestas j = propuestaServicio.ObtenerPorId(id);
               
                if (j != null)
                {
                    if (j.Estado == (int)Estado.Activa)
                    {
                        
                        if (propuestaServicio.EsEditable(j, sessionHelper.GetIdUsuarioEnSesion()))
                        {

                            if (j.TipoDonacion == (int)TipoDePropuesta.HorasDeTrabajo)
                            {
                                bool isNumeric = false;
                                string profesion = null;
                                //setea el valor del listado o lo que haya cargado en el input
                                foreach (var p in j.PropuestasDonacionesHorasTrabajo)
                                {
                                    isNumeric = int.TryParse(p.Profesion, out int n);
                                    ViewBag.OtraProfesion = p.Profesion;
                                    profesion = p.Profesion;
                                }
                                var listaProfesiones = (!isNumeric) ? UDropDownList.GetProfesiones("10") : UDropDownList.GetProfesiones(profesion);
                                ViewData["listaProfesiones"] = listaProfesiones;
                            }

                            return View(j);
                        }
                        else
                        {
                            TempData["ModificacionInactiva"] = true;
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        TempData["NoEsPosibleModificar"] = true;
                        return RedirectToAction("Index", "Home");

                    }
                       
                }

                TempData["ErrorInternoEnElServidor"] = true;
                return RedirectToAction("Index", "Home");
            }

            TempData["DebeLoguearse"] = true;
            return RedirectToAction("Login", "Perfil");
        }

        //POST MODIFICAR
        [HttpPost]
        public ActionResult Modificar(Propuestas j)
        {
            if (ModelState.Remove("Foto")) { 
                if (ModelState.IsValid)
                {
                    string pathViejo = Request["fotoVieja"];
                    string otraProfesion = Request["otraProfesion"];

                    //si cambia la imagen, borra la vieja de la cerpeta y actualiza la nueva.
                    if (j.Foto != null)
                    {
                        string nombreSignificativo = j.NombreSignificativoImagen;
                        j.Foto = UImagenes.ActualizarImagen(pathViejo, Request, nombreSignificativo);
                    }

                    propuestaServicio.Modificar(j, otraProfesion);
                    TempData["ModificacionExitosa"] = true;
                    return RedirectToAction("Index", "Home");
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewBag.SesionIniciada = true;
            return View(j);

        }

        // GET LISTAR
        [HttpGet]
        //propuestas denunciadas
        public ActionResult Listar()
        {
            ViewBag.SesionIniciada = false;
            if (sessionHelper.HaySesionIniciada())
            {
                ViewBag.NombreUsuario = (sessionHelper.getUserNameUsuarioEnSesion() != null) ? sessionHelper.getUserNameUsuarioEnSesion() : "USUARIO";
                ViewBag.SesionIniciada = true;
                ViewBag.EsAdmin = sessionHelper.EsAdmin();

                int idUsuario = sessionHelper.GetIdUsuarioEnSesion();
                List<Propuestas> listadoPropDenunciadas = propuestaServicio.ObtenerPropuestasDenunciadas(idUsuario);
                return View(listadoPropDenunciadas);
            }
            TempData["DebeLoguearse"] = true;
            return RedirectToAction("Login", "Perfil");
        }

        [HttpGet]
        public ActionResult CambiarEstado(int id)
        {
            ViewBag.SesionIniciada = false;
            if (sessionHelper.HaySesionIniciada())
            {
                ViewBag.NombreUsuario = (sessionHelper.getUserNameUsuarioEnSesion() != null) ? sessionHelper.getUserNameUsuarioEnSesion() : "USUARIO";
                ViewBag.Id = sessionHelper.GetIdUsuarioEnSesion();
                ViewBag.SesionIniciada = true;
                if (!propuestaServicio.CambiarEstado(id, sessionHelper.GetIdUsuarioEnSesion()))

                {
                    TempData["NoEsPosibleActivar"] = true;

                }
                else
                {
                    TempData["ActivacionCorrecta"] = true;
                }

                return RedirectToAction("Index", "Home");
            }
            TempData["DebeLoguearse"] = true;
            return RedirectToAction("Login", "Perfil");
        }

        public ActionResult Detalle(int id)
        {
            ViewBag.SesionIniciada = false;
            if (sessionHelper.HaySesionIniciada())
            {
                ViewBag.InicioSesionCorrecto = (HayRedireccion()) ? InicioSesionCorrecto() : false;
                ViewBag.NombreUsuario = (sessionHelper.getUserNameUsuarioEnSesion() != null) ? sessionHelper.getUserNameUsuarioEnSesion() : "USUARIO";
                ViewBag.SesionIniciada = true;
                ViewBag.EsAdmin = sessionHelper.EsAdmin();
                Propuestas prop = propuestaServicio.ObtenerPorIdParaDetalle(id);

                if (prop != null)
                {
                    if (prop.TipoDonacion == (int)TipoDePropuesta.Monetaria)
                    {
                        ViewBag.Estilo = "fondo fondo-monetaria";
                        ViewBag.Tipo = "MONETARIA";
                    }
                    else if (prop.TipoDonacion == (int)TipoDePropuesta.Insumos)
                    {
                        ViewBag.Estilo = "fondo fondo-insumos";
                        ViewBag.Tipo = "INSUMOS";
                    }
                    else
                    {
                        ViewBag.Estilo = "fondo fondo-trabajo";
                        ViewBag.Tipo = " HORAS DE TRABAJO";
                        bool isNumeric;
                        foreach (var p in prop.PropuestasDonacionesHorasTrabajo)
                        {
                           isNumeric = int.TryParse(p.Profesion, out int n);
                            // select string o select option guardada
                            ViewBag.Profesion = (!isNumeric) ? p.Profesion : UDropDownList.GetNombreSelecccionado(p.Profesion); 
                        }

                    }

                    ViewBag.Visibilidad = (propuestaServicio.SeDebeVerBotonera(prop, sessionHelper.GetIdUsuarioEnSesion())) ? "d-inline" : "disabled readonly d-none";
                    ViewBag.MeGusta = propuestaServicio.TieneValoracion(prop, sessionHelper.GetIdUsuarioEnSesion());
                    return View(prop);
                }
                TempData["ErrorInternoEnElServidor"] = true;
                return RedirectToAction("Index", "Home");
            }
            TempData["Redireccion"] = "../Propuesta/Detalle/"+id;
            TempData["DebeLoguearse"] = true;
            return RedirectToAction("Login", "Perfil");
            
        }
        private bool InicioSesionCorrecto()
        {
            ViewBag.MensajeTemporal = mensajes.MensajeTemporalInicioSesionCorrecto();
            return TempData["InicioSesion"] != null;
        }

        private bool HayRedireccion()
        {
            return TempData["Redireccion"] != null;
        }
        
        public ActionResult MisPropuestas()
        {
            ViewBag.SesionIniciada = false;
            if (sessionHelper.HaySesionIniciada())
            {
                ViewBag.NombreUsuario = (sessionHelper.getUserNameUsuarioEnSesion() != null) ? sessionHelper.getUserNameUsuarioEnSesion() : "USUARIO";
                ViewBag.Id = sessionHelper.GetIdUsuarioEnSesion();
                ViewBag.SesionIniciada = true;
                ViewBag.EsAdmin = sessionHelper.EsAdmin();
                List<Propuestas> listado = propuestaServicio.ObtenerTodos((int)Session["_usuarioId"]);
                return View(listado);
            }
            TempData["DebeLoguearse"] = true;
            return RedirectToAction("Login", "Perfil");

        }

     


    }
}