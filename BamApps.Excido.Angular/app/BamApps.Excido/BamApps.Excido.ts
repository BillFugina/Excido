﻿module BamApps {
    "use strict"
    export module Excido {
        Logger.verbosity(Logger.Level.Log);
        export var app = angular.module('excido', ['breeze.angular', 'ui.bootstrap', 'monospaced.elastic']);
        app.directive("syncFocusWith", ["$timeout", "$rootScope", "$parse", BamApps.Directive.SyncFocusDirective]);
        app.directive("onEnterKey", ["$timeout", "$rootScope", BamApps.Directive.OnEnterKeyDirective]);
        app.directive("onTabKey", ["$timeout", "$rootScope", BamApps.Directive.OnTabKeyDirective]);
        app.directive("clipboard", ["$timeout", "$rootScope", BamApps.Directive.ClipboardDirective]);
        app.factory("entityManagerFactory", ["$q", BamApps.Service.breezeEntityManagerFactory]);
        app.factory("sharedContentUnitServiceFactory", ["$q", "entityManagerFactory", Excido.Service.SharedContentUnitServiceFactory]);
        app.controller("shared-units", ["$rootScope", "$q", "sharedContentUnitServiceFactory", Excido.Controller.SharedUnitsController]);
        app.filter("collapse", Filter.CollapseFilter);
    }
}