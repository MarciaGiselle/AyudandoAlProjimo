using System.Web;
using System.Web.Optimization;

namespace ProyectoWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery-{version}.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            "~/Scripts/jquery-ui-{version}.js",
             "~/Scripts/jquery-ui-{version}.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css", 
                      "~/Content/fontawesome/css/all.min.css", 
                      "~/Content/alertifyjs/alertify.min.css", 
                      "~/Content/alertifyjs/themes/bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                "~/Scripts/bootstrap/bootstrap-datepicker.min.js",
                "~/Scripts/locales/bootstrap-datepicker.es.min.js" ));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
               "~/Scripts/umd/popper-utils.js",
               "~/Scripts/umd/popper.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include("~/Scripts/moment.min.js", "~/Scripts/moment-timezone-with-data.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/base64").Include("~/Scripts/js-base64/base64.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/fontawesome").Include("~/Scripts/fontawesome/js/all.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap.min*",
                      "~/Content/site.css"));


            bundles.Add(new StyleBundle("~/Content/datepicker").Include(
                     "~/Content/bootstrap-datepicker.min.css",
                     "~/Content/bootstrap-datepicker*"
                     ));

            bundles.Add(new StyleBundle("~/Content/fontawesome").Include(
                   "~/Content/font-awesome.css",
                   "~/Content/font-awesome.min.css"
                   ));

            bundles.Add(new ScriptBundle("~/bundles/propuesta").Include(
                         "~/Scripts/Propuesta.js",
                         "~/Scripts/Donaciones.js"
                         ));
        }
    }
}

