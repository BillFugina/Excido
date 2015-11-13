module BamApps {
    export module Service {

        breezeEntityManagerFactory.$inject = ["$q", "breeze"];

        export function breezeEntityManagerFactory($q : ng.IQService ): Interface.IBreezeEntityManagerFactory {
            var title = "BamApps.Service.breezeEntityManagerFactory";

            function newEntityManager(hostName: string, servicePath: string): ng.IPromise<breeze.EntityManager> {
                var deferred = $q.defer <breeze.EntityManager>();

                var serviceRoot = window.location.protocol + '//' + hostName + '/';
                var serviceName = serviceRoot + servicePath;
                var entityManager = new breeze.EntityManager(serviceName);

                entityManager.fetchMetadata()
                    .then((result) => {
                        Logger.info("Breeze Metadata successfully fetched.", title, result);
                        deferred.resolve(entityManager);
                    })
                    .catch((reason) => {
                        Logger.error("Breeze Metadata fetch failed!", title, reason);
                        deferred.reject(reason);
                    });

                return deferred.promise;
            }

            var factory: Interface.IBreezeEntityManagerFactory = {
                newEntityManager: newEntityManager
            }


            return factory;
        }
    }
}