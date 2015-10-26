using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace BamApps.Excido.Angular.App_Start {
    public class BundleConfig {
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.UseCdn = true;

            bundles.Add(new StyleBundle("~/bundles/app-styles")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/font-awesome.css")
                .Include("~/Content/toastr.css")
                .Include("~/Content/app.css"));


            bundles.Add(new ScriptBundle("~/bundles/jquery", "https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js")
                .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/base")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/q.js")
                .Include("~/Scripts/breeze.js")
                .Include("~/Scripts/breeze.bridge.angular.js")
                .Include("~/Scripts/toastr.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/angular", "https://ajax.googleapis.com/ajax/libs/angularjs/1.3.15/angular.min.js")
                .Include("~/Scripts/angular.js"));

            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/app/controllers.js")
                .Include("~/app/app.js")
                .Include("~/app/entityManagerFactory.js")
                );
        }

    }
}