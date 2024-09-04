using System.Web.Optimization;

namespace MyLottoCheck
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootbox.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/megamillionsentrygrid").Include(
                      "~/Scripts/mega-millions-entry-grid.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                      //"~/Scripts/jquery.mobile-1.4.5.js",
                      "~/Scripts/site.js",
                      "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                      "~/Scripts/login.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/jquery.mobile-1.4.5.css",
                      //"~/Content/jquery.mobile.theme-1.4.5.css",
                      "~/Content/ace.css",
                      "~/Content/bootstrap-cerulean-theme.min.css",
                      "~/Content/site.css",
                      "~/Content/font-awesome.css",
                      "~/Content/toastr.css"

                      ));

            bundles.Add(new StyleBundle("~/Content/grid").Include(
                "~/Content/grid.css"
                
            ));
        }
    }
}
