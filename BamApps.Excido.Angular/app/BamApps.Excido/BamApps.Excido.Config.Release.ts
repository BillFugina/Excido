module BamApps {
    export module Excido {
        export module Config {
            export module Release {
                export class Settings implements Interface.ISettings {
                    _ApiServer: string = 'api.excido.net';
                    _ApiServicePath: string = 'breeze/ExcidoBreeze';
                    _SlugPrefix: string = 'http://exci.do/';
                    _AuthenticationServiceBaseUrl: string = 'https://localhost:44300/';

                    get ApiServer() {
                        return this._ApiServer;
                    }

                    get ApiServicePath() {
                        return this._ApiServicePath;
                    }

                    get SlugPrefix() {
                        return this._SlugPrefix;
                    }

                    get AuthenticationServiceBaseUrl() {
                        return this._AuthenticationServiceBaseUrl;
                    }

                    RouteProvider($routeProvider: angular.route.IRouteProvider) {
                        $routeProvider.when('/shared-units', {
                            templateUrl: 'app/BamApps.Excido.View/shared-units.html',
                            controller: 'shared-units',
                            controllerAs: 'sharedunits'
                        })

                        $routeProvider.when('/signup', {
                            templateUrl: 'app/BamApps.Excido.View/signup.html',
                            controller: 'signupController',
                            controllerAs: 'signupController'
                        })

                        $routeProvider.when('/login', {
                            templateUrl: 'app/BamApps.Excido.View/login.html',
                            controller: 'loginController',
                            controllerAs: 'loginController'
                        })

                        $routeProvider.otherwise({
                            redirectTo: '/shared-units'
                        })
                    };

                    uiRouteConfiguration = ($stateProvider: angular.ui.IStateProvider, $urlRouterProvider: angular.ui.IUrlRouterProvider) => {
                        $urlRouterProvider.otherwise('/home');
                        $stateProvider
                            .state('home', {
                                url: '/home',
                                templateUrl: '/app/BamApps.Excido.View/partial-home.html'
                            })
                            .state('about', {
                            });
                    };
                }
            }
        }
    }
}
