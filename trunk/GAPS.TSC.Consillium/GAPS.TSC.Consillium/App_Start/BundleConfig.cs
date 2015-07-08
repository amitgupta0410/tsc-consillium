using System.Web;
using System.Web.Optimization;

namespace GAPS.TSC.Consillium {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/Assets/bundles/js")
                  .Include("~/Assets/js/jquery.validate*")
                  .Include("~/Assets/js/mvcfoolproof.unobtrusive.min.js")
                  .Include("~/Assets/js/expressive.annotations.validate.js")
                 .Include("~/Assets/plugins/daterangepicker/moment.min.js")
                 .Include("~/Assets/plugins/daterangepicker/daterangepicker.js")
                 .Include("~/Assets/plugins/chosen/chosen.jquery.min.js")
                );

            bundles.Add(new StyleBundle("~/Assets/bundles/css")
                .Include("~/Assets/plugins/daterangepicker/daterangepicker-bs3.css")
                .Include("~/Assets/plugins/chosen/chosen.min.css")
                );

            bundles.Add(new StyleBundle("~/Assets/bundles/css/theme")
                .Include("~/Assets/theme/css/components.css")
                .Include("~/Assets/theme/css/plugins.css")
                .Include("~/Assets/theme/css/layout.css")
                .Include("~/Assets/theme/css/default.css")
                );

            bundles.Add(new ScriptBundle("~/Assets/bundles/js/theme")
                .Include("~/Assets/theme/js/jquery-migrate.js")
                .Include("~/Assets/theme/js/jquery-ui.js")
                .Include("~/Assets/theme/js/bs-hover.js")
                .Include("~/Assets/theme/js/slim-scroll.js")
                .Include("~/Assets/theme/js/block-ui.js")
                .Include("~/Assets/theme/js/cookie.js")
                .Include("~/Assets/theme/js/theme.js")
                .Include("~/Assets/theme/js/layout.js")
                );

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
