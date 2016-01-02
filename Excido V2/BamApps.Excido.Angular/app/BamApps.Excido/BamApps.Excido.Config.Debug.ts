﻿module BamApps {
    export module Excido {
        export module Config {
            export module Debug {
                export class Settings implements BamApps.Excido.Interface.ISettings {
                    _ApiServer: string = 'localhost:44302';
                    _ApiServicePath: string = 'breeze/ExcidoBreeze';
                    _SlugPrefix: string = 'https://localhost:44302/';

                    get ApiServer() {
                        return this._ApiServer;
                    }

                    get ApiServicePath() {
                        return this._ApiServicePath;
                    }

                    get SlugPrefix() {
                        return this._SlugPrefix;
                    }


                    RouteProvider($routeProvider: angular.route.IRouteProvider) {
                        $routeProvider.when('/shared-units', {
                            templateUrl: 'app/BamApps.Excido.View/shared-units.html',
                            controller: 'shared-units',
                            controllerAs: 'sharedunits'
                        })

                        $routeProvider.otherwise({
                            redirectTo: '/shared-units'
                        })
                    }
                }
            }
        }
    }
}