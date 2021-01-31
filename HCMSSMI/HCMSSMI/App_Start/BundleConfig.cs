using System.Web;
using System.Web.Optimization;

namespace HCMSSMI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {


            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/bootstrap.min.css",
                     "~/Content/main/app.min.css"));

            bundles.Add(new StyleBundle("~/Content/DataTables").Include
                (
                    "~/Content/DataTables/css/dataTables.bootstrap4.min.css",
                    "~/Content/DataTables/css/jquery.dataTables.min.css"

                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
                 "~/Scripts/DataTables/jquery.dataTables.min.js"
                ));
        }
    }
}
