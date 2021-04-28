using System.Web;
using System.Web.Optimization;

namespace BTL_QuanLyBanGiay
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

            bundles.Add(new ScriptBundle("~/layout/js").Include(
                "~/Assest/layout/js/jquery-2.1.4.min.js",
                "~/Assest/layout/js/modernizr-2.6.2.min.js",
                "~/Assest/layout/js/classie.js",
                "~/Assest/layout/js/demo1.js",
                "~/Assest/layout/js/search.js",
                "~/Assest/layout/js/jquery-ui.js",
                "~/Assest/layout/js/move-top.js",
                "~/Assest/layout/js/easing.js",
                "~/Assest/layout/js/bootstrap-3.1.1.min.js",
                "~/Assest/layout/js/imagezoom.js",
                "~/Assest/layout/js/easy-responsive-tabs.js",
                "~/Assest/layout/js/jquery.flexslider.js"
                        ));

            bundles.Add(new StyleBundle("~/layout/css").Include(
                "~/Assest/layout/css/easy-responsive-tabs.css",
                "~/Assest/layout/css/bootstrap.css",
                "~/Assest/layout/css/style7.css",
                "~/Assest/layout/css/shopnow.css",
                "~/Assest/layout/css/checkout.css",
                "~/Assest/layout/css/jquery-ui1.css",
                "~/Assest/layout/css/styles.css"
                    ));
        }
    }
}
