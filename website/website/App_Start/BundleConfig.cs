using System.Web;
using System.Web.Optimization;

namespace website
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

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                        "~/Content/jquery-ui/jquery-ui.min.js"));
            bundles.Add(new StyleBundle("~/Content/css/jquery-ui").Include(
                      "~/Content/jquery-ui/jquery-ui.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));            

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/toastr.min.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/weather-icons.min.css",
                      "~/Content/css/main.css"));

            bundles.Add(new ScriptBundle("~/bundles/boilermacc").Include(
                      "~/Scripts/macc.ajax.js",
                      "~/Scripts/macc.form.js",
                      "~/Scripts/macc.img.js",
                      "~/Scripts/macc.input.radio.js",
                      "~/Scripts/toastr.min.js",
                      "~/Scripts/macc.modal.js",
                      "~/Scripts/macc.msg.js",
                      "~/Scripts/macc.scrollable.slider.js",
                      "~/Scripts/macc.tabs.js"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                      "~/Scripts/main.js"));
        }
    }
}
