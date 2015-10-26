// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
var Breeze;
(function (Breeze) {
    "use strict";
    entityManagerFactory.$inject = ["$http", "breeze"];
    function entityManagerFactory($http, breeze) {
        var serviceRoot = window.location.protocol + '//' + 'localhost:53941' + '/';
        var serviceName = serviceRoot + 'breeze/ExcidoBreeze';
        function newEntityManager() {
            return new breeze.EntityManager(serviceName);
        }
        var service = {
            newEntityManager: newEntityManager,
            serviceName: serviceName
        };
        return service;
    }
    Breeze.entityManagerFactory = entityManagerFactory;
    angular.module("store").factory("entityManagerFactory", entityManagerFactory);
})(Breeze || (Breeze = {}));
//# sourceMappingURL=entityManagerFactory.js.map