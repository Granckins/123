using System.Web;
using System.Web.Optimization;

namespace WarehouseDB
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
             bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/plugins/jquery/jquery.min.js"));

             bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       "~/plugins/bootstrap/js/bootstrap.js"));

             bundles.Add(new ScriptBundle("~/bundles/bootstrap-select").Include(
            "~/plugins/bootstrap-select/js/bootstrap-select.js"));

             bundles.Add(new ScriptBundle("~/bundles/slimscroll").Include(
           "~/plugins/jquery-slimscroll/jquery.slimscroll.js"));

             bundles.Add(new ScriptBundle("~/bundles/waves").Include(
          "~/plugins/node-waves/waves.js"));

             bundles.Add(new ScriptBundle("~/bundles/Morris").Include(
 "~/plugins/raphael/raphael.min.js",
 "~/plugins/morrisjs/morris.js"));

             bundles.Add(new ScriptBundle("~/bundles/countTo").Include(
 "~/plugins/jquery-countto/jquery.countTo.js"));

             bundles.Add(new ScriptBundle("~/bundles/Chart").Include(
  "~/plugins/chartjs/Chart.bundle.js"));

             bundles.Add(new ScriptBundle("~/bundles/flot").Include(
  "~/plugins/flot-charts/jquery.flot.js",
  "~/plugins/flot-charts/jquery.flot.resize.js",
  "~/plugins/flot-charts/jquery.flot.pie.js",
  "~/plugins/flot-charts/jquery.flot.categories.js",
  "~/plugins/flot-charts/jquery.flot.time.js"
  ));
             bundles.Add(new ScriptBundle("~/bundles/sparkline").Include(
  "~/plugins/jquery-sparkline/jquery.sparkline.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/js/admin.js",
                "~/js/pages/index.js" 
                ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));
            bundles.Add(new StyleBundle("~/Content/material").Include(
               "~/css/Roboto.css",
               "~/css/MaterialIcons.css" )            );
            bundles.Add(new StyleBundle("~/Content/css").Include(
            
                "~/css/style.css",
                "~/css/themes/theme-lime.css"));



            bundles.Add(new StyleBundle("~/Content/plugins").Include(
                     "~/plugins/bootstrap/css/bootstrap.css",
                     "~/plugins/node-waves/waves.css",
                     "~/plugins/animate-css/animate.css",
                     "~/plugins/morrisjs/morris.css"));
        }
    }
}