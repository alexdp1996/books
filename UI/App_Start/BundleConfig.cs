using System.Web;
using System.Web.Optimization;

namespace UI
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
                      "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/book").Include(
                      "~/node_modules/select2/dist/js/select2.min.js",
                      "~/node_modules/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js",
                      "~/Scripts/book.js"));

            bundles.Add(new StyleBundle("~/book/css").Include(
                      "~/node_modules/select2/dist/css/select2.min.css",
                      "~/node_modules/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                      "~/node_modules/datatables.net/js/jquery.dataTables.min.js",
                      "~/node_modules/datatables.net-bs/js/dataTables.bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/datatables/css").Include(
                      "~/node_modules/datatables.net-bs/css/dataTables.bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/books").Include(
                      "~/Scripts/popup-binder.js",
                      "~/node_modules/moment/min/moment.min.js",
                      "~/Scripts/book-list.js"));

            bundles.Add(new ScriptBundle("~/bundles/authors").Include(
                      "~/Scripts/popup-binder.js",
                      "~/Scripts/author-list.js"));

            bundles.Add(new ScriptBundle("~/bundles/author").Include(
                      "~/Scripts/author.js"));

            bundles.Add(new ScriptBundle("~/bundles/delete").Include(
                      "~/Scripts/delete-modal.js"));
        }
    }
}
