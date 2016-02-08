module BamApps {
    export module Excido {
        Logger.verbosity(Logger.Level.Log);
        export var app = angular.module('excido', ['ui.router', 'breeze.angular', 'ui.bootstrap', 'angular-loading-bar', 'monospaced.elastic', 'LocalStorageModule']);

        app.value('authenticationServiceBaseUrl', Configuration.Settings.AuthenticationServiceBaseUrl);

        app.provider('settingsService', BamApps.Excido.Service.SettingsServiceProvider);

        app.controller("loginController", ['$scope', '$state', 'authenticationService', 'settingsService', Excido.Controller.LoginController]);
        app.controller("shared-units", ["$rootScope", "$q", "sharedContentUnitServiceFactory", Excido.Controller.SharedUnitsController]);
        app.controller("signupController", ['$scope', '$location', '$timeout', 'authenticationService', Excido.Controller.SignupController]);
        app.controller("mainAppController", ['$scope', 'settingsService', '$state', Excido.Controller.MainAppController]);
        app.controller("homeController", ['$scope', 'helloWorldService', Excido.Controller.HomeController]);

        app.directive("syncFocusWith", ["$timeout", "$rootScope", "$parse", BamApps.Directive.SyncFocusDirective]);
        app.directive("onEnterKey", ["$timeout", "$rootScope", BamApps.Directive.OnEnterKeyDirective]);
        app.directive("onTabKey", ["$timeout", "$rootScope", BamApps.Directive.OnTabKeyDirective]);
        app.directive("clipboard", ["$timeout", "$rootScope", BamApps.Directive.ClipboardDirective]);

        app.factory("entityManagerFactory", ["$q", "breeze", BamApps.Service.breezeEntityManagerFactory]);
        app.factory("sharedContentUnitServiceFactory", ["$q", "entityManagerFactory", Excido.Service.SharedContentUnitServiceFactory]);
        app.factory("authenticationService", ['$http', '$q', 'localStorageService', 'settingsService', BamApps.Service.authenticationServiceFactory]);
        app.factory("webApiService", ['$http', '$q', 'localStorageService', 'settingsService', BamApps.Excido.Service.WebApiServiceFactory]);
        app.factory("authenticationInterceptorServiceFactory", ['$q', '$location', 'localStorageService', BamApps.Service.getAuthenticationInteceptorService]);
        app.factory("helloWorldService", ['$q', '$http', 'settingsService', BamApps.Service.HelloWorldServiceFactory]);
        //app.factory("stateChangeInspectorService", ['$rootScope', '$urlRouter', 'helloWorldService', BamApps.Service.StateChangeInspectorServiceFactory]);

        app.service("stateChangeInspectorService", ['$rootScope', '$urlRouter', 'helloWorldService', BamApps.Service.StateChangeInspectorService]);

        app.filter("collapse", Filter.CollapseFilter);

        app.config(['$stateProvider', '$urlRouterProvider', Configuration.Settings.uiRouteConfiguration]);

        app.config(($httpProvider: ng.IHttpProvider) => {
            $httpProvider.interceptors.push('authenticationInterceptorServiceFactory');
        });

        app.run(['$rootScope', '$state', '$urlRouter', 'webApiService', function ($rootScope: angular.IRootScopeService, $state: angular.ui.IStateService, $urlRouter: angular.ui.IUrlRouterService, webApiService: BamApps.Excido.Interface.IWebApiService) {
            var bypass = false;
            var waitingForAsync = false;

            $rootScope.$on('$stateChangeStart',
                (event: angular.IAngularEvent, toState: angular.ui.IState, toStateParams, fromState: angular.ui.IState, fromParams, options: angular.ui.IStateOptions) => {
                    if (waitingForAsync) {
                        event.preventDefault();
                        return;
                    }

                    var logObject = {
                        waitingForAsync: waitingForAsync,
                        bypass: bypass,
                        event: event,
                        toState: toState,
                        toStateParams: toStateParams,
                        fromState: fromState,
                        fromStateParams: fromParams,
                        options: options
                    }
                    BamApps.Logger.log('$stateChangeStart', 'StateChangeInspectorService', logObject);

                    if (bypass || (Utils.isNullOrEmpty(toState.redirectWhenAuthenticated) && !toState.protected)) {
                        bypass = false;
                        return;
                    }

                    var states = {
                        toState: toState,
                        fromState: fromState
                    }

                    event.preventDefault();

                    waitingForAsync = true;
                    webApiService.verify()
                        .then(result => {
                            BamApps.Logger.toast(`WebApi Access Verification:"${result}"`, 'StateChangeInspectorService', states, toastr.success, 'WebApi Access Verification');
                            if (!toState.protected && !Utils.isNullOrEmpty(toState.redirectWhenAuthenticated)) {
                                bypass = true;
                                waitingForAsync = false;
                                $state.go(toState.redirectWhenAuthenticated);
                            } else {
                                bypass = true;
                                waitingForAsync = false;
                                $state.go(toState, toStateParams);
                            }
                        })
                        .catch(reason => {
                            BamApps.Logger.error(reason.err.message, 'StateChangeInspectorService', states, toastr.error, 'WebApi Access Verification Failure');
                            waitingForAsync = false;
                            if (!toState.protected && !Utils.isNullOrEmpty(toState.name)) {
                                bypass = true;
                                $state.go(toState.name, toStateParams);
                            }
                            else if (!fromState.protected && !Utils.isNullOrEmpty(fromState.name)) {
                                bypass = true;
                                $state.go(fromState.name, fromParams);
                            } else {
                                bypass = true;
                                $state.go('login');
                            }

                        })
                        .finally(() => {
                            waitingForAsync = false;
                        });

                });
        }]);

    }
}