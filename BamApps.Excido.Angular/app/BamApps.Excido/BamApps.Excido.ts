module BamApps {
    "use strict"
    export module Excido {
        Logger.verbosity(Logger.Level.Log);
        export var app = angular.module('excido', ['ui.router', 'breeze.angular', 'ui.bootstrap', 'angular-loading-bar', 'monospaced.elastic', 'LocalStorageModule']);

        app.value('authenticationServiceBaseUrl', Configuration.Settings.AuthenticationServiceBaseUrl);

        app.provider('settingsService', BamApps.Excido.Service.SettingsServiceProvider);

        app.controller("loginController", ['$scope', '$location', 'authenticationServiceFactory', Excido.Controller.LoginController]);
        app.controller("shared-units", ["$rootScope", "$q", "sharedContentUnitServiceFactory", Excido.Controller.SharedUnitsController]);
        app.controller("signupController", ['$scope', '$location', '$timeout', 'authenticationServiceFactory', Excido.Controller.SignupController]);
        app.controller("mainAppController", ['$scope', 'settingsService', '$state', Excido.Controller.MainAppController]);
        app.controller("homeController", ['$scope', Excido.Controller.HomeController]);

        app.directive("syncFocusWith", ["$timeout", "$rootScope", "$parse", BamApps.Directive.SyncFocusDirective]);
        app.directive("onEnterKey", ["$timeout", "$rootScope", BamApps.Directive.OnEnterKeyDirective]);
        app.directive("onTabKey", ["$timeout", "$rootScope", BamApps.Directive.OnTabKeyDirective]);
        app.directive("clipboard", ["$timeout", "$rootScope", BamApps.Directive.ClipboardDirective]);

        app.factory("entityManagerFactory", ["$q", "breeze", BamApps.Service.breezeEntityManagerFactory]);

        app.factory("sharedContentUnitServiceFactory", ["$q", "entityManagerFactory", Excido.Service.SharedContentUnitServiceFactory]);
        app.factory("authenticationServiceFactory", ['$http', '$q', 'localStorageService', 'authenticationServiceBaseUrl', BamApps.Service.authenticationServiceFactory]);

        app.factory("authenticationInterceptorServiceFactory", ['$q', '$location', 'localStorageService', BamApps.Service.getAuthenticationInteceptorService]);

        app.filter("collapse", Filter.CollapseFilter);

        app.config(['$stateProvider', '$urlRouterProvider', Configuration.Settings.uiRouteConfiguration]);

        app.config(($httpProvider: ng.IHttpProvider) => {
            $httpProvider.interceptors.push('authenticationInterceptorServiceFactory');
        });
    }
}