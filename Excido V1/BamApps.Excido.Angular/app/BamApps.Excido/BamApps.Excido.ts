module BamApps {
    "use strict"
    export module Excido {
        Logger.verbosity(Logger.Level.Log);
        export var app = angular.module('excido', ['breeze.angular']);
        app.directive("syncFocusWith", ["$timeout", "$rootScope", BamApps.Directive.SyncFocusDirective]);
        app.factory("entityManagerFactory", ["$q", BamApps.Service.breezeEntityManagerFactory]);
        app.factory("sharedContentUnitServiceFactory", ["$q", "entityManagerFactory", Excido.Service.SharedContentUnitServiceFactory]);
        app.controller("shared-units", ["$q", "sharedContentUnitServiceFactory", Excido.Controller.SharedUnitsController]);
    }
}