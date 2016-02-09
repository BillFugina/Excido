﻿using System;
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
                .Include("~/Content/bootstrap-theme.css")
                .Include("~/Content/font-awesome.css")
                .Include("~/Content/toastr.css")
                .Include("~/Content/app.css")
                .Include("~/Content/bootstrap-social.css")
                .Include("~/Content/loading-bar.css")
                );


            bundles.Add(new ScriptBundle("~/bundles/jquery", "https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js")
                .Include("~/Scripts/jquery-{version}.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/base")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/q.js")
                .Include("~/Scripts/breeze.js")
                .Include("~/Scripts/breeze.bridge.angular.js")
                .Include("~/Scripts/toastr.js")
                .Include("~/Scripts/moment.js")
                .Include("~/Scripts/clipboard.js")
                .Include("~/Scripts/ngclipboard.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include("~/Scripts/angular.js")
                .Include("~/Scripts/angular-animate.js")
                .Include("~/Scripts/angular-local-storage.js")
                .Include("~/Scripts/elastic.js")
                .Include("~/Scripts/loading-bar.js")
                .Include("~/Scripts/angular-ui/ui-bootstrap-tpls.js")
                .Include("~/Scripts/angular-ui-router.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/app")

#if DEBUG
                .Include("~/app/BamApps.Excido/BamApps.Excido.Config.Debug.js")
#endif

#if DEV
                .Include("~/app/BamApps.Excido/BamApps.Excido.Config.DevRelease.js")
#endif

#if RELEASE
                .Include("~/app/BamApps.Excido/BamApps.Excido.Config.Release.js")
#endif

                .Include("~/app/BamApps.Excido/BamApps.Excido.Configuration.js")
                .Include("~/app/BamApps/BamApps.Utils.js")
                .Include("~/app/BamApps/BamApps.Logger.js")

                .Include("~/app/BamApps/BamApps.Filter.js")
                .Include("~/app/BamApps/BamApps.Interface.js")
                .Include("~/app/BamApps/BamApps.Model.js")
                .Include("~/app/BamApps/BamApps.Service.js")
                .Include("~/app/BamApps/BamApps.Directive.js")

                .Include("~/app/BamApps/Service/Authentication.js")
                .Include("~/app/BamApps/Service/HelloWorld.js")
                .Include("~/app/BamApps/Service/StateChangeInspector.js")


                .Include("~/app/BamApps.Excido/BamApps.Excido.Interface.js")
                .Include("~/app/BamApps.Excido/BamApps.Excido.Model.js")
                .Include("~/app/BamApps.Excido/BamApps.Excido.Service.js")
                .Include("~/app/BamApps.Excido/BamApps.Excido.Directive.js")

                .Include("~/app/BamApps.Excido/Service/SettingsService.js")
                .Include("~/app/BamApps.Excido/Service/WebApi.js")

                .Include("~/app/BamApps.Excido.Controller/LoginController.js")
                .Include("~/app/BamApps.Excido.Controller/SignupController.js")
                .Include("~/app/BamApps.Excido.Controller/SharedUnitsController.js")
                .Include("~/app/BamApps.Excido.Controller/PreLoginController.js")
                .Include("~/app/BamApps.Excido.Controller/MainController.js")
                .Include("~/app/BamApps.Excido.Controller/HomeController.js")


                .Include("~/app/BamApps.Excido/BamApps.Excido.js")
                );
        }

    }
}