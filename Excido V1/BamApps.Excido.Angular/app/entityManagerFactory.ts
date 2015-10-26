// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
module Breeze {
    "use strict";

    export interface IEntityManagerFactory {
        newEntityManager: () => breeze.EntityManager;
        serviceName: string;
    }

    entityManagerFactory.$inject = ["$http", "breeze"];

    export function entityManagerFactory($http: ng.IHttpService, breeze): IEntityManagerFactory {

        var serviceRoot = window.location.protocol + '//' + 'localhost:53941' + '/';
        var serviceName = serviceRoot + 'breeze/ExcidoBreeze';

        function newEntityManager(): breeze.EntityManager {
            return new breeze.EntityManager(serviceName);
        }

        var service: IEntityManagerFactory = {
            newEntityManager : newEntityManager,
            serviceName : serviceName
        };

        return service;
    }

    angular.module("store").factory("entityManagerFactory", entityManagerFactory);
}