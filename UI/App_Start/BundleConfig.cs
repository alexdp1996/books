﻿using System.Web;
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

            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
                            "~/Scripts/AutoGenerated/layout-controller.js"));

            bundles.Add(new StyleBundle("~/book/css").Include(
                      "~/node_modules/select2/dist/css/select2.min.css",
                      "~/node_modules/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                      "~/node_modules/datatables.net/js/jquery.dataTables.min.js",
                      "~/node_modules/datatables.net-bs/js/dataTables.bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/datatables/css").Include(
                      "~/node_modules/datatables.net-bs/css/dataTables.bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/books").Include(
                      "~/node_modules/moment/min/moment.min.js",
                      "~/Scripts/AutoGenerated/DataTable/base-datatable-controller.js",
                      "~/Scripts/AutoGenerated/DataTable/book-datatable-controller.js"));

            bundles.Add(new ScriptBundle("~/bundles/authors").Include(
                      "~/Scripts/AutoGenerated/DataTable/base-datatable-controller.js",
                      "~/Scripts/AutoGenerated/DataTable/author-datatable-controller.js"));

            bundles.Add(new ScriptBundle("~/bundles/popup")
                        .IncludeDirectory("~/Scripts/AutoGenerated/Popup", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/alert").Include(
                      "~/Scripts/AutoGenerated/alert-controller.js"));

            bundles.Add(new ScriptBundle("~/bundles/base-entity").Include(
                      "~/Scripts/AutoGenerated/Entity/base-entity-controller.js",
                      "~/Scripts/AutoGenerated/Entity/base-entity-business.js",
                      "~/Scripts/AutoGenerated/Entity/base-entity-service.js"));

            bundles.Add(new ScriptBundle("~/bundles/author").Include(
                      "~/Scripts/AutoGenerated/Entity/author-controller.js"));

            bundles.Add(new ScriptBundle("~/bundles/book").Include(
                      "~/Scripts/AutoGenerated/Entity/book-controller.js",
                      "~/node_modules/select2/dist/js/select2.min.js",
                      "~/node_modules/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js",
                      "~/Scripts/AutoGenerated/Entity/book-controller.js"));

            bundles.Add(new ScriptBundle("~/bundles/view-models").Include(
                      "~/Scripts/AutoGenerated/ViewModels/alertVM.js",
                      "~/Scripts/AutoGenerated/ViewModels/baseVM.js",
                      "~/Scripts/AutoGenerated/ViewModels/popupVM.js",
                      "~/Scripts/AutoGenerated/ViewModels/authorBaseVM.js",
                      "~/Scripts/AutoGenerated/ViewModels/bookBaseVM.js",
                      "~/Scripts/AutoGenerated/ViewModels/authorVM.js",
                      "~/Scripts/AutoGenerated/ViewModels/bookVM.js",
                      "~/Scripts/AutoGenerated/ViewModels/authorVM.js",
                      "~/Scripts/AutoGenerated/ViewModels/savableBookVM.js",
                      "~/Scripts/AutoGenerated/ViewModels/entityUrlsVM.js",
                      "~/Scripts/AutoGenerated/ViewModels/bookUrlsVM.js",
                      "~/Scripts/AutoGenerated/ViewModels/dataTableUrlsVM.js",
                      "~/Scripts/AutoGenerated/ViewModels/bookDataTableUrlsVM.js"));
        }
    }
}
