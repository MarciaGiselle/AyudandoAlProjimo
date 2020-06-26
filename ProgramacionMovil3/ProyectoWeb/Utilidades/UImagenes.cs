using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;


namespace ProyectoWeb.Utilidades
{
    public class UImagenes
    {


        /// <summary>
        /// Guarda la imagen y retorna la ruta relativa donde se guardó.
        /// </summary>
        /// <param name="archivoSubido"></param>
        /// <param name="nombreSignificativo">Puede ser el username en el caso de la imagen de un usuario, puede ser el nombde de una marca, el nombre un producto, dependiendo de con que se relacione la imagen subida</param>
        /// <returns></returns>
        public static string Guardar(HttpPostedFileBase archivoSubido, string nombreSignificativo)
        {

            //ejemplo: /Media/Imagenes/
            //la carpeta (con path relativo) donde se guardan las imagenes se obtiene del web.config
            string carpetaImagenes = System.Configuration.ConfigurationManager.AppSettings["CarpetaImagenes"];

            if (string.IsNullOrEmpty(carpetaImagenes))
            {
                throw new Exception("En el archivo web.config debe agregar dentro de <appSettings> el elemento <add key=\"CarpetaImagenes\" value=\"/Imagenes/\" />");
            }

            carpetaImagenes = string.Format("/{0}/", carpetaImagenes.TrimStart('/').TrimEnd('/'));

            //Server.MapPath antepone a un string la ruta fisica donde actualmente esta corriendo la aplicacion (ej. c:\inetpub\misitio\)
            string pathDestino = System.Web.Hosting.HostingEnvironment.MapPath("~") + carpetaImagenes;

            //si no exise la carpeta, la creamos
            if (!System.IO.Directory.Exists(pathDestino))
            {
                System.IO.Directory.CreateDirectory(pathDestino);
            }

            string nombreArchivoFinal = GenerarNombreUnico(nombreSignificativo);
            nombreArchivoFinal = string.Concat(nombreArchivoFinal, Path.GetExtension(archivoSubido.FileName));

            archivoSubido.SaveAs(string.Concat(pathDestino, nombreArchivoFinal));

            return string.Concat(carpetaImagenes, nombreArchivoFinal);
        }

        private static string GenerarNombreUnico(string nombreSignificativo)
        {
            //Genero un string random de 20 caracteres para asegurar un nombre unico y que no se pisen archivos inesperadamente
            //System.Web.Security.Membership.GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
            string randomString = System.Web.Security.Membership.GeneratePassword(20, 0);
            Random rnd = new Random();

            //removiendo espacios y caracteres raros del string random 
            randomString = Regex.Replace(randomString, @"[^a-zA-Z0-9]", m => "");

            //removiendo espacios y caracteres raros del nombre 
            nombreSignificativo = Regex.Replace(nombreSignificativo.Trim(), @"[^a-zA-Z0-9]", m => "").ToLower();

            //{Nombre,8 carac}-{Random,5 carac}
            return string.Format("{0}-{1}", UString.Truncar(nombreSignificativo, 8), UString.Truncar(randomString, 5));
        }

        /// <summary>
        /// Borra la imagen guardada en el server basandose en el parametro (relativo o absoluto)
        /// </summary>
        /// <param name="pathGuardado"></param>
        /// <returns></returns>
        public static void Borrar(string pathGuardado)
        {
            //si el path es relativo, se le agrega el mapeo completo para que sea absoluto
            //y pasar de /temp/imagen.jpg a c:\inetpub\temp\imagen.jpg por ejemplo
            if (Path.GetPathRoot(pathGuardado).Contains(":"))
            {
                //Alternativa a Server.MapPath(
                pathGuardado = System.Web.Hosting.HostingEnvironment.MapPath("~") + pathGuardado;
            }

            if (System.IO.File.Exists(pathGuardado))
            {
                System.IO.File.Decrypt(pathGuardado);
            }
        }

        public static Boolean ValidarImagen(HttpPostedFileBase archivo)
        {
            string[] images = { ".png", ".jpg", ".jpeg", ".gif" };

            if (images.Contains(Path.GetExtension(archivo.FileName)))
            {
                return true;
            }

            return false;
        }

        public static string ActualizarImagen(string pathViejo, HttpRequestBase request, String nombreSignificativo)
        { 
           Borrar(pathViejo);
           string pathRelativoImagen = UImagenes.Guardar(request.Files[0], nombreSignificativo);
           return pathRelativoImagen;
        }
    }
}