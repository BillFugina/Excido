module BamApps {
    "use strict"
    export module Excido {
        BamApps.Logger.verbosity(BamApps.Logger.Level.Log);
        export var app = angular.module('excido', ['breeze.angular']);
        app.factory("entityManagerFactory", ["$q", BamApps.Service.breezeEntityManagerFactory]);
        app.factory("sharedContentUnitServiceFactory", ["$q", "entityManagerFactory", BamApps.Excido.Service.SharedContentUnitServiceFactory]);
        app.controller("shared-units", ["$q", "sharedContentUnitServiceFactory", BamApps.Excido.Controller.SharedUnitsController]);
    }
}