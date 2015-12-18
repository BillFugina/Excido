using System.Web;
using System.Web.Mvc;

namespace BamApps.Excido.Angular.App_Start {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
