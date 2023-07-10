using System.Web;
using System.Web.Optimization;

namespace AplicacionWeb
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Content/js.3.7/jquery-{version}.j"));

            bundles.Add(new Bundle("~/bundles/complementos").Include(                        
                        "~/Content/fontawesome/all.min.js",
                        "~/Scripts/DataTables/jquery.dataTables.js",
                        "~/Scripts/DataTables/dataTables.responsive.js",
                        "~/Content/loadingoverlay/loadingoverlay.min.js",
                        "~/Content/js/sweetalert.min.js",
                        "~/Content/js/scripts.js"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Content/js.3.7/bootstrap.bundle.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap/bootstrap.css",
                      "~/Content/css/site.css",
                      "~/Content/DataTables/css/jquery.DataTables.css",
                      "~/Content/DataTables/css/responsive.DataTables.css",
                      "~/Content/css/sweetalert.css"));
        }
    }
}
