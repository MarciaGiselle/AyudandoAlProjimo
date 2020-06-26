using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProyectoWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "Acerca-de",
                url: "acerca-de",
                defaults: new {controller="Home", action="acerca-de" }
            );

            routes.MapRoute(
                name:"Home",
                url:"propuestas/home",
                defaults: new {controller = "Home" , action="Index" }
                );
            routes.MapRoute(
                name:"propuestas/crear",
                url: "propuestas/crear",
                defaults: new {controller="Propuesta" , action="Crear"}
                );
            routes.MapRoute(
                name:"Perfil",
                url: "perfil",
                defaults: new {controller="Perfil", action="MiPerfil"}
                );
            routes.MapRoute(
                name:"denuncias",
                url:"denuncias",
                defaults: new {controller="Denuncia",action="Denuncias" }
                );
            routes.MapRoute(
                name:"Salir",
                url:"propuestas/salir",
                defaults: new {controller="Perfil", action= "CerrarSession" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
