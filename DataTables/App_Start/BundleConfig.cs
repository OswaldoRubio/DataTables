using System.Web;
using System.Web.Optimization;

namespace DataTables
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            BundleTable.EnableOptimizations = true;
            BundleTable.Bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/bundles/DataTables", "https://cdn.datatables.net/v/bs4/dt-1.10.21/datatables.min.js")
                .Include("~/Scripts/datatables.js"));

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

            bundles.Add(new StyleBundle("~/Content/DataTables", "https://cdn.datatables.net/v/bs4/dt-1.10.21/datatables.min.css").
                Include("~/Content/DataTables.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/fontawesome-all.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
