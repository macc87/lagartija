﻿using System.Web;
using System.Web.Optimization;

namespace ClaimManagementCenter
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                            "~/Scripts/jquery-ui-{version}.js",
                                "~/Scripts/jquery.validate.js",
                                    "~/Scripts/knockout-3.4.0.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/global.css",
                       "~/Content/font-awesome.css",
                "~/Content/themes/base/all.css",
                      "~/Content/site.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                  "~/Scripts/jquery.dataTables.min.1.10.8.js",
                     "~/Scripts/dataTables.bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/LogBrowser").Include(
                 "~/Scripts/LogBrowser.js"));

            bundles.Add( new ScriptBundle( "~/bundles/Mailbox" ).Include(
                 "~/Scripts/Mailbox/Mailbox.js" ) );

            BundleTable.EnableOptimizations = true;
        }
    }
}
