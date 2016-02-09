module BamApps {
    export module Excido {
        export module Config {
            export module Debug {
                export class Settings implements BamApps.Excido.Interface.ISettings {
                    _ApiServer: string = 'localhost:44302';
                    _ApiBreezeServicePath: string = 'breeze/ExcidoBreeze';
                    _ApiExcidoServicePath: string = 'api/Excido';
                    _ApiClientId: string = 'localExcido';
                    _SlugPrefix: string = 'https://localhost:44302/';
                    _ExcidoServiceBaseUrl: string = 'https://localhost:44302/';
                    _AuthenticationServiceBaseUrl: string = 'https://localhost:44300/';

                    get ApiServer() {
                        return this._ApiServer;
                    }

                    get ApiBreezeServicePath() {
                        return this._ApiBreezeServicePath;
                    }

                    get ApiExcidoServicePath() {
                        return this._ApiExcidoServicePath;
                    }

                    get ApiClientId() {
                        return this._ApiClientId;
                    }

                    get SlugPrefix() {
                        return this._SlugPrefix;
                    }

                    get ExcidoServiceBaseUrl() {
                        return this._ExcidoServiceBaseUrl;
                    }

                    get AuthenticationServiceBaseUrl() {
                        return this._AuthenticationServiceBaseUrl;
                    }

                    uiRouteConfiguration = ($stateProvider: angular.ui.IStateProvider, $urlRouterProvider: angular.ui.IUrlRouterProvider) => {
                        $urlRouterProvider
                            .otherwise(function ($injector, $location) {
                                var $state = $injector.get("$state");
                                $state.go('home');
                            });

                        $stateProvider
                            .state('site', {
                                'abstract': true,
                                template: '<ui-view/>'
                            })
                            .state('main', {
                                'abstract': true,
                                templateUrl: '/app/BamApps.Excido.View/main.html',
                                controller: 'mainController',
                                controllerAs: 'mainController'
                            })
                           
                           
                            .state('home', {
                                parent: 'site',
                                url: '/home',
                                templateUrl: '/app/BamApps.Excido.View/partial-home.html',
                                controller: 'homeController',
                                controllerAs: 'homeController',
                                protected: false,
                                redirectWhenAuthenticated: 'sharedUnits'

                            })
                            .state('login', {
                                parent : 'site',
                                url: '/login',
                                templateUrl: '/app/BamApps.Excido.View/login.html',
                                controller: 'loginController',
                                controllerAs: 'loginController',
                                protected: false,
                                redirectWhenAuthenticated: 'sharedUnits'
                            })
                            .state('logout', {
                                parent: 'site',
                                url: '/logout',
                                templateUrl: '/app/BamApps.Excido.View/logout.html',
                                controller: 'loginController',
                                controllerAs: 'loginController',
                                protected: false
                            })
                            .state('about', {
                                parent: 'site'
                            })
                            .state('sharedUnits', {
                                parent: 'main',
                                url: '/sharedUnits',
                                templateUrl: '/app/BamApps.Excido.View/shared-units.html',
                                controller: 'shared-units',
                                controllerAs: 'shared-units',
                                protected: true
                            })
                            ;
                    };
                }
            }
        }
    }
}