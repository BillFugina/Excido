﻿module BamApps {
    "use strict"
    export module Excido {
        Logger.verbosity(Logger.Level.Log);
        export var app = angular.module('excido', ['ngRoute', 'breeze.angular', 'ui.bootstrap', 'monospaced.elastic', 'LocalStorageModule']);

        app.value('authenticationServiceBaseUrl', Configuration.Settings.AuthenticationServiceBaseUrl);


        app.controller("loginController", ['$scope', '$location', 'authenticationServiceFactory', Excido.Controller.LoginController]);
        app.controller("shared-units", ["$rootScope", "$q", "sharedContentUnitServiceFactory", Excido.Controller.SharedUnitsController]);
        app.controller("signupController", ['$scope', '$location', '$timeout', 'authenticationServiceFactory', Excido.Controller.SignupController]);

        app.directive("syncFocusWith", ["$timeout", "$rootScope", "$parse", BamApps.Directive.SyncFocusDirective]);
        app.directive("onEnterKey", ["$timeout", "$rootScope", BamApps.Directive.OnEnterKeyDirective]);
        app.directive("onTabKey", ["$timeout", "$rootScope", BamApps.Directive.OnTabKeyDirective]);
        app.directive("clipboard", ["$timeout", "$rootScope", BamApps.Directive.ClipboardDirective]);

        app.factory("entityManagerFactory", ["$q", "breeze", BamApps.Service.breezeEntityManagerFactory]);

        app.factory("sharedContentUnitServiceFactory", ["$q", "entityManagerFactory", Excido.Service.SharedContentUnitServiceFactory]);
        app.factory("authenticationServiceFactory", ['$http', '$q', 'localStorageService', 'authenticationServiceBaseUrl', BamApps.Service.authenticationServiceFactory]);

        app.factory("authenticationInterceptorServiceFactory", ['$q', '$location', 'localStorageService', BamApps.Service.getAuthenticationInteceptorService]);

        app.filter("collapse", Filter.CollapseFilter);

        app.config(['$routeProvider', Configuration.Settings.RouteProvider]);
        app.config(($httpProvider: ng.IHttpProvider) => {
            $httpProvider.interceptors.push('authenticationInterceptorServiceFactory');
        });
    }
}