using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BamApps.Excido.WebApi.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            Properties.Settings settings = new Properties.Settings();
            if (Request.Url.IsLoopback) {
                Response.Redirect(settings.LoopbackRootRedirect, true);
            }
            else {
                Response.Redirect(settings.RootRedirect, true);
            }
            return View();
        }
    }
}
