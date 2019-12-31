using System.Web;
using System.Web.Optimization;

namespace ProjectWebApplicationMVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/datatable")
                .Include(
                      "~/Content/DataTable/DataTables-1.10.18/css/dataTables.bootstrap.min.css",
                      "~/Content/DataTable/Buttons-1.5.6/css/buttons.bootstrap.min.css",
                      "~/Content/DataTable/FixedColumns-3.2.5/css/fixedColumns.bootstrap.min.css",
                      "~/Content/DataTable/FixedHeader-3.1.4/css/fixedHeader.bootstrap.min.css",
                      "~/Content/DataTable/Responsive-2.2.2/css/responsive.bootstrap.min.css"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(

                "~/Content/DataTable/pdfmake-0.1.36/pdfmake.min.js",
                "~/Content/DataTable/pdfmake-0.1.36/vfs_fonts.js",
                "~/Content/DataTable/DataTables-1.10.18/js/jquery.dataTables.min.js",
                "~/Content/DataTable/DataTables-1.10.18/js/dataTables.bootstrap.min.js",

                "~/Content/DataTable/Buttons-1.5.6/js/dataTables.buttons.min.js",
                "~/Content/DataTable/Buttons-1.5.6/js/buttons.bootstrap.min.js",
                "~/Content/DataTable/Buttons-1.5.6/js/buttons.colVis.min.js",
                "~/Content/DataTable/Buttons-1.5.6/js/buttons.html5.min.js",
                "~/Content/DataTable/Buttons-1.5.6/js/buttons.print.min.js",


                "~/Content/DataTable/FixedColumns-3.2.5/js/dataTables.fixedColumns.min.js",
                "~/Content/DataTable/FixedHeader-3.1.4/js/dataTables.fixedHeader.min.js",

                "~/Content/DataTable/Responsive-2.2.2/js/dataTables.responsive.min.js",
                "~/Content/DataTable/Responsive-2.2.2/js/responsive.bootstrap.min.js"
                ));
        }
    }
}
