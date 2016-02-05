module BamApps {
    export module Excido {
        export module Config {
            export module Debug {
                export class Settings implements BamApps.Excido.Interface.ISettings {
                    _ApiServer: string = 'localhost:44302';
                    _ApiServicePath: string = 'breeze/ExcidoBreeze';
                    _SlugPrefix: string = 'https://localhost:44302/';
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
                                templateUrl: '/app/BamApps.Excido.View/partial-home.html',
                                controller: 'homeController',
                                controllerAs: 'homeController'
                            })
                            .state('about', {
                            });
                    };
                }
            }
        }
    }
}