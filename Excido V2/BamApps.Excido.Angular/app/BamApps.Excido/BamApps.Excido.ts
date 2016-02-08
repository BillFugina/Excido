module BamApps {
    "use strict"
    export module Excido {
        Logger.verbosity(Logger.Level.Log);
        export var app = angular.module('excido', ['ui.router', 'breeze.angular', 'ui.bootstrap', 'angular-loading-bar', 'monospaced.elastic', 'LocalStorageModule']);

        app.value('authenticationServiceBaseUrl', Configuration.Settings.AuthenticationServiceBaseUrl);

        app.provider('settingsService', BamApps.Excido.Service.SettingsServiceProvider);

        app.controller("loginController", ['$scope', '$location', 'authenticationServiceFactory', 'settingsService', Excido.Controller.LoginController]);
        app.controller("shared-units", ["$rootScope", "$q", "sharedContentUnitServiceFactory", Excido.Controller.SharedUnitsController]);
        app.controller("signupController", ['$scope', '$location', '$timeout', 'authenticationServiceFactory', Excido.Controller.SignupController]);
        app.controller("mainAppController", ['$scope', 'settingsService', '$state', Excido.Controller.MainAppController]);
        app.controller("homeController", ['$scope', 'helloWorldService', Excido.Controller.HomeController]);

        app.directive("syncFocusWith", ["$timeout", "$rootScope", "$parse", BamApps.Directive.SyncFocusDirective]);
        app.directive("onEnterKey", ["$timeout", "$rootScope", BamApps.Directive.OnEnterKeyDirective]);
        app.directive("onTabKey", ["$timeout", "$rootScope", BamApps.Directive.OnTabKeyDirective]);
        app.directive("clipboard", ["$timeout", "$rootScope", BamApps.Directive.ClipboardDirective]);

        app.factory("entityManagerFactory", ["$q", "breeze", BamApps.Service.breezeEntityManagerFactory]);
        app.factory("sharedContentUnitServiceFactory", ["$q", "entityManagerFactory", Excido.Service.SharedContentUnitServiceFactory]);
        app.factory("authenticationServiceFactory", ['$http', '$q', 'localStorageService', 'settingsService', BamApps.Service.authenticationServiceFactory]);
        app.factory("authenticationInterceptorServiceFactory", ['$q', '$location', 'localStorageService', BamApps.Service.getAuthenticationInteceptorService]);
        app.factory("helloWorldService", ['$q', '$http', 'settingsService', BamApps.Service.HelloWorldServiceFactory]);
        //app.factory("stateChangeInspectorService", ['$rootScope', '$urlRouter', 'helloWorldService', BamApps.Service.StateChangeInspectorServiceFactory]);

        app.service("stateChangeInspectorService", ['$rootScope', '$urlRouter', 'helloWorldService', BamApps.Service.StateChangeInspectorService]);

        app.filter("collapse", Filter.CollapseFilter);

        app.config(['$stateProvider', '$urlRouterProvider', Configuration.Settings.uiRouteConfiguration]);

        app.config(($httpProvider: ng.IHttpProvider) => {
            $httpProvider.interceptors.push('authenticationInterceptorServiceFactory');
        });

        app.run(['$rootScope', '$state', '$urlRouter', 'helloWorldService', function ($rootScope : angular.IRootScopeService, $state: angular.ui.IStateService, $urlRouter: angular.ui.IUrlRouterService, helloWorldService: BamApps.Interface.IHelloWorldService) {
            var bypass = false;

            $rootScope.$on('$stateChangeStart',
                (event: angular.IAngularEvent, toState: angular.ui.IState, toStateParams, fromState: angular.ui.IState, fromParams, options: angular.ui.IStateOptions) => {
                    if (bypass) {
                        bypass = false;
                        return;
                    }

                    event.preventDefault();

                    helloWorldService.SayHello()
                        .then(result => {
                            BamApps.Logger.toast('helloWorldService says"' + result + '"', 'StateChangeInspectorService', result, toastr.success, 'Hello');
                            bypass = true;
                            $state.go(toState, toStateParams);
                        })
                        .catch(reason => {
                            BamApps.Logger.error('helloWorldService says"' + reason + '"', 'StateChangeInspectorService', reason, toastr.error, "Error");
                        });

                });
        }]);

    }
}